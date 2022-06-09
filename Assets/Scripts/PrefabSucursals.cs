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
    public void OnDeleteSucursalBtn()
    {
        PanelManagerMainScene.instance.LoadPanelIndex(9, 0);
        ManagerScene.instance.StoreSucursalToDelete(this);
    }
    public void DeleteSucursal()
    {
        for (int i = 0; i < DataHolder.superAdminClass.listSucursals.Count; i++)
        {
            if(DataHolder.superAdminClass.listSucursals[i].nameSucursal == thisSucursal.nameSucursal)
            {
                ManagerScene.instance.LoadingOn();

                //Delete the sucursal on the admin profile
                for (int j = 0; j < DataHolder.superAdminClass.listAdmins.Count; j++)
                {
                    if (DataHolder.superAdminClass.listSucursals[i].sucursalManager.nameManager == DataHolder.superAdminClass.listAdmins[j].nameManager)
                    {
                        DataHolder.superAdminClass.listAdmins[j].sucursalManager = string.Empty;
                        break;
                    }
                }
                //Delete the sucursal from the database
                DataHolder.superAdminClass.listSucursals.RemoveAt(i);
                DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
                break;
            }
        }
        ConsultSucursals.instance.ClearSucursalGO();
        ConsultSucursals.instance.LoadInfo();
        PanelManagerMainScene.instance.LoadPanelIndex(6, 0); //Load Consult sucursals panel
    }
    public void DetailSucursal()
    {
        int thisIndex = -1;
        for (int i = 0; i < DataHolder.superAdminClass.listSucursals.Count; i++)
        {
            if (DataHolder.superAdminClass.listSucursals[i].nameSucursal == thisSucursal.nameSucursal)
            {
                thisIndex = i;
                break;
            }
        }
        ConsultSucursals.instance.LoadDetailSpecificBranch(thisIndex, thisSucursal.sucursalManager);
        PanelManagerMainScene.instance.LoadPanelIndex(7, 0);
    }
}
