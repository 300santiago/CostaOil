using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScriptDropDown : MonoBehaviour
{
    public string nameSucursal;
    public static ScriptDropDown instance;

    private void Awake()
    {
        instance = this;

    }


    public void DropDownAddListSucursals(int _index = 0, bool _interactable = true)
    {
        var dropDown = transform.GetComponent<TMP_Dropdown>();
        dropDown.interactable = _interactable;
        dropDown.options.Clear();
        foreach (Sucursals p in DataHolder.superAdminClass.listSucursals)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = p.nameSucursal });
        }
        dropDown.value = _index;
        DropdownItemsSelected(dropDown);
        dropDown.onValueChanged.AddListener(delegate { DropdownItemsSelected(dropDown); });
    }


    void DropdownItemsSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        nameSucursal = dropdown.options[index].text;
    }
}
