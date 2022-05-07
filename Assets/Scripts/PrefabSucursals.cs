using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PrefabSucursals : MonoBehaviour
{
    [Header("Panels Gameobjects")]
    [SerializeField] GameObject panelShowInfo;
    [SerializeField] GameObject panelSucursals;

    [Header("Texts")]
    [SerializeField] TMP_Text nameSucursalPrefb;
    [SerializeField] TMP_Text nameSucursals;
    [Header("Classes")]
    public Sucursals thisSucursal = new Sucursals();
   

    private void Start()
    {
        panelShowInfo.SetActive(false);
    }

    public void AssignSucursal(Sucursals sucursals)
    {
        thisSucursal = sucursals;
        nameSucursalPrefb.text = $"{thisSucursal.nameSucursal}";
    }
    public void ShowInfoSucursals()
    {
        panelShowInfo.SetActive(true);
        panelSucursals.SetActive(false);
        nameSucursals.text = $"Name sucursal: {thisSucursal.nameSucursal}";
        
    }
}
