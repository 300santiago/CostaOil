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
    public UserEmployee userEmployer;
    public UsersPermissions usersPermissions;
    public UserManager userManager;
    public GroupManagers groupManagers;
    public ListEmployersClass listEmployersClass;
    public SuperAdminClass superAdminClass;
    public BasicUserEmployee basicUserEmployer;
    public BasicUserManager basicUserManager;
    public static AuthenticationHandler instance;
    [Header("Variables Super Admin User")] //This user holds all the database as employees list, manager List, sucursal List
    public string superUserAdminEmail = "adminaccount@costaoil.com";
    public string superUserAdminPassword = "admin1234.";
    [Header("Variables SuperUser")]
    public string superUserEmail = "superadmin@costaoil.com";
    private string superUserPassword = "Super1234.";
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
            ReadPermissions(email);
            LogInScene.instance.HideLoadingPanel();
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
        yield return new WaitForSeconds(1f);
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

        userEmployer = new UserEmployee
        {
            nameEmployee = _nameEmployer,
            positionEmployee = "",
            emailEmployee = email,
            sucursalEmployee = _sucursal,
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
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
        DataHolder.userEmployer = userEmployer;
        //DataHolder.superAdminClass.listEmployee.Add(basicUserEmployer);
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
                DataHolder.userEmployer = JsonUtility.FromJson<UserEmployee>(userData[i].Value);
            }
            else if (userData[i].Key == "UserPermissions")
            {
                DataHolder.usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
            }
            else if (userData[i].Key == "BasicInfoUser")
            {
                DataHolder.basicUserEmployer = JsonUtility.FromJson<BasicUserEmployee>(userData[i].Value);
            }
        }
    }
    //creacion de usuario manager por medio se SP:
    ///////////////////////////////////////////////////////////////
    public async void SignUpNewManager(string _emailManager, string _passwordManager, string _nameManager, string _nameSucursal)
    {
        try
        {
            session = await client.AuthenticateEmailAsync(_emailManager, _passwordManager, _nameManager, true);
            StorageObjectsManager(_emailManager, _passwordManager, _nameManager, _nameSucursal);
        }
        catch (ApiResponseException e)
        {
            ManagerScene.instance.ShowError(e.Message);
            ManagerScene.instance.LoadingOff();
            ManagerScene.instance.ClearInputFieldsManager();
            PanelManagerMainScene.instance.LoadMainPanelSuper();
        }
    }
    public async void StorageObjectsManager(string email, string _password, string _name, string _nameSucursal)
    {
        string managerID = "";
        if (DataHolder.superAdminClass.listAdmins.Count < 1)
        {
            managerID = "ad01";
        }
        else
        {
            managerID = $"ad{DataHolder.superAdminClass.listAdmins.Count + 1}";
        }
        userManager = new UserManager
        {
            nameManager = _name,
            emailManager = email,
            sucursalManager = _nameSucursal,
            tutorialFirst = false,
            idManager = managerID,
            passwordAdmin = _password,
        };
        usersPermissions = new UsersPermissions
        {
            createUserEmployer = true,
            createUserManager = false,
            createNewSucursals = false,
            createNewWorkCar = false,
        };
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(userManager)
            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);

        for (int i = 0; i < DataHolder.superAdminClass.listSucursals.Count; i++)
        {
            if (DataHolder.superAdminClass.listSucursals[i].nameSucursal == _nameSucursal)
            {
                DataHolder.superAdminClass.listSucursals[i].sucursalManager = userManager;
                break;
            }
        }
        ManagerScene.instance.LoadingOff();
        ManagerScene.instance.ClearInputFieldsManager();
        PanelManagerMainScene.instance.LoadMainPanelSuper();
        ManagerScene.instance.ShowNewCredentials(email, _password, _name, managerID);
        DataHolder.superAdminClass.listAdmins.Add(userManager);
        DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
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
            session = await client.AuthenticateEmailAsync(superUserEmail, superUserPassword, "SuperUser", true, null, retryConfiguration);
            superUserStorageObjects();
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
            emailSuperUser = superUserEmail,
            tutorialFirst = false
        };
        usersPermissions = new UsersPermissions
        {
            createUserEmployer = true,
            createUserManager = true,
            createNewSucursals = true,
            createNewWorkCar = true,
            workerKind = WorkerKind.superUser
        };

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = superUserEmail,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserClass)
            },
            new WriteStorageObject
            {
                Collection = superUserEmail,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }
    #endregion
    #region AdminAccount Creation & Management, this account holds all the sucrusals and work orders of the DataBase
    public async void SignUpSuperAdminUser()
    {
        sessionSuperAdmin = await client.AuthenticateEmailAsync(superUserAdminEmail, superUserAdminPassword, "AdminAccount", true);
        StorageObjectsSadmin(superUserAdminEmail);
    }
    public async void StorageObjectsSadmin(string _emailSuperAdmin)
    {
        superAdminClass = new SuperAdminClass
        {
            nameAdmin = "AdminAccount"
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
    public async void LoginSadmin()
    {
        sessionSuperAdmin = await client.AuthenticateEmailAsync(superUserAdminEmail, superUserAdminPassword, "AdminAccount", false);
        DataHolder.instance.sessionSuperAdmin = sessionSuperAdmin;
        ReadMyStorageObjectsSadmin(superUserAdminEmail);
    }
    #endregion
    #region  read permissions
    public async void ReadPermissions(string email)
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

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserPermissions")
            {
                DataHolder.usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
                //Is superUser///////////////////////////
                if (DataHolder.usersPermissions.workerKind == WorkerKind.superUser)
                {
                    LoadInfoSuperUser(userData);
                }
                //Is Admin User///////////////////////////
                else if (DataHolder.usersPermissions.workerKind == WorkerKind.admin)
                {
                    LoadInfoAdminUser(userData);
                }
                //Is employee User///////////////////////////
                else if (DataHolder.usersPermissions.workerKind == WorkerKind.employee)
                {
                    LoadInfoEmployeeUser(userData);
                }
            }
        }
    }
    public void LoadInfoSuperUser(IApiStorageObject[] _userData)
    {
        for (int i = 0; i < _userData.Length; i++)
        {
            if (_userData[i].Key == "UserInfo")
            {
                DataHolder.superUserclass = JsonUtility.FromJson<SuperUserClass>(_userData[i].Value);
            }
        }
    }
    public void LoadInfoAdminUser(IApiStorageObject[] _userData)
    {
        for (int i = 0; i < _userData.Length; i++)
        {
            if (_userData[i].Key == "UserInfo")
            {
                DataHolder.userManager = JsonUtility.FromJson<UserManager>(_userData[i].Value);
            }
        }
    }
    public void LoadInfoEmployeeUser(IApiStorageObject[] _userData)
    {
        for (int i = 0; i < _userData.Length; i++)
        {
            if (_userData[i].Key == "UserInfo")
            {
                DataHolder.userEmployer = JsonUtility.FromJson<UserEmployee>(_userData[i].Value);
            }
        }
    }
    #endregion
}
