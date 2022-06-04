using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PrefabSucursals : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TMP_Text nameSucursalPrefb;
    [SerializeField] TMP_Text nameSucursals;
    [Header("Classes")]
    public Sucursals thisSucursal = new Sucursals();
    public void AssignSucursal(Sucursals sucursals)
    {
        thisSucursal = sucursals;
        nameSucursalPrefb.text = $"{thisSucursal.nameSucursal}";
    }
    public void DeleteSucursal()
    {
        for (int i = 0; i < DataHolder.superAdminClass.listSucursals.Count; i++)
        {
            if(DataHolder.superAdminClass.listSucursals[i].nameSucursal == thisSucursal.nameSucursal)
            {
                ManagerScene.instance.LoadingOn();
                DataHolder.superAdminClass.listSucursals.RemoveAt(i);
                DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
            }
        }
    }
}
