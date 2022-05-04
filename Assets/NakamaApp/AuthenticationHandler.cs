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
    private int _case;



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
        session = await client.AuthenticateEmailAsync(email, password, "", false);
        DataHolder.instance.session = session;
        DataHolder.instance.email = email;
        ReadMyStorageObjects(email);
        ReadMyStorageObjectsUserEmployer(email);
        ReadMyStorageObjectsUserManager(email);
        StartCoroutine(delay());
    }

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
             passwordSuperUser = LogInScene.instance.passwordCredentials.text,
             tutorialFirst = false,
        };
         usersPermissions = new UsersPermissions
        {
            createUserEmployer = true,
            createUserManager = true,
            createNewSucursals = true,
            createNewWorkCar = true,
        };

        sucursals = new Sucursals
        {
            nameSucursal = ""
        };

        listSucursals = new ListSucursals
        {

        };
        groupEmployers = new GroupEmployers
        {

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
        DataHolder.superUserclass = superUserClass;
        DataHolder.usersPermissions = usersPermissions;
        DataHolder.sucursals = sucursals;
        DataHolder.listSucursals = listSucursals;
        DataHolder.groupEmployers = groupEmployers;
    }


    //lectura de datos super user
     public async void ReadMyStorageObjects(string email)
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
                Key = "Sucursal",
                UserId = session.UserId
            },
            new StorageObjectId
            {
                Collection = email,
                Key = "ListSucursals",
                UserId = session.UserId
            },
            new StorageObjectId
            {
                Collection = email,
                Key = "ListEmployers",
                UserId = session.UserId
            },

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
                Debug.Log("Lectura datos 1 super user");
                DataHolder.superUserclass= JsonUtility.FromJson<SuperUserClass>(userData[i].Value);

            }
            else if (userData[i].Key == "UserPermissions")
            {
                Debug.Log("Lectura datos 2 super user");
                usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
                DataHolder.usersPermissions = usersPermissions;
            }
             else if (userData[i].Key == "Sucursal")
            {
                Debug.Log("Lectura datos 3 super user");
                sucursals = JsonUtility.FromJson<Sucursals>(userData[i].Value);
                DataHolder.sucursals = sucursals;
            }

            else if (userData[i].Key == "ListSucursals")
            {
                Debug.Log("Lectura datos 4 super user");
                listSucursals = JsonUtility.FromJson<ListSucursals>(userData[i].Value);
                DataHolder.listSucursals = listSucursals;
            }
            else if (userData[i].Key == "ListEmployers")
            {
                Debug.Log("Lectura datos 5 super user");
                groupEmployers = JsonUtility.FromJson<GroupEmployers>(userData[i].Value);
                DataHolder.groupEmployers = groupEmployers;
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
    //Usuario Employer:
    public async void SignUpNewEmployers(string _emailEmployer, string _password, string _nameEmployer)
    {
        session = await client.AuthenticateEmailAsync(_emailEmployer,_password, _nameEmployer, true);
        StorageObjectsEmployer(_emailEmployer, _password, _nameEmployer);
    }




    public async void StorageObjectsEmployer(string email,  string _passwordEmployer, string _nameEmployer)
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
        AddListEmployers(userEmployer);
    }



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
            print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                Debug.Log("lectura de datos employer 1");
                userEmployer = JsonUtility.FromJson<UserEmployer>(userData[i].Value);
                DataHolder.userEmployer = userEmployer;
            }
            else if (userData[i].Key == "UserPermissions")
            {
                Debug.Log("lectura de datos employer 2");
                usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
                DataHolder.usersPermissions = usersPermissions;
            }
        }
        _case = 2;
        Debug.Log(_case);
    }



    public async void SignUpNewManager(string _emailManager, string _passwordManager, string _nameManager)
    {
        session = await client.AuthenticateEmailAsync(_emailManager, _passwordManager, _nameManager, true);
        StorageObjectsManager(_emailManager, _passwordManager, _nameManager);
    }


    public async void StorageObjectsManager(string email,  string _password, string _name)
    {
        Debug.Log("escritura de datos");
        userManager = new UserManager
        {
            nameManager = _name,
            emailManager = email,
            passwordManager = _password,
            sucursalManager = "Houston",
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
        Debug.Log(DataHolder.userManager.emailManager);
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
            print(o.Key + "/" + o.Value);
        }

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                Debug.Log("lectura de datos manager 1");
                userManager = JsonUtility.FromJson<UserManager>(userData[i].Value);
                DataHolder.userManager = userManager;
            }
            else if (userData[i].Key == "UserPermissions")
            {
                Debug.Log("lectura de datos manager 2");
                usersPermissions = JsonUtility.FromJson<UsersPermissions>(userData[i].Value);
                DataHolder.userManager = userManager;
            }
        }
        _case = 3;
        Debug.Log(_case);
    }


    public void AddSucursal(Sucursals _sucursals)
    {
        Debug.Log("add sucursal");
        listSucursals.teamSucursals.Add(_sucursals);
        DataHolder.listSucursals = listSucursals;
        DataHolder.instance.WriteNakamaSuperUser(email);
    }

    public void AddListEmployers(UserEmployer _userEmployer)
    {
        Debug.Log("add employer List");
        groupEmployers.employers.Add(_userEmployer);
        DataHolder.groupEmployers = groupEmployers;
        DataHolder.instance.WriteNakamaSaveEmployerList(email);
    }
}
