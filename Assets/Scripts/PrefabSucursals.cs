using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PrefabSucursals : MonoBehaviour
{
    [SerializeField] TMP_Text nameSucursal;
    //public ListSucursals listSucursals = new ListSucursals();
    public Sucursals thisSucursals = new Sucursals();

    public ListSucursals listSucursals;

    private void Start()
    {

    }


    public void AssignSucursal(ListSucursals sucursalslist, Sucursals _sucursals)
    {
        listSucursals = sucursalslist;
        nameSucursal.text = _sucursals.nameSucursal;
    }

    public void ShowSucursals()
    {

    }

}
