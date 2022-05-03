using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ConsultSucursals : MonoBehaviour
{
    [Header("titles")]
    [SerializeField] TMP_Text titlePrincipal;

    public GameObject prefabSucurals;
    public RectTransform contentSucursals;

    private void Start()
    {
        titlePrincipal.text = $"SUCURSALS CREATED: {DataHolder.listSucursals.teamSucursals.Count}";
        foreach (Sucursals p in DataHolder.listSucursals.teamSucursals)
        {
            print(p.nameSucursal);
            GameObject tempPrefabSuc = Instantiate(prefabSucurals, contentSucursals);
            tempPrefabSuc.GetComponent<PrefabSucursals>().AssignSucursal(p.nameSucursal);
        }    
    }
    public void SceneLoader()
    {
        SceneManager.LoadScene("ManagerScene");
    }
}
