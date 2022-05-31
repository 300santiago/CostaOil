using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nakama;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;

public class AuthenticationHandler : MonoBehaviour
{


    [Header("variables Nakama")]
    //variables nakama
    private IClient client;
    private ISession session;
    private ISession sessionSuperAdmin;
    private ISession sessionEmployer;
    private IApiAccount account;
    public string protocol;
    public string server;
    public int port;
    public string serverKey;
    private RetryConfiguration retryConfiguration;
    private ISocket socket;
    [HideInInspector][SerializeField] NakamaSessionHandler sessionHandler;
    private CancellationTokenSource canceller = new CancellationTokenSource();

    [Header("Variables credentials")]
    public string email;
    private string password;
    [Header("Classes")]
    public SuperUserClass superUserClass;
    public ListSucursals listSucursals;
    public Sucursals sucursals;
    public UserEmployer userEmployer;
    public UsersPermissions usersPermissions;
    public UserManager userManager;
    public GroupManagers groupManagers;
    public ListEmployersClass listEmployersClass;
    public SuperAdminClass superAdminClass;
    public BasicUserEmployer basicUserEmployer;
    public BasicUserManager basicUserManager;
    public static AuthenticationHandler instance;
    [Header("Variables Super Admin User")] //This user holds all the database as employees list, manager List, sucursal List
    public string superUserAdminEmail = "superuseradmin@correo.com";
    [Header("Variables SuperUser")]
    public string superAdminEmail = "superadmin@costaoil.com";
    private string superAdminPassword = "Super1234.";
    private string superAdminName = "SuperAdmin";

