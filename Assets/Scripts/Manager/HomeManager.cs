using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class HomeManager : MonoBehaviour
{
    [Header("GameObjects panels")]

    [Header("Text InputField")]
    [SerializeField] TMP_InputField nameEmployer;
    [SerializeField] TMP_InputField emailEmployer;

    [Header("Text in scene")]
    [SerializeField] TMP_Text sucursalManager;

    [Header("classes to use")]
    public GameObject gameObject;

    private void Start()
    {
        sucursalManager.text = $"You are the admin of {DataHolder.userManager.sucursalManager} sucursal";   
    }

    public void CleanDates()
    {
        nameEmployer.text = string.Empty;
        emailEmployer.text = string.Empty;
    }

    public void CreateNewEmployer()
    {
        string _nameEmployer = nameEmployer.text;
        string _emailEmployer = emailEmployer.text;
        string _password = "12345678";
        string _sucursal = DataHolder.userManager.sucursalManager;
        AuthenticationHandler.instance.SignUpNewEmployers2(_emailEmployer, _password, _nameEmployer, _sucursal);
    }

}
