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
  
    [SerializeField] GameObject panelLoading; 

    [Header("Variables InputField")]
    public TMP_InputField emailCredentials;
    public TMP_InputField passwordCredentials;
    public static LogInScene instance;



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


        /*
        else if (DataHolder.userManager.tutorialFirst == true)
        {
            SceneManager.LoadScene("ManagerScene");
        }
        else if (DataHolder.userManager.tutorialFirst == false)
        {
            SceneManager.LoadScene("TutorialScene");
        }*/
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



}