    private void Awake()
    {
        instance = this;
        sessionHandler = GameObject.Find("DataHolder").GetComponent<NakamaSessionHandler>();
        try
        {
            retryConfiguration = new RetryConfiguration(1, 5, delegate { print("about to retry."); });
            client = new Client(protocol, server, port, serverKey, UnityWebRequestAdapter.Instance);
            socket = client.NewSocket(true);
            sessionHandler.AssignRefrences(client, socket);
            socket.Closed += () => Debug.Log("Socket closed.");
            socket.Connected += () => Debug.Log("Socket connected.");
            socket.ReceivedError += e => Debug.Log("Socket error: " + e.Message);
            DataHolder.instance.client = client;
        }

        catch (Exception e)
        {
            print(e.Message);
            throw;
        }
    }
    private void Start()
    {

    }
    //log in de super usuario administrador:
    public async void LoginSadmin()
    {
        string _passwordSuperAdmin = "12345678";
        sessionSuperAdmin = await client.AuthenticateEmailAsync(superUserAdminEmail, _passwordSuperAdmin, "Super User Admin", false);
        DataHolder.instance.sessionSuperAdmin = sessionSuperAdmin;
        ReadMyStorageObjectsSadmin(superUserAdminEmail);
    }
    // log in normal de usuarios:
    public async void Login()
    {
        email = LogInScene.instance.emailCredentials.text;
        password = LogInScene.instance.passwordCredentials.text;
        LogInScene.instance.ShowLoadingPanel();
        try
        {
            session = await client.AuthenticateEmailAsync(email, password, "", false);
            DataHolder.instance.session = session;
            ReadMyStorageObjectsSuser(email);
            ReadMyStorageObjectsUserEmployer(email);
            ReadMyStorageObjectsUserManager(email);
            LogInScene.instance.DontShowLoadingPanel();
            StartCoroutine(delay());
        }
        catch (ApiResponseException e)
        {
            ErrorLogin.instance.ShowError(e.Message);
            Debug.LogError($"Error: {e.Message} / codes: {e.StatusCode}, {e.GrpcStatusCode}");
        }

    }
    public void ClearFields()
    {
        LogInScene.instance.emailCredentials.text = string.Empty;
        LogInScene.instance.passwordCredentials.text = string.Empty;
    }
    //sign up de Super Usuario Administrador:
    public async void SignUpSuperAdminUser()
    {
        string _passwordSuperAdmin;
        _passwordSuperAdmin = "12345678";
        sessionSuperAdmin = await client.AuthenticateEmailAsync(superUserAdminEmail, _passwordSuperAdmin, "Super User Admin", true);
        StorageObjectsSadmin(superUserAdminEmail);
    }
    //escritura nakama Super Usuario Administrador:
    public async void StorageObjectsSadmin(string _emailSuperAdmin)
    {
        superAdminClass = new SuperAdminClass
        {
            nameAdmin = "Super Admin"
        };
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = _emailSuperAdmin,
                Key = "InfoAdmin",
                Value = JsonUtility.ToJson(superAdminClass),
                PermissionRead = 2,
            },
        };
        await client.WriteStorageObjectsAsync(sessionSuperAdmin, writeObjects);
        DataHolder.superAdminClass = superAdminClass;
    }


    //Lectura nakama Super Usuario Administrador:
    public async void ReadMyStorageObjectsSadmin(string _emailSuperAdmin)
    {
        IApiReadStorageObjectId[] objectsId =
        {
            new StorageObjectId
            {
                Collection = _emailSuperAdmin,
                Key = "InfoAdmin",
                UserId = sessionSuperAdmin.UserId
            },
        };
        IApiStorageObjects objects = await client.ReadStorageObjectsAsync(sessionSuperAdmin, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        foreach (IApiStorageObject o in userData)
        {
            //print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "InfoAdmin")
            {
                DataHolder.superAdminClass = JsonUtility.FromJson<SuperAdminClass>(userData[i].Value);
            }
        }
    }

    //sign up de super usuario:
    // public async void SignUp()
    // {
    //     email = LogInScene.instance.emailCredentials.text;
    //     password = LogInScene.instance.passwordCredentials.text;
    //     session = await client.AuthenticateEmailAsync(email, password, "Super User", true);
    //     superUserStorageObjects(email);
    // }
    //escribir en nakama super user unico registro



    //lectura de datos super user
    public async void ReadMyStorageObjectsSuser(string email)
    {
        IApiReadStorageObjectId[] objectsId = {
            new StorageObjectId
            {
                Collection = email,
                Key = "UserInfo",
                UserId = session.UserId
            },
            new StorageObjectId
            {
                Collection = email,
                Key = "UserPermissions",
                UserId = session.UserId
            },
        };
        IApiStorageObjects objects = await client.ReadStorageObjectsAsync(session, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        foreach (IApiStorageObject o in userData)
        {
            //print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                DataHolder.superUserclass = JsonUtility.FromJson<SuperUserClass>(userData[i].Value);
            }
            else if (userData[i].Key == "UserPermissions")
            {
                DataHolder.usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
            }
        }
    }

    IEnumerator delay()
    {
        //LogInScene.instance.ShowLoadingPanel();
        yield return new WaitForSeconds(1f);
        //LogInScene.instance.DontShowLoadingPanel();
        LogInScene.instance.AuthenticationUsers();
    }
    //creacion de usuarios nuevos por medio de SP
    ///////////////////////////////////////////////////////////////
    //Usuario Employer:
    public async void SignUpNewEmployers(string _emailEmployer, string _password, string _nameEmployer, string _sucursal)
    {
        session = await client.AuthenticateEmailAsync(_emailEmployer, _password, _nameEmployer, true);
        StorageObjectsEmployer(_emailEmployer, _password, _nameEmployer, _sucursal);
    }

    public async void StorageObjectsEmployer(string email, string _passwordEmployer, string _nameEmployer, string _sucursal)
    {

        userEmployer = new UserEmployer
        {
            nameEmployer = _nameEmployer,
            positionEmployer = "",
            emailEmployer = email,
            sucursalEmployer = _sucursal,
            tutorialFirst = false,
        };

        usersPermissions = new UsersPermissions
        {
            createUserEmployer = false,
            createUserManager = false,
            createNewSucursals = false,
            createNewWorkCar = true,
        };
        basicUserEmployer = new BasicUserEmployer
        {
            nameEmployer = _nameEmployer,
            sucursalEmployer = _sucursal,
            emailEmployer = email,
            idEmployer = session.UserId,
        };

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(userEmployer),
                PermissionRead = 2,
                PermissionWrite = 1
            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions),
                PermissionRead = 2,
                PermissionWrite = 1

            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "BasicInfoUser",
                Value = JsonUtility.ToJson(basicUserEmployer),
                PermissionRead = 2,
                PermissionWrite = 1

            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
        DataHolder.userEmployer = userEmployer;
        DataHolder.superAdminClass.listEmployers.Add(basicUserEmployer);
        DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
    }


    //lectura de datos de usuarios employers creados
    public async void ReadMyStorageObjectsUserEmployer(string email)
    {
        IApiReadStorageObjectId[] objectsId = {
            new StorageObjectId
            {
                Collection = email,
                Key = "UserInfo",
                UserId = session.UserId,

            },

            new StorageObjectId
            {
                Collection = email,
                Key = "UserPermissions",
                UserId = session.UserId
            },
            new StorageObjectId
            {
                Collection = email,
                Key = "BasicInfoUser",
                UserId = session.UserId,
            }
        };
        IApiStorageObjects objects = await client.ReadStorageObjectsAsync(session, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        foreach (IApiStorageObject o in userData)
        {
            //print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                DataHolder.userEmployer = JsonUtility.FromJson<UserEmployer>(userData[i].Value);
            }
            else if (userData[i].Key == "UserPermissions")
            {
                DataHolder.usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
            }
            else if (userData[i].Key == "BasicInfoUser")
            {
                DataHolder.basicUserEmployer = JsonUtility.FromJson<BasicUserEmployer>(userData[i].Value);
            }
        }
    }



    //creacion de usuario manager por medio se SP:
    ///////////////////////////////////////////////////////////////
    public async void SignUpNewManager(string _emailManager, string _passwordManager, string _nameManager, string _nameSucursal)
    {
        session = await client.AuthenticateEmailAsync(_emailManager, _passwordManager, _nameManager, true);
        //StorageObjectsManager(_emailManager, _passwordManager, _nameManager, _nameSucursal);
    }

    public async void ReadMyStorageObjectsUserManager(string email)
    {
        IApiReadStorageObjectId[] objectsId = {
            new StorageObjectId
            {
                Collection = email,
                Key = "UserInfo",
                UserId = session.UserId
            },

            new StorageObjectId
            {
                Collection = email,
                Key = "UserPermissions",
                UserId = session.UserId
            },
            new StorageObjectId
            {
                Collection = email,
                Key = "BasicInfoUser",
                UserId = session.UserId
            }
        };
        IApiStorageObjects objects = await client.ReadStorageObjectsAsync(session, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        foreach (IApiStorageObject o in userData)
        {
            //print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                DataHolder.userManager = JsonUtility.FromJson<UserManager>(userData[i].Value);


            }
            else if (userData[i].Key == "UserPermissions")
            {
                DataHolder.usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
            }
            else if (userData[i].Key == "BasicInfoUser")
            {
                DataHolder.basicUserManager = JsonUtility.FromJson<BasicUserManager>(userData[i].Value);
            }
        }
    }
    #region SuperUser
    //One time function
    public async void CreateSuperUser()
    {
        try
        {
            session = await client.AuthenticateEmailAsync(superAdminEmail, superAdminPassword, "SuperAdmin", true, null, retryConfiguration);
            superUserStorageObjects();
            print("done");
        }
        catch (ApiResponseException e)
        {
            Debug.LogError($"Error: {e.Message} / codes: {e.StatusCode}, {e.GrpcStatusCode}");
        }
    }
    public async void superUserStorageObjects()
    {
        superUserClass = new SuperUserClass
        {
            nameSuperUser = superAdminName,
            emailSuperUser = superAdminEmail,
            tutorialFirst = false
        };
        usersPermissions = new UsersPermissions
        {
            createUserEmployer = true,
            createUserManager = true,
            createNewSucursals = true,
            createNewWorkCar = true
        };

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = superAdminEmail,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserClass)
            },
            new WriteStorageObject
            {
                Collection = superAdminEmail,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }
    #endregion

}
