using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PrefabSucursals : MonoBehaviour
{
    [SerializeField] TMP_Text nameSucursal;
    public Sucursals thisSucursal = new Sucursals();

    private void Start()
    {

    }

    public void AssignSucursal(Sucursals sucursals)
    {
        thisSucursal = sucursals;
        nameSucursal.text = thisSucursal.nameSucursal;
    }
}
