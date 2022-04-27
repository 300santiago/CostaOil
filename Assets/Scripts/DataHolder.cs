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
    public IClient client;
    public ISession session;
    public IApiAccount account;

    public string email;
    //public string password;

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

    public async void WriteBoolean()
    {
        string email = "superuser@hotmail.com";
        Debug.Log("funcion");
        superUserclass.tutorialFirst = true;

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
}
