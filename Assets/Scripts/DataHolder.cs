using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using System.Linq;
using Nakama.TinyJson;
using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    
    public static DataHolder instance;
    public static SuperUserClass superUserclass;
    public static UserEmployer userEmployer;
    public static UsersPermissions usersPermissions;
    public static UserManager userManager;
    public static Sucursals sucursals;
    public static ListSucursals listSucursals;
   



    public IClient client;
    public ISession session;
    public ISession sessionEmployer;
    public IApiAccount account;
    

    public string email;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;   
    }

    public async void WriteNakamaSuperUser(string email)
    {
        Debug.Log("Funcion Nakama escribir super user");
        superUserclass.tutorialFirst = true;
        IApiWriteStorageObject[] writeObjects = new[]
        {
            
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserclass)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }


    public async void ChangeNameSP(string email)
    {
        Debug.Log("Funcion nakama sobreescribir nombre");
        IApiWriteStorageObject[] writeObjects = new[]
        {
            
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserclass)
            }
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);

    }

    public async void ChangePasswordEmployer(string _email)
    {
        Debug.Log("Cambiar password");
        userEmployer.tutorialFirst = true;
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = _email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(userEmployer)
            }

        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }

    public async void ChangePasswordManager(string _email)
    {
        Debug.Log("Cambiar password");
        userManager.tutorialFirst = true;
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = _email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(userManager)
            }

        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }
}
