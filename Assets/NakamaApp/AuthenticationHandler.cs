using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nakama;
using System.Linq;



public class AuthenticationHandler : MonoBehaviour
{
    
    [Header("variables Nakama")]
    //variables nakama
    private IClient client;
    private ISession session;
    private IApiAccount account;
    public string protocol;
    public string server;
    public int port;
    public string serverKey;

    [Header("Variables credentials")]
    private string email;
    private string password;
    [Header("Classes")]
    public SuperUserClass superUserClass;



    public static  AuthenticationHandler instance;

    private void Awake()
    {

        instance = this;    
    }
    private void Start() 
    {
        try
        {
            client = new Client (protocol, server, port ,serverKey);
            DataHolder.instance.client = client;
        }   

        catch (Exception e)
        {
            print(e.Message);
            throw;
        }
    }

    public async void Login()
    {
        email = LogInScene.instance.emailCredentials.text;
        password = LogInScene.instance.passwordCredentials.text;
        session = await client.AuthenticateEmailAsync(email, password, "Super User", false);
        DataHolder.instance.session = session;
        DataHolder.instance.email = email;
        ReadMyStorageObjects(email);
        StartCoroutine(delay());
    }

    public async void SignUp()
    {
        email = LogInScene.instance.emailCredentials.text;
        password = LogInScene.instance.passwordCredentials.text;
        session = await client.AuthenticateEmailAsync(email, password, "Super User", true);
        StorageObjects(email);
    }



    //escribir en nakama
    public async void StorageObjects(string email)
    {
        superUserClass = new SuperUserClass
         {
             nameSuperUser = "",
             emailSuperUser =  LogInScene.instance.emailCredentials.text,
             passwordSuperUser = LogInScene.instance.passwordCredentials.text,
             tutorialFirst = false,
         };

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserClass)
            }
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
        DataHolder.superUserclass = superUserClass;
    }


     public async void ReadMyStorageObjects(string email)
    {
        IApiReadStorageObjectId[] objectsId = {
            new StorageObjectId
            {
                Collection = email,
                Key = "UserInfo",
                UserId = session.UserId
            }
        };
        IApiStorageObjects objects = await client.ReadStorageObjectsAsync(session, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        foreach (IApiStorageObject o in userData)
        {
            print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                DataHolder.superUserclass= JsonUtility.FromJson<SuperUserClass>(userData[i].Value);

            }
        }
    }

    IEnumerator delay()
    {
        LogInScene.instance.ShowLoadingPanel();
        yield return new WaitForSeconds(1f);
        LogInScene.instance.DontShowLoadingPanel();
        LogInScene.instance.AuthenticationUsers();
    }

}
