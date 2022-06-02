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

    [Header("Detailed Info")]
    public TMP_Text sucursalName;
    public TMP_Dropdown adminList;
    public GameObject noneText;
    private void Awake()
    {
        instance = this;
        panelSucursals.SetActive(true);
        panelOptionsSucursal.SetActive(false);
    }

    private void Start()
    {
        //Invoke(nameof(LoadInfo), 1f);
    }
    public void LoadInfo()
    {
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
    public void LoadDetailBranch(int _index)
    {
        List<Sucursals> list = DataHolder.superAdminClass.listSucursals;
        sucursalName.text = $" Watching {list[_index].nameSucursal} Sucursal";
        adminList.ClearOptions();
        if (DataHolder.superAdminClass.listAdmins.Count < 1)
        {
            adminList.gameObject.SetActive(false);
            noneText.SetActive(true);
        }
        else
        {
            foreach (BasicUserManager b in DataHolder.superAdminClass.listAdmins)
            {
                adminList.options.Add(new TMP_Dropdown.OptionData() { text = b.nameManager });
            }
            DropdownItemsSelected(list, _index);
            adminList.onValueChanged.AddListener(delegate { DropdownItemsSelected(list, _index); });
        }

    }
    void DropdownItemsSelected(List<Sucursals> list, int _index)
    {
        int adminIndex = adminList.value;
        list[_index].sucursalManager = DataHolder.superAdminClass.listAdmins[adminIndex];
    }
}
