using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrefabManagers : MonoBehaviour
{
     [SerializeField] TMP_Text nameManager;
     [SerializeField] TMP_Text textInfoName;
    [SerializeField] TMP_Text textInfoEmail;
    [SerializeField] TMP_Text textSucursal;

    [SerializeField] GameObject panelInfo;
    [SerializeField] GameObject panelManagers;

    private string _nameManager;
    private string emailManager;
    private string sucursalManager;

      public void AssignManagers(BasicUserManager _userManager)
    {
        nameManager.text = _userManager.nameManager;
        _nameManager = _userManager.nameManager;
        //emailManager = _userManager.emailManager;
        sucursalManager = _userManager.sucursalManager;
    }

    public void ShowInfoEmployer()
    {
        panelInfo.SetActive(true);
        panelManagers.SetActive(false);
        textInfoName.text = $"Name Manager: {_nameManager}";
        textInfoEmail.text = $"Email Manager : {emailManager}";
        textSucursal.text = $"Sucursal Manager : {sucursalManager}";
    }
}
