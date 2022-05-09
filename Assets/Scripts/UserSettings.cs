using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UserSettings : MonoBehaviour
{

    [Header("gameobjects panels")]
    [SerializeField] GameObject panelprofileSettings;
    [SerializeField] GameObject panelGeneralUserOptions;

    [Header("Variables InputFields")]
    public TMP_InputField nameSuperUser;

    public static UserSettings instance;
    private void Start()
    {
        instance = this;
        panelGeneralUserOptions.SetActive(true);
        panelprofileSettings.SetActive(false);
    }

    public void SceneLoader(string sceneLoader)
    {
        SceneManager.LoadScene(sceneLoader);
    }
}
