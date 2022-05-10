using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrefabEmployers : MonoBehaviour
{
    [SerializeField] TMP_Text nameEmployer;
    [SerializeField] TMP_Text textInfoName;
    [SerializeField] TMP_Text textInfoEmail;
    [SerializeField] TMP_Text textSucursal;

    [SerializeField] GameObject panelInfo;
    [SerializeField] GameObject panelEmployers;

    private string _nameEmployer;
    private string emailEmployer;
    private string sucursalEmployer;

    public void AssignEmployers(BasicUserEmployer userEmployer)
    {
        nameEmployer.text = userEmployer.nameEmployer;
        _nameEmployer = userEmployer.nameEmployer;
        //emailEmployer = userEmployer.emailEmployer;
        sucursalEmployer = userEmployer.sucursalEmployer;
    }

    public void ShowInfoEmployer()
    {
        panelInfo.SetActive(true);
        panelEmployers.SetActive(false);
        //textInfoName.text = $"Name Employer: {user}";
        textInfoName.text = $"Name Employer: {_nameEmployer}";
        //textInfoEmail.text = $"Email Employer : {emailEmployer}";
        textSucursal.text = $"Sucursal Employer : {sucursalEmployer}";
    }

    public void ReadInfoEmployers()
    {
        panelInfo.SetActive(true);  
        panelEmployers.SetActive(false);
        AuthenticationHandler.instance.ReadMyStorageObjectsUserEmployer(DataHolder.instance.email);
        
    }
}
