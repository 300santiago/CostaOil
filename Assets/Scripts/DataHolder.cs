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

    public async void WriteBoolean(string _email)
    {
        Debug.Log("funcion");
        superUserclass.tutorialFirst = true;

        IApiWriteStorageObject[] writeObjects = new[]
        {
            
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfoEmployer",
                Value = JsonUtility.ToJson(superUserclass)
            }
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }

    public async void ChangeNameSP()
    {
        string email = "superuser@hotmail.com";
        //superUserclass.nameSuperUser = UserSettings.instance.nameSuperUser.text;

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

    public async void WriteBoolean2(string _email, string _password)
    {
        Debug.Log("funcion");
        userEmployer.tutorialFirst = true;
        userEmployer.passwordEmployer = _password;
        IApiWriteStorageObject[] writeObjects = new[]
        {
            
            new WriteStorageObject
            {
                Collection = _email,
                Key = "UserInfoEmployer",
                Value = JsonUtility.ToJson(userEmployer)
            }
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }
}
