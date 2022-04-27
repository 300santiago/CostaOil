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


    private string nameManager;
    private int counterfirst = 0;
    public string passwordaleatory;
    //public UserEmployer userEmployer;

    [Header("classes in scene")]

    public GroupEmployers groupEmployers = new GroupEmployers();
    public static ManagerScene instance;

    private void Awake()
    {
        instance = this;
        panelHome.SetActive(false);
        backgroundNormal.SetActive(false);
        //background1Sesion.SetActive(false);
        //backgroundExplication.SetActive(false);
    }
    void Start()
    { 
        panelHome.SetActive(true);
        backgroundNormal.SetActive(true);
        textTitleManager.text = $"Welcome: {DataHolder.superUserclass.nameSuperUser}";
        Debug.Log(DataHolder.superUserclass.nameSuperUser);
    }


    public void SceneLoader(string sceneLoader)
    {
        SceneManager.LoadScene(sceneLoader);
    }

    public void AddEmployer()
    {
        panelSavePlayers.SetActive(true);
    }

    public void AddEmployerList()
    {
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


        UserEmployer _userEmployer = new UserEmployer
        {
            nameEmployer = textNameEmployer.text,
            positionEmployer = textPositionEmployer.text,
            passwordEmployer = passwordaleatory,
            emailEmployer = textEmailEmployer.text,
        };
        groupEmployers.employers.Add(_userEmployer);
        foreach (UserEmployer p in groupEmployers.employers)
        {
            print(p.nameEmployer);
        }

        GameObject _tempGo = Instantiate(generalEmployer,contentEmployers);
        _tempGo.GetComponent<GeneralUserEmployer>().AssignValueEmployer(_userEmployer);
    }


    /*public void AddEmployersNakama()
    {
        HanlderAuthentication.instance.SignUp2();
    }*/

    public void CancelEmployer()
    {
        textNameEmployer.text = string.Empty;
        textPositionEmployer.text = string.Empty;
        textEmailEmployer.text = string.Empty;
    }
}
