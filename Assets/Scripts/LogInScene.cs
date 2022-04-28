using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LogInScene : MonoBehaviour
{
    [Header("GameObjects panels")]
    [SerializeField] GameObject panelCredentials;
    [SerializeField] GameObject panelLogin;
    [SerializeField] GameObject panelCheck;
    //[SerializeField] GameObject panelFirstSesionSP;
    [SerializeField] GameObject panelLoading; 

    [Header("Variables InputField")]
    public TMP_InputField emailCredentials; //ingreso de email
    public TMP_InputField passwordCredentials;
    //[SerializeField] TMP_Text textFirstSesionSP;
    public static LogInScene instance;


    private string superUserEmail = "superuser@hotmail.com";
    private string superUserPassword = "12345678";

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;   
    }
    void Start()
    {
        panelCredentials.SetActive(false);
        panelLogin.SetActive(true);
        panelLoading.SetActive(false);
    }

    void Update()
    {
        
    }

    public void AuthenticationUsers()
    {
        if (DataHolder.superUserclass.tutorialFirst == false)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else if (DataHolder.superUserclass.tutorialFirst == true)
        {
            SceneManager.LoadScene("ManagerScene");
        }
        else if (DataHolder.userEmployer.tutorialFirst == false)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else if (DataHolder.userEmployer.tutorialFirst == true)
        {
            SceneManager.LoadScene("ManagerScene"); 
        }
    }

    public void ShowLoadingPanel()
    {
        panelLoading.SetActive(true);
        panelCredentials.SetActive(false);
    }

    public void DontShowLoadingPanel()
    {
        panelLoading.SetActive(false);
    }



   /* public void LoginUserFirst()
    {
        SceneManager.LoadScene("ManagerScene");
        Debug.Log(PlayerPrefs.GetInt("firstSesionSP"));
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("firstSesionSP", 0);
        Debug.Log(PlayerPrefs.GetInt("firstSesionSP"));  
    }
    
    IEnumerator ShowLoadingPanel()
    {
        panelLoading.SetActive(true);
        yield return new WaitForSeconds(1f);
        panelLoading.SetActive(false);
        SceneManager.LoadScene("TutorialScene");
    }

     IEnumerator ShowLoadingPanel2()
    {
        panelLoading.SetActive(true);
        yield return new WaitForSeconds(1f);
        panelLoading.SetActive(false);
        SceneManager.LoadScene("ManagerScene");
    }*/
}
