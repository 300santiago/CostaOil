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
    private ISession sessionSuperAdmin;
    private ISession sessionEmployer;
    private IApiAccount account;
    public string protocol;
    public string server;
    public int port;
    public string serverKey;

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
    public GroupEmployers groupEmployers; 
    public GroupManagers groupManagers;
    public ListEmployersClass listEmployersClass;
    public SuperAdminClass superAdminClass;
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

    //log in de super usuario administrador:
    public async void LoginSadmin()
    {
        string _emailSuperAdmin = "admin@hotmail.com";
        string _passwordSuperAdmin = "12345678";
        sessionSuperAdmin = await client.AuthenticateEmailAsync(_emailSuperAdmin, _passwordSuperAdmin, "Super User Admin", false);
        ReadMyStorageObjectsSadmin(_emailSuperAdmin);

    }
    // log in normal de usuarios:
    public async void Login()
    {
        email = LogInScene.instance.emailCredentials.text;
        password = LogInScene.instance.passwordCredentials.text;
        session = await client.AuthenticateEmailAsync(email, password, "", false);
        ReadMyStorageObjectsSuser(email);
        ReadMyStorageObjectsUserEmployer(email);
        ReadMyStorageObjectsUserManager(email);
        StartCoroutine(delay());
    }

    //sign up de Super Usuario Administrador:
    public async void SignUpSuperAdminUser()
    {
        string _emailSuperAdmin;
        string _passwordSuperAdmin;
        _emailSuperAdmin = LogInScene.instance.emailCredentials.text;
        _passwordSuperAdmin = "12345678";
        sessionSuperAdmin = await client.AuthenticateEmailAsync(_emailSuperAdmin, _passwordSuperAdmin, "Super User Admin", true);
        StorageObjectsSadmin(_emailSuperAdmin);
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
                Value = JsonUtility.ToJson(superAdminClass)
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
                DataHolder.superAdminClass= JsonUtility.FromJson<SuperAdminClass>(userData[i].Value);
            }
        }
    }

   
   
   
   
    //sign up de super usuario:
    public async void SignUp()
    {
        email = LogInScene.instance.emailCredentials.text;
        password = LogInScene.instance.passwordCredentials.text;
        session = await client.AuthenticateEmailAsync(email, password, "Super User", true);
        StorageObjects(email);
    }
    //escribir en nakama super user unico registro
    public async void StorageObjects(string email)
    {
        superUserClass = new SuperUserClass
        {
             nameSuperUser = "",
             emailSuperUser =  LogInScene.instance.emailCredentials.text,
             tutorialFirst = false,
        };
         usersPermissions = new UsersPermissions
        {
            createUserEmployer = true,
            createUserManager = true,
            createNewSucursals = true,
            createNewWorkCar = true,
        };

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserClass)
            },

            new WriteStorageObject
            {
                Collection = email,
                Key = "UserPermissions",
                Value = JsonUtility.ToJson(usersPermissions)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
        DataHolder.superUserclass = superUserClass;
        DataHolder.usersPermissions = usersPermissions;
    }


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
                DataHolder.superUserclass= JsonUtility.FromJson<SuperUserClass>(userData[i].Value);
            }
            else if (userData[i].Key == "UserPermissions")
            {
                DataHolder.usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
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




    





    //creacion de usuarios nuevos por medio de SP
    ///////////////////////////////////////////////////////////////
    //Usuario Employer:
    public async void SignUpNewEmployers(string _emailEmployer, string _password, string _nameEmployer, string _sucursal)
    {
        session = await client.AuthenticateEmailAsync(_emailEmployer,_password, _nameEmployer, true);
        StorageObjectsEmployer(_emailEmployer, _password, _nameEmployer, _sucursal);
    }

    public async void StorageObjectsEmployer(string email,  string _passwordEmployer, string _nameEmployer, string _sucursal)
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

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
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
        AddEmployerList(userEmployer);
    }


    //lectura de datos de usuarios employers creados
    public async void ReadMyStorageObjectsUserEmployer(string email)
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
        }

    }



    //creacion de usuario manager por medio se SP:
    ///////////////////////////////////////////////////////////////
    public async void SignUpNewManager(string _emailManager, string _passwordManager, string _nameManager, string _nameSucursal)
    {
        session = await client.AuthenticateEmailAsync(_emailManager, _passwordManager, _nameManager, true);
        StorageObjectsManager(_emailManager, _passwordManager, _nameManager, _nameSucursal);
    }


    public async void StorageObjectsManager(string email,  string _password, string _name, string _nameSucursal)
    {
        userManager = new UserManager
        {
            nameManager = _name,
            emailManager = email,
            sucursalManager = _nameSucursal,
            tutorialFirst = false,
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
            }
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
        DataHolder.userManager = userManager;
        DataHolder.usersPermissions = usersPermissions;
        AddManagerList(userManager);
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
        }
    }
 
    public void AddSucursal(Sucursals _sucursals)
    {
        string _emailSuperAdmin = "admin@hotmail.com"; 
        DataHolder.superAdminClass.listSucursals.Add(_sucursals);
        WriteNakamaSuperUser(email);   
        WriteNakamaAdminUser(_emailSuperAdmin);
    }

    public void AddEmployerList(UserEmployer _userEmployer)
    {
        string _emailSuperAdmin = "admin@hotmail.com";
        DataHolder.superAdminClass.listEmployers.Add(_userEmployer);
        WriteNakamaAdminUser(_emailSuperAdmin);
    }

    public void AddManagerList(UserManager _userManager)
    {
        string _emailSuperAdmin = "admin@hotmail.com";
        DataHolder.superAdminClass.listManagers.Add(_userManager);
        WriteNakamaAdminUser(_emailSuperAdmin);
    }

    public async void WriteNakamaSuperUser(string email)
    {
        superUserClass = new SuperUserClass
        {
            nameSuperUser = DataHolder.superUserclass.nameSuperUser,
            tutorialFirst = DataHolder.superUserclass.tutorialFirst,
            
        };
        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "UserInfo",
                Value = JsonUtility.ToJson(superUserClass)
            },
        };
        await client.WriteStorageObjectsAsync(session, writeObjects);
    }

     public async void WriteNakamaAdminUser(string email)
    {
        superAdminClass = new SuperAdminClass
        {
            listEmployers = DataHolder.superAdminClass.listEmployers,
            listManagers = DataHolder.superAdminClass.listManagers,
            listSucursals = DataHolder.superAdminClass.listSucursals,
        };

        IApiWriteStorageObject[] writeObjects = new[]
        {
            new WriteStorageObject
            {
                Collection = email,
                Key = "InfoAdmin",
                Value = JsonUtility.ToJson(superAdminClass)
            },
        };
        await client.WriteStorageObjectsAsync(sessionSuperAdmin, writeObjects);
    }

    public async void WriteNakamaManagerUser(string email)
    {
        userManager = new UserManager
        {
            tutorialFirst = DataHolder.userManager.tutorialFirst
        };

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


























    public void AddListEmployers(UserEmployer _userEmployer)
    {
        groupEmployers.employers.Add(_userEmployer);
        DataHolder.groupEmployers = groupEmployers;
        DataHolder.instance.WriteNakamaSaveEmployerList(email);
    }

    public void AddListEmployers2(UserEmployer _userEmployer)
    {
        groupEmployers.employers.Add(_userEmployer);
        DataHolder.groupEmployers = groupEmployers;
        DataHolder.instance.WriteNakamaSaveEmployerList(email);
        //DataHolder.instance.WriteNakamaSaveEmployerList2("s@hotmail.com");
    }

    public void AddListManagers(UserManager _userManager)
    {
        groupManagers.managers.Add(_userManager);
        DataHolder.groupManagers = groupManagers;
        DataHolder.instance.WriteNakamaSaveManagerList(email);
    }
}
