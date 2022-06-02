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

    [Header("GameObjects panels")]
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
    //[SerializeField] TMP_Text titleEmployer;

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
    public UserEmployee userEmployer;
    [Header("classes in scene")]
    //public GroupEmployers groupEmployers = new GroupEmployers();
    public ListSucursals listSucursals;
    [Header("Loading")]
    public GameObject loadingPanel;
    public static ManagerScene instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (DataHolder.usersPermissions.workerKind == WorkerKind.employee)
        {
            panelHomeEmployer.SetActive(true);
        }
        else if (DataHolder.usersPermissions.workerKind == WorkerKind.superUser)
        {
            PanelManagerMainScene.instance.LoadSuperUserPanel();
            PanelManagerMainScene.instance.LoadMainPanelSuper();
            textTitleManager.text = $"Welcome Super User: {DataHolder.superUserclass.nameSuperUser}";
        }
        else if (DataHolder.usersPermissions.workerKind == WorkerKind.admin)
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

    public async void AddSucursalList()
    {
        LoadingOn();
        sucursals = new Sucursals
        {
            nameSucursal = textNameSucursal.text
        };
        try
        {
            DataHolder.superAdminClass.listSucursals.Add(sucursals);
            DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
            textNameSucursal.text = string.Empty;
            LoadingOff();
            ConsultSucursals.instance.LoadDetailBranch(DataHolder.superAdminClass.listSucursals.Count-1);
        }
        catch
        {
            print("error");
            throw;
        }
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


        for (int i = 0; i < lengPassword; i++)
        {
            letter = charactersPassword[Random.Range(0, longcharacters)];
            passwordaleatory += letter.ToString();
        }

        _emailEmployer = textEmailEmployer.text;
        //_passwordEmployer = passwordaleatory;
        _passwordEmployer = "12345678";
        _nameEmployer = textNameEmployer.text;

        AuthenticationHandler.instance.SignUpNewEmployers(_emailEmployer, _passwordEmployer, _nameEmployer, ScriptDropDown.instance.nameSucursal);
    }

    public void ClearInputFieldsEmployer()
    {
        textEmailEmployer.text = string.Empty;
        textNameEmployer.text = string.Empty;
    }
    public void ClearInputFieldsManager()
    {
        emailManager.text = string.Empty;
        nameManager.text = string.Empty;
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
        for (int i = 0; i < lengPassword; i++)
        {
            letter = charactersPassword[Random.Range(0, longcharacters)];
            passwordaleatoryManager += letter.ToString();
        }

        _emailManager = emailManager.text;
        //_passwordManager = passwordaleatoryManager;
        _passwordManager = "12345678";
        _nameManager = nameManager.text;

        AuthenticationHandler.instance.SignUpNewManager(_emailManager, _passwordManager, _nameManager, ScriptDropDown.instance.nameSucursal);
    }

    public void AddListDropDown()
    {
        ScriptDropDown.instance.DropDownAddList();
    }
    public void LoadingOn() { loadingPanel.SetActive(true); }
    public void LoadingOff() { loadingPanel.SetActive(false); }
}