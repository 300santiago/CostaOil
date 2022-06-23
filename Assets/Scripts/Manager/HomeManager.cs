using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class HomeManager : MonoBehaviour
{
    public static HomeManager instance;
    [Header("GameObjects panels")]
    public GameObject confirmScreen;
    public GameObject loadingPanel;
    public GameObject[] panels;

    [Header("Text InputField")]
    [SerializeField] TMP_InputField nameEmployee;
    [SerializeField] TMP_InputField emailEmployee;

    [Header("Text in scene")]
    [SerializeField] TMP_Text sucursalManager;
    [Header("New account info")]
    public TMP_Text newEmail;
    public TMP_Text newPassword;
    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        if (DataHolder.usersPermissions.workerKind == WorkerKind.admin)
        {
            sucursalManager.text = $"Sucrusal: {DataHolder.userManager.sucursalManager}";  
        } 
    }
    public void CheckNewEmployee()
    {
        if(nameEmployee.text.Length < 2)
        {
            ManagerScene.instance.ShowError("Employee name must have more than 2 characters.");
            return;
        }
        if (emailEmployee.text.Length < 2)
        {
            ManagerScene.instance.ShowError("Please enter a valid email.");
            return;
        }
        if (!emailEmployee.text.Contains("@"))
        {
            ManagerScene.instance.ShowError("Please enter a valid email.");
            return;
        }
        confirmScreen.SetActive(true);
    }
    public void CleanDates()
    {
        nameEmployee.text = string.Empty;
        emailEmployee.text = string.Empty;
    }

    public void CreateNewEmployee()
    {
        loadingPanel.SetActive(true);
        string charactersPassword = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        int longcharacters = charactersPassword.Length;
        char letter;
        int lengPassword = 8;
        string passwordEmployee = string.Empty;
        for (int i = 0; i < lengPassword; i++)
        {
            letter = charactersPassword[Random.Range(0, longcharacters)];
            passwordEmployee += letter.ToString();
        }
        string _nameEmployee = nameEmployee.text;
        string _emailEmployee = emailEmployee.text;
        string _sucursal = DataHolder.userManager.sucursalManager;
        AuthenticationHandler.instance.SignUpNewEmployee(_emailEmployee, passwordEmployee, _nameEmployee, _sucursal);
    }
    public void HideLoadingPanel(string _email, string _password)
    {
        loadingPanel.SetActive(false);
        foreach(GameObject g in panels) {g.SetActive(false);}
        panels[0].SetActive(true); //MainPanel
        newEmail.text = _email;
        newPassword.text = $"Password: {_password}";
        panels[3].SetActive(true); //Show new account panel
    }
}
