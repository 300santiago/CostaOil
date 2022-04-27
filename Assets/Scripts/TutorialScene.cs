using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TutorialScene : MonoBehaviour
{
    [SerializeField] GameObject [] panelsTutorial;
    [SerializeField] GameObject [] imagesTutorialSP;
    [SerializeField] GameObject [] numbersTutorialSP;
    [SerializeField] GameObject inputNameSPpanel;
    [SerializeField] GameObject inputNameSucursal;
    [SerializeField] GameObject imageLogo;
    [SerializeField] GameObject panelLoading;
    [SerializeField] GameObject panelSPTutorial;
    [SerializeField] TMP_Text textTutorial;
    [SerializeField] TMP_Text textTitle;
    [SerializeField] TMP_InputField textNameSuperUser;
    [SerializeField] TMP_InputField textNameSucursal;
    

    private int counterTutorial = 0;
    private string inputName_SP;
    private string inputName_Sucursal;
    public static SuperUserClass _superUserClass;

    private void Awake() 
    {
        for (int i =0; i<panelsTutorial.Length; i++)
        {
            panelsTutorial[i].SetActive(false);
        }
        for (int i =0; i<panelsTutorial.Length; i++)
        {
            imagesTutorialSP[i].SetActive(false);
        }
        for (int i =0; i<panelsTutorial.Length; i++)
        {
            numbersTutorialSP[i].SetActive(false);
        }
    }

    private void Start()
    {
        inputNameSPpanel.SetActive(false);
        imageLogo.SetActive(false);
 
        if (PlayerPrefs.GetInt("firstSesionSP") == 0)
        {
            panelsTutorial[0].SetActive(true);
            imagesTutorialSP[0].SetActive(true);
            textTutorial.text = $"This application allows you to organize your workspace. Here is a quick explanation of the first steps";
            textTitle.text = $"FIRST STEPS WITH THE PLATFORM SUPER USER";
        }
    }

    public void ButtonNext(int counter)
    {
        counter = counterTutorial;

        switch(counter)
        {
            case 0:
            imagesTutorialSP[1].SetActive(true);
            textTutorial.text = $"With this application you can create new branch offices with the following button";
            counterTutorial++;
            break;

            case 1:
            imagesTutorialSP[2].SetActive(true);
            textTutorial.text = $"with this application you can create the administrators of the work sites";
            counterTutorial++;
            break;

            case 2:
            imagesTutorialSP[3].SetActive(true);
            textTutorial.text = $"with this application you can create new mechanical employers";
            counterTutorial++;
            break;

            case 3:
            for (int i=0; i<4; i++)
            {
                imagesTutorialSP[i].SetActive(false);
            }
            imagesTutorialSP[4].SetActive(true);
            textTutorial.text = $"Additionally, you can consult branches, workers, administrators, view transfers and observe the general movements of the company";
            counterTutorial++;
            break;

            case 4:
            for (int i=0; i<imagesTutorialSP.Length; i++)
            {
                imagesTutorialSP[i].SetActive(false);
            }
            textTitle.text = $"Environment settings of Super User";
            textTutorial.text = $"The first process is to enter your name";
            inputNameSPpanel.SetActive(true);

            numbersTutorialSP[0].SetActive(true);
            counterTutorial++;
            break;

            case 5:
            textTutorial.text = $"The second process is to enter the name of your first sucursal";
            DataHolder.superUserclass.nameSuperUser = inputName_SP;
            textNameSuperUser.text = string.Empty;
            inputNameSPpanel.SetActive(false);
            inputNameSucursal.SetActive(true);
            numbersTutorialSP[1].SetActive(true);
            counterTutorial++;
            break;

            case 6:
            textTitle.text = $"Finally";
            textTutorial.text = $"Great Job welcome to the Oil aplication";
            textNameSucursal.text = string.Empty;
            inputNameSucursal.SetActive(false);
            imageLogo.SetActive(true);
            for (int i = 0; i<numbersTutorialSP.Length; i++)
            {
                numbersTutorialSP[i].SetActive(false);
            }
            DataHolder.instance.WriteBoolean();
            counterTutorial++;
            break;

            case 7:
            StartCoroutine(ShowLoadingPanel(0));
            break;
        }
    }

    public void AsignNameSuperUser(string nameSP)
    {
        inputName_SP = nameSP;
        Debug.Log(inputName_SP);
        PlayerPrefs.SetString("nameSuperUser",inputName_SP);
    }
    public void AsignNameSucursal(string nameSC)
    {
        inputName_Sucursal = nameSC;
        Debug.Log(inputName_Sucursal);
        PlayerPrefs.SetString("nameSucursal", inputName_Sucursal);
    }


      IEnumerator ShowLoadingPanel(int _Scene)
    {

        panelLoading.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        
        switch(_Scene)
        {
            case 0:
                SceneManager.LoadScene("ManagerScene");
                panelLoading.SetActive(false);
            break;
        }
    }
}
