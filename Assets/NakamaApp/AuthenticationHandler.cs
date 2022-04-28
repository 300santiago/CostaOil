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
    private ISession sessionEmployer;
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
    public UserEmployer userEmployer;
    public UsersPermissions usersPermissions;
    private int _case; //variable para dividir el log in de super user, manager y employer. 1:SP, 2: MG, 3: EMP


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
        sessionEmployer = await client.AuthenticateEmailAsync(email, password, "", false);

        DataHolder.instance.session = session;
        DataHolder.instance.email = email;
        ReadMyStorageObjects(email);
        ReadMyStorageObjectsUserEmployer(email);
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
                Key = "UserInfoEmployer",
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
                Key = "UserInfoEmployer",
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
            if (userData[i].Key == "UserInfoEmployer")
            {
                Debug.Log("lectura1");
                DataHolder.superUserclass= JsonUtility.FromJson<SuperUserClass>(userData[i].Value);

            }
        }
        print(DataHolder.superUserclass);
        Debug.Log("");
    }

    IEnumerator delay()
    {
        LogInScene.instance.ShowLoadingPanel();
        yield return new WaitForSeconds(1f);
        LogInScene.instance.DontShowLoadingPanel();
        LogInScene.instance.AuthenticationUsers();
    }





    //creacion de usuarios nuevos por medio de SP
    public async void SignUpNewEmployers(string _emailEmployer, string _password, string _nameEmployer)
    {
        string newEmail = $"{_emailEmployer}";
        session = await client.AuthenticateEmailAsync(newEmail, _password, _nameEmployer, true);
        StorageObjectsEmployer(newEmail, _nameEmployer, _password);

    }
    public async void StorageObjectsEmployer(string email, string _nameEmployer, string _passwordEmployer)
    {
        userEmployer = new UserEmployer
        {
            nameEmployer = _nameEmployer,
            positionEmployer = "",
            emailEmployer = email,
            passwordEmployer = _passwordEmployer,
            sucursalEmployer = "",
            tutorialFirst = false,
        };

        usersPermissions = new UsersPermissions
        {
            createUserEmployer = false,
            createUserManager = false,
            createNewSucursals = false,
            createNewWorkCar = true,
        };
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfoEmployer",
                Value = JsonUtility.ToJson(userEmployer)
            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions)        
            }
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
        DataHolder.userEmployer = userEmployer;
        DataHolder.usersPermissions = usersPermissions;
    }



    public async void ReadMyStorageObjectsUserEmployer(string email)
    {
         IApiReadStorageObjectId[] objectsId = {
            new StorageObjectId
            {
                Collection = email,
                Key = "UserInfoEmployer",
                UserId = sessionEmployer.UserId
            },

            new StorageObjectId
            {
                Collection = email,
                Key = "UserPermissions",
                UserId = sessionEmployer.UserId
            }
        };
        IApiStorageObjects objects = await client.ReadStorageObjectsAsync(sessionEmployer, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        foreach (IApiStorageObject o in userData)
        {
            print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfoEmployer")
            {
                Debug.Log("lectura2");
                userEmployer = JsonUtility.FromJson<UserEmployer>(userData[i].Value);
                DataHolder.userEmployer = userEmployer;
                //DataHolder.superUserclass= JsonUtility.FromJson<SuperUserClass>(userData[i].Value);
            }
            else if (userData[i].Key == "UserPermissions")
            {
                usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
                DataHolder.usersPermissions = usersPermissions;
            }
        }
        _case = 2;
        Debug.Log(_case);
        Debug.Log("employer");
        print(DataHolder.userEmployer);
    }
}
