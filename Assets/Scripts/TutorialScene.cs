using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TutorialScene : MonoBehaviour
{
    [Header("GameObjects panels")]
    [SerializeField] GameObject[] panelsTutorial;

    [Header("SuperUserComponents")]
    [SerializeField] GameObject[] imagesTutorialSP;
    [SerializeField] GameObject[] numbersTutorialSP;
    [SerializeField] GameObject inputNameSPpanel;
    [SerializeField] GameObject inputNameSucursal;
    [SerializeField] GameObject panelSPTutorial;

    [SerializeField] GameObject imageLogo;
    [SerializeField] GameObject panelLoading;

    [SerializeField] TMP_Text textTutorial;
    [SerializeField] TMP_Text textTitle;
    [SerializeField] TMP_InputField textNameSuperUser;
    [SerializeField] TMP_InputField textNameSucursal;

    [Header("EmployerComponents")]
    [SerializeField] TMP_Text explicationEmployerText;
    [SerializeField] TMP_Text titleTextEmployer;
    [SerializeField] GameObject[] iconsEmployer;
    [SerializeField] GameObject numbersEmployer;
    [SerializeField] GameObject passwordEmployer;
    [SerializeField] TMP_InputField inputPasswordEmployer;

    [Header("ManagerComponents")]
    [SerializeField] TMP_Text explicationManagerText;
    [SerializeField] TMP_Text titleTextManager;
    [SerializeField] GameObject[] iconsManager;
    [SerializeField] GameObject numbersManager;
    [SerializeField] GameObject passwordManager;
    [SerializeField] TMP_InputField inputPasswordManager;

    //variables to use in the script
    private int counterTutorial = 0;
    private int counterEmployer = 0;
    private int counterManager = 0;
    private string inputName_SP;
    private string inputName_Sucursal;
    private string emailSuperUser;


    public static SuperUserClass _superUserClass;
    public static Sucursals _sucursals;
    public static ListSucursals listSucursals = new ListSucursals();
    public static TutorialScene instance;
    [Header("Error")]
    public GameObject errorGO;
    public TMP_Text errorTxt;

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < panelsTutorial.Length; i++)
        {
            panelsTutorial[i].SetActive(false);
        }
        for (int i = 0; i < panelsTutorial.Length; i++)
        {
            imagesTutorialSP[i].SetActive(false);
        }
        for (int i = 0; i < panelsTutorial.Length; i++)
        {
            numbersTutorialSP[i].SetActive(false);
        }
        passwordEmployer.SetActive(false);
        passwordManager.SetActive(false);
    }

    private void Start()
    {
        inputNameSPpanel.SetActive(false);
        imageLogo.SetActive(false);

        if (DataHolder.usersPermissions.workerKind == WorkerKind.employee)
        {
            //employer user
            Debug.Log("employer");
            panelsTutorial[2].SetActive(true);
            explicationEmployerText.text = $"Welcome {DataHolder.userEmployee.nameEmployee}, This application allows you to organize your workspace.";
        }

        else if (DataHolder.usersPermissions.workerKind == WorkerKind.superUser)
        {
            // super user
            Debug.Log("super User");
            panelsTutorial[0].SetActive(true);
            textTutorial.text = $"This application allows you to organize your workspace. Here is a quick explanation of the first steps";
            textTitle.text = $"FIRST STEPS ON THE SUPERUSER PLATFORM";
        }
        //manager user
        else if (DataHolder.usersPermissions.workerKind == WorkerKind.admin)
        {
            Debug.Log("manager user");
            panelsTutorial[1].SetActive(true);
            iconsManager[0].SetActive(true);
            explicationManagerText.text = $"Welcome {DataHolder.userManager.nameManager}, This application allows you to organize your workspace.";
        }
    }

    // tutorial super user
    public void ButtonNext(int counter)
    {
        counter = counterTutorial;

        switch (counter)
        {
            case 0:
                imagesTutorialSP[1].SetActive(true);
                textTutorial.text = $"You can create new branch offices with the following button";
                counterTutorial++;
                break;

            case 1:
                imagesTutorialSP[2].SetActive(true);
                textTutorial.text = $"You can create administrators for branches";
                counterTutorial++;
                break;

            case 2:
                imagesTutorialSP[3].SetActive(true);
                textTutorial.text = $"You can create new employees";
                counterTutorial++;
                break;

            case 3:
                for (int i = 0; i < 4; i++)
                {
                    imagesTutorialSP[i].SetActive(false);
                }
                imagesTutorialSP[4].SetActive(true);
                textTutorial.text = $"Additionally, you can consult branches, workers, administrators, view transfers and observe the general movements of the company";
                counterTutorial++;
                break;

            case 4:
                for (int i = 0; i < imagesTutorialSP.Length; i++)
                {
                    imagesTutorialSP[i].SetActive(false);
                }
                textTitle.text = $"SUPER USER SETTINGS";
                textTutorial.text = $"The first step is to enter your name";
                inputNameSPpanel.SetActive(true);

                numbersTutorialSP[0].SetActive(true);
                counterTutorial++;
                break;

            case 5:
                if (textNameSuperUser.text.Length < 1)
                {
                    errorTxt.text = "Please enter a valid username";
                    errorGO.SetActive(true);
                    return;
                }
                textTutorial.text = $"The second step is to enter the name of your first sucursal";
                DataHolder.superUserclass.nameSuperUser = textNameSuperUser.text;
                DataHolder.superUserclass.tutorialFirst = true;
                DataHolder.instance.WriteNakamaSuperUser(AuthenticationHandler.instance.email);
                textNameSuperUser.text = string.Empty;
                inputNameSPpanel.SetActive(false);
                inputNameSucursal.SetActive(true);
                numbersTutorialSP[1].SetActive(true);
                counterTutorial++;
                break;

            case 6:
                if (textNameSucursal.text.Length < 1)
                {
                    errorTxt.text = "Please enter a valid sucursal name";
                    errorGO.SetActive(true);
                    return;
                }
                for (int i = 0; i < DataHolder.superAdminClass.listSucursals.Count; i++)
                {
                    if (DataHolder.superAdminClass.listSucursals[i].nameSucursal == textNameSucursal.text)
                    {
                        errorTxt.text = "This sucursal name already exists";
                        errorGO.SetActive(true);
                        return;
                    }
                }
                textTitle.text = $"Finally";
                textTutorial.text = $"Great Job, welcome to the Oil aplication";
                imageLogo.SetActive(true);
                for (int i = 0; i < numbersTutorialSP.Length; i++)
                {
                    numbersTutorialSP[i].SetActive(false);
                }
                //add sucursal:
                _sucursals = new Sucursals
                {
                    nameSucursal = textNameSucursal.text,
                };
                // add sucursal in the list:
                DataHolder.superAdminClass.listSucursals.Add(_sucursals);
                DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
                inputNameSucursal.SetActive(false);
                counterTutorial++;
                break;

            case 7:
                textNameSucursal.text = string.Empty;
                StartCoroutine(ShowLoadingPanel(0));
                break;
        }
    }




    IEnumerator ShowLoadingPanel(int _Scene)
    {

        panelLoading.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        switch (_Scene)
        {
            case 0:
                SceneManager.LoadScene("ManagerScene");
                panelLoading.SetActive(false);
                break;
        }
    }

    //employers
    public void ButtonNextEmployer(int _case)
    {
        _case = counterEmployer;
        switch (_case)
        {
            case 0:
                iconsEmployer[0].SetActive(false);
                iconsEmployer[1].SetActive(true);
                explicationEmployerText.text = $"With this app you can add new jobs with this button";
                counterEmployer++;
                break;

            case 1:
                iconsEmployer[0].SetActive(false);
                iconsEmployer[1].SetActive(false);
                iconsEmployer[2].SetActive(true);
                explicationEmployerText.text = $"Additionally, you can organize your work and show the progress";
                counterEmployer++;
                break;

            case 2:
                for (int i = 0; i < iconsEmployer.Length; i++)
                {
                    iconsEmployer[i].SetActive(false);
                }
                numbersEmployer.SetActive(true);
                titleTextEmployer.text = "Enviroment settings of Employer User";
                explicationEmployerText.text = "The first step is change your password for a personal password";
                passwordEmployer.SetActive(true);
                counterEmployer++;
                break;

            case 3:
                //passwordEmployer.SetActive(false);
                numbersEmployer.SetActive(false);
                //DataHolder.userEmployer.passwordEmployer = inputPasswordEmployer.text;
                passwordEmployer.SetActive(false);
                titleTextEmployer.text = "Finally";
                explicationEmployerText.text = "Great Job Welcome to the Oil Aplication";
                //DataHolder.instance.ChangePasswordEmployer(emailEmployer);
                DataHolder.userEmployee.tutorialFirst = true;
                DataHolder.instance.WriteNakamaEmployerUser(AuthenticationHandler.instance.email);
                counterEmployer++;

                break;

            case 4:
                inputPasswordEmployer.text = string.Empty;
                SceneManager.LoadScene("ManagerScene");
                break;
        }
    }



    //Totorial managers
    public void ButtonNextManager(int _case)
    {
        _case = counterManager;

        switch (_case)
        {
            case 0:
                iconsManager[0].SetActive(false);
                iconsManager[1].SetActive(true);
                explicationManagerText.text = $"With this app you can add new workers with this button";
                counterManager++;
                break;

            case 1:
                iconsManager[0].SetActive(false);
                iconsManager[1].SetActive(false);
                iconsManager[2].SetActive(true);
                explicationManagerText.text = $"Additionally, you can organize your work and show the progress";
                counterManager++;
                break;

            case 2:
                for (int i = 0; i < iconsManager.Length; i++)
                {
                    iconsManager[i].SetActive(false);
                }
                numbersManager.SetActive(true);
                titleTextManager.text = "Enviroment settings of Manager User";
                explicationManagerText.text = "The first step is change your password for a personal password";
                passwordManager.SetActive(true);
                counterManager++;
                break;

            case 3:
                if (string.IsNullOrEmpty(inputPasswordManager.text))
                {
                    errorTxt.text = "Please enter a valid password";
                    errorGO.SetActive(true);
                    return;
                }
                else if (inputPasswordManager.text.Length < 6)
                {
                    errorTxt.text = "Password must be 6 or more characters";
                    errorGO.SetActive(true);
                    return;
                }
                numbersManager.SetActive(false);
                DataHolder.instance.ChangePassword(inputPasswordManager.text, WorkerKind.admin);
                // for (int i = 0; i < DataHolder.superAdminClass.listAdmins.Count; i++)
                // {
                //     if (DataHolder.superAdminClass.listAdmins[i].emailManager == DataHolder.userManager.emailManager)
                //     {
                //         DataHolder.superAdminClass.listAdmins[i].passwordAdmin = inputPasswordManager.text;
                //         DataHolder.superAdminClass.listAdmins[i].tutorialFirst = true;
                //         for (int j = 0; j < DataHolder.superAdminClass.listSucursals.Count; j++)
                //         {
                //             if (DataHolder.superAdminClass.listSucursals[j].nameSucursal == DataHolder.superAdminClass.listAdmins[i].sucursalManager)
                //             {
                //                 DataHolder.superAdminClass.listSucursals[j].sucursalManager.passwordAdmin = inputPasswordManager.text;
                //                 DataHolder.superAdminClass.listSucursals[j].sucursalManager.tutorialFirst = true;
                //             }
                //         }
                //     }
                // }
                // titleTextManager.text = "Finally";
                // explicationManagerText.text = "Great Job, Welcome to the Oil Aplication";
                // string emailManager;
                // emailManager = AuthenticationHandler.instance.email;
                // //DataHolder.instance.ChangePasswordManager(emailManager);
                // DataHolder.userManager.tutorialFirst = true;
                // DataHolder.instance.WriteNakamaManagerrUser(AuthenticationHandler.instance.email);
                // passwordManager.SetActive(false);
                // counterManager++;
                break;
            case 4:
                inputPasswordManager.text = string.Empty;
                SceneManager.LoadScene("ManagerScene");
                break;
        }
    }

    public void AfterChangePswAdmin(string _password)
    {
        print(DataHolder.userManager.emailManager);
        for (int i = 0; i < DataHolder.superAdminClass.listAdmins.Count; i++)
        {
            print(DataHolder.superAdminClass.listAdmins[i].emailManager);
            if (DataHolder.superAdminClass.listAdmins[i].emailManager == DataHolder.userManager.emailManager)
            {
                print(DataHolder.superAdminClass.listAdmins[i].passwordAdmin);
                DataHolder.superAdminClass.listAdmins[i].passwordAdmin = inputPasswordManager.text;
                DataHolder.superAdminClass.listAdmins[i].tutorialFirst = true;
                print(DataHolder.superAdminClass.listAdmins[i].passwordAdmin);
                for (int j = 0; j < DataHolder.superAdminClass.listSucursals.Count; j++)
                {
                    if (DataHolder.superAdminClass.listSucursals[j].nameSucursal == DataHolder.superAdminClass.listAdmins[i].sucursalManager)
                    {
                        DataHolder.superAdminClass.listSucursals[j].sucursalManager.passwordAdmin = inputPasswordManager.text;
                        DataHolder.superAdminClass.listSucursals[j].sucursalManager.tutorialFirst = true;
                    }
                }
            }
        }
        titleTextManager.text = "Finally";
        explicationManagerText.text = "Great Job, Welcome to the Oil Aplication";
        string emailManager;
        emailManager = AuthenticationHandler.instance.email;
        //DataHolder.instance.ChangePasswordManager(emailManager);
        DataHolder.userManager.tutorialFirst = true;
        DataHolder.userManager.passwordAdmin = _password;
        DataHolder.instance.WriteNakamaManagerrUser(AuthenticationHandler.instance.email);
        DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
        passwordManager.SetActive(false);
        counterManager++;
    }
    public void sucursals()
    {
        Debug.Log("sucursals");
    }
}
