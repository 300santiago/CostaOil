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

    public ListSucursals listSucursals = new ListSucursals();

    private void Start()
    {

    }


    public void AssignSucursal(string name)
    {
        nameSucursal.text = name;
    }
    
}
