using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ConsultSucursals : MonoBehaviour
{
    [Header("GameObjects panels")]
    public GameObject panelSucursals;
    public GameObject panelOptionsSucursal;

    [Header("titles")]
    [SerializeField] TMP_Text titlePrincipal;
    [SerializeField] TMP_Text titleNameSucursal;

    [Header("classes")]

    public GameObject prefabSucurals;
    public RectTransform contentSucursals;

    public static ConsultSucursals instance;

    private void Awake()
    {
        instance = this;
        panelSucursals.SetActive(true);
        panelOptionsSucursal.SetActive(false);
    }

    private void Start()
    {
        Invoke(nameof(LoadInfo), 2f);
    }
    public void LoadInfo()
    {
        print(DataHolder.superAdminClass.listSucursals.Count);
        titlePrincipal.text = $"SUCURSALS CREATED: {DataHolder.superAdminClass.listSucursals.Count}";

        foreach (Sucursals p in DataHolder.superAdminClass.listSucursals)
        {
            GameObject tempPrefabSuc = Instantiate(prefabSucurals, contentSucursals);
            tempPrefabSuc.GetComponent<PrefabSucursals>().AssignSucursal(p);
        }
    }
    public void SceneLoader()
    {
        SceneManager.LoadScene("ManagerScene");
    }
}
