using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ManagerScene : MonoBehaviour
{
     [Header("Variables Texts")]
    [SerializeField] TMP_Text textTitleManager;

    [Header("Variables InputField")]
    public TMP_InputField textNameEmployer;
    [SerializeField] TMP_InputField textPositionEmployer;
    [SerializeField] TMP_InputField textNameSucursal;
    public TMP_InputField textEmailEmployer;

    [Header("GameObjects")]
    [SerializeField] GameObject panelHome;
    [SerializeField] GameObject panelSavePlayers;
    [SerializeField] GameObject backgroundNormal;
    public GameObject generalEmployer;

    [Header("RectTransform")]
    public RectTransform contentEmployers;
    public RectTransform contentSucursals;


    [Header("InputFields New Manager")]

    [SerializeField] TMP_InputField nameManager;
    [SerializeField] TMP_InputField emailManager;


    [Header("Employer")]
    [SerializeField] GameObject panelHomeEmployer;
    [SerializeField] TMP_Text titleEmployer;

    [Header("Manager")]
    [SerializeField] GameObject panelHomeManager;
    [SerializeField] TMP_Text titleManager;

    [Header("prefabs")]
    public GameObject prefabSucurals;

    //private string nameManager;
    private int counterfirst = 0;
    public string passwordaleatory;
    public string passwordaleatoryManager;
    public Sucursals sucursals;

   


    [Header("classes in scene")]

    public GroupEmployers groupEmployers = new GroupEmployers();
    public ListSucursals listSucursals;
   
    public static ManagerScene instance;

    private void Awake()
    {
        instance = this;
        panelHome.SetActive(false);
        panelHomeEmployer.SetActive(false);
        backgroundNormal.SetActive(false);
        panelHomeManager.SetActive(false);
      
    }
    void Start()
    { 
        Debug.Log("carga escena");
        if (DataHolder.usersPermissions.createNewSucursals == false && DataHolder.usersPermissions.createNewWorkCar == true && DataHolder.usersPermissions.createUserEmployer == false && DataHolder.usersPermissions.createUserManager == false)
        {
            panelHomeEmployer.SetActive(true);
            titleEmployer.text = $"Welcome Employer: {DataHolder.userEmployer.nameEmployer}";

        }

         else if (DataHolder.usersPermissions.createNewSucursals == true && DataHolder.usersPermissions.createNewWorkCar == true && DataHolder.usersPermissions.createUserEmployer == true && DataHolder.usersPermissions.createUserManager == true)
        {
            panelHome.SetActive(true);
            textTitleManager.text = $"Welcome Super User: {DataHolder.superUserclass.nameSuperUser}";
        }

        else if (DataHolder.usersPermissions.createNewSucursals == false && DataHolder.usersPermissions.createNewWorkCar == false && DataHolder.usersPermissions.createUserEmployer == true && DataHolder.usersPermissions.createUserManager == false)
        {
            panelHomeManager.SetActive(true);
            titleManager.text = $"Welcome Manager: {DataHolder.userManager.nameManager}";
        }
        backgroundNormal.SetActive(true);
    }


    public void SceneLoader(string sceneLoader)
    {
        SceneManager.LoadScene(sceneLoader);
    }

    public void AddEmployer()
    {
        panelSavePlayers.SetActive(true);
    }

    public void AddSucursalList()
    {
        sucursals = new Sucursals
        {
            nameSucursal = textNameSucursal.text
        };
        DataHolder.sucursals = sucursals;
        AuthenticationHandler.instance.AddSucursal(sucursals);
        textNameSucursal.text = string.Empty;
    }

    public void printListEmployers()
    {
        /*foreach (Sucursals p in DataHolder.listSucursals.teamSucursals)
        {
            print(p.nameSucursal);
            GameObject tempPrefabSuc = Instantiate(prefabSucurals, contentSucursals);
            tempPrefabSuc.GetComponent<PrefabSucursals>().AssignSucursal(p.nameSucursal);
        }*/
        /*if (DataHolder.groupEmployers.employers.Count < 0)
        {
            Debug.Log("no tiene trabajadores agregados");
        }
        else
        {
                foreach (UserEmployer p in DataHolder.groupEmployers.employers)
            {
                    print(p.nameEmployer);
            }
        }*/

    }
    public void CancelEmployer()
    {
        textNameEmployer.text = string.Empty;
        textPositionEmployer.text = string.Empty;
        textEmailEmployer.text = string.Empty;
    }

    public void AddNewEmployer()
    {

        string _emailEmployer;
        string _passwordEmployer;
        string _nameEmployer;

        string charactersPassword = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        int longcharacters = charactersPassword.Length;
        char letter;
        int lengPassword = 10;
        passwordaleatory = string.Empty;


        for  (int i = 0; i<lengPassword; i++)
        {
            letter = charactersPassword[Random.Range(0,longcharacters)];
            passwordaleatory += letter.ToString();
        }

        _emailEmployer = textEmailEmployer.text;
        _passwordEmployer = passwordaleatory;
        _nameEmployer = textNameEmployer.text;
        AuthenticationHandler.instance.SignUpNewEmployers(_emailEmployer, _passwordEmployer, _nameEmployer);
           
    }

    public void AddNewManager()
    {
        string _emailManager;
        string _passwordManager;
        string _nameManager;

        string charactersPassword = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        int longcharacters = charactersPassword.Length;
        char letter;
        int lengPassword = 10;
        passwordaleatoryManager = string.Empty;


        for  (int i = 0; i<lengPassword; i++)
        {
            letter = charactersPassword[Random.Range(0,longcharacters)];
            passwordaleatoryManager += letter.ToString();
        }

        _emailManager = emailManager.text;
        _passwordManager = passwordaleatoryManager;
        _nameManager = nameManager.text;

        AuthenticationHandler.instance.SignUpNewManager( _emailManager, _passwordManager,  _nameManager); 
    }

    public void AddListDropDown()
    {
        ScriptDropDown.instance.DropDownAddList();
    }
}
