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

    [Header("clases to use")]
    public static LogInScene instance;



    

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

    public void AuthenticationUsers()
    {
        if(DataHolder.superUserclass != null)
        {
            if (DataHolder.superUserclass.tutorialFirst == false)
            {
                SceneManager.LoadScene("TutorialScene");
            }
            else if (DataHolder.superUserclass.tutorialFirst == true)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
        else if (DataHolder.userEmployee != null)
        {
            if (DataHolder.userEmployee.tutorialFirst == false)
            {
                SceneManager.LoadScene("TutorialScene");
            }
            else if (DataHolder.userEmployee.tutorialFirst == true)
            {
                SceneManager.LoadScene("MainScene"); 
            }
        }
        else if (DataHolder.userManager != null)
        {
            if (DataHolder.userManager.tutorialFirst == false)
            {
                SceneManager.LoadScene("TutorialScene");
            }
            else if (DataHolder.userManager.tutorialFirst == true)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    public void ShowLoadingPanel()
    {
        panelLoading.SetActive(true);
        panelCredentials.SetActive(false);
    }

    public void HideLoadingPanel()
    {
        panelLoading.SetActive(false);
    }



}
