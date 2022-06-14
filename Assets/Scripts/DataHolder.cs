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
    [Header("classes:")]
    public static DataHolder instance;
    public static SuperUserClass superUserclass;
    public static UserEmployee userEmployer;
    public static UsersPermissions usersPermissions;
    public static UserManager userManager;
    public static Sucursals sucursals;
    public static ListSucursals listSucursals;
    //public static GroupEmployers groupEmployers;
    public static GroupManagers groupManagers;
    public static ListEmployersClass listEmployersClass;
    public static SuperAdminClass superAdminClass;
    public static BasicUserEmployee basicUserEmployer;
    public static BasicUserManager basicUserManager;
    //public static CarJobsClass carJobsClass;

 
    [Header("nakama")]
    public IClient client;
    public ISession session;
    public ISession sessionSuperAdmin;
    private RetryConfiguration retryConfiguration;
    

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
    //Write information to the Admin User which holds the database information
    public async void WriteNakamaAdmUser(string email)
    {
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "InfoAdmin",
                Value = JsonUtility.ToJson(superAdminClass),
                //PermissionRead = 2,
            },
        };
        await client.WriteStorageObjectsAsync(sessionSuperAdmin, writeObjects);
    }
    //Write information to the super user Boss
    public async void WriteNakamaSuperUser(string email)
    {
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

    public async void WriteNakamaEmployerUser(string email)
    {
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(userEmployer)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }

    public async void WriteNakamaManagerrUser(string email)
    {
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(userManager)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }
    #region ChangePasswordEmployer
    public async void ChangePassword(string _password, WorkerKind _workerKind)
    {
        if(_workerKind == WorkerKind.superUser)
        {
            await client.LinkEmailAsync(session, superUserclass.emailSuperUser, _password, retryConfiguration);
        }
        else if (_workerKind == WorkerKind.admin)
        {
            await client.LinkEmailAsync(session, userManager.emailManager, _password, retryConfiguration);
            TutorialScene.instance.AfterChangePswAdmin();
        }
        else if (_workerKind == WorkerKind.employee)
        {
            await client.LinkEmailAsync(session, userEmployer.emailEmployee, _password, retryConfiguration);
        }
    }
    #endregion
}
    

    

























































    /*public async void WriteNakamaSuperUser(string email)
    {
        Debug.Log("Funcion Nakama escribir super user");
        Debug.Log($"La sucursal que se guardara es: {DataHolder.sucursals.nameSucursal}");
        superUserclass.tutorialFirst = true;
        
        IApiWriteStorageObject[] writeObjects = new[]
        {
            
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserclass)
            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions)
            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "Sucursal",
                Value = JsonUtility.ToJson(sucursals)
            },
            new WriteStorageObject
            {
                Collection = email,
                Key = "ListSucursals",
                Value = JsonUtility.ToJson(listSucursals)
            },
            new WriteStorageObject
            {
                Collection = email,
                Key = "ListEmployers",
                Value = JsonUtility.ToJson(groupEmployers)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }*/

