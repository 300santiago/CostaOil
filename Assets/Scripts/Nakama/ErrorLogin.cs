using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ErrorLogin : MonoBehaviour
{
    public static ErrorLogin instance;
    public TMP_Text errorText;
    public GameObject errorGO, loadingPanel, panelLogin;
    void Awake()
    {
        instance = this;
    }
    public void ShowError(string _error)
    {
        errorGO.SetActive(true);
        errorText.text = _error;
    }
    public void HideError()
    {
        errorText.text = string.Empty;
        errorGO.SetActive(false);
        loadingPanel.SetActive(false);
        panelLogin.SetActive(true);
        AuthenticationHandler.instance.ClearFields();
    }
}
