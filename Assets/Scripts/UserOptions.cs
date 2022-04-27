using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UserOptions : MonoBehaviour
{
    public TMP_Text textUserName;
    public TMP_InputField userName;
    public GameObject [] panelUserOptions;

    void Start()
    {
        panelUserOptions[0].SetActive(false);
        //textUserName.text = $"Profile name: {ManagerScene.instance.nameManager}";

    }

    public void PanelProfileSettings()
    {
        panelUserOptions[0].SetActive(true);
    }

    public void ChangesData(string newName)
    {
        newName = userName.text;
        textUserName.text = $"Profile name: {newName}";
        //ManagerScene.instance.nameManager = newName;


    }
    public void SceneLoader2()
    {
        SceneManager.LoadScene("ManagerScene");    
    }
}
