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
    

    public void DropDownAddList()
    {
       var dropDown = transform.GetComponent<TMP_Dropdown>();
        dropDown.options.Clear();
        foreach (Sucursals p in DataHolder.listSucursals.teamSucursals)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = p.nameSucursal });
        }
        DropdownItemsSelected(dropDown);
        dropDown.onValueChanged.AddListener(delegate { DropdownItemsSelected (dropDown);}); 
    }
    

    void DropdownItemsSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        nameSucursal = dropdown.options[index].text; 
    }
}
