using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ConsultSucursals : MonoBehaviour
{
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
    [Header("Employees List")]
    public GameObject employeePrefab;
    public Transform employeesHolder;
    public GameObject noEmployeesPrefab;
    private void Awake()
    {
        instance = this;
    }
    public void LoadInfo()
    {
        ClearSucursalGO();
        titlePrincipal.text = $"SUCURSALS CREATED: {DataHolder.superAdminClass.listSucursals.Count}";

        foreach (Sucursals p in DataHolder.superAdminClass.listSucursals)
        {
            GameObject tempPrefabSuc = Instantiate(prefabSucurals, contentSucursals);
            tempPrefabSuc.GetComponent<PrefabSucursals>().AssignSucursal(p);
        }
        ManagerScene.instance.LoadingOff();
    }
    //Load Information after creation
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
        //Load Admins
        else
        {
            foreach (UserManager b in DataHolder.superAdminClass.listAdmins)
            {
                adminList.options.Add(new TMP_Dropdown.OptionData() { text = b.nameManager });
            }
            DropdownItemsSelected(list, _index);
            adminList.onValueChanged.AddListener(delegate { DropdownItemsSelected(list, _index); });
        }   
        //Load Employees
        ClearEmployeesGO();
        if (DataHolder.superAdminClass.listSucursals[_index].listEmployee.Count > 0)
        {
            //Instantiate employees
            foreach (UserEmployee u in DataHolder.superAdminClass.listSucursals[_index].listEmployee)
            {
                GameObject employee = Instantiate(employeePrefab, employeesHolder);
                employee.GetComponent<PrefabEmployee>().AssignEmployee(u);
            }
        }
        else //No employees
        {
            Instantiate(noEmployeesPrefab, employeesHolder);
        }
    }
    //Load information after consult
    public void LoadDetailSpecificBranch(int _index, UserManager _manager)
    {
        List<Sucursals> list = DataHolder.superAdminClass.listSucursals;
        sucursalName.text = $" Watching {list[_index].nameSucursal} Sucursal";
        adminList.ClearOptions();
        if (DataHolder.superAdminClass.listAdmins.Count < 1)
        {
            adminList.gameObject.SetActive(false);
            noneText.SetActive(true);
        }
		//Load Admins
		else
		{
			print(adminList.value);
			int i = 0;
			foreach (UserManager b in DataHolder.superAdminClass.listAdmins)
            {
				//Add the Admins to the TMPDROPDOWN
                adminList.options.Add(new TMP_Dropdown.OptionData() { text = b.nameManager });
				if(_manager.idManager == b.idManager)
				{
					print(i);
					print(_manager.idManager + " " + b.idManager);
					adminList.value = i;
					print(adminList.value);
				}
				i++;
            }
			if(_manager.idManager == string.Empty)
			{
            	adminList.options.Add(new TMP_Dropdown.OptionData() {text = "Not Assigned"});
				adminList.value = adminList.options.Count-1;
			}
            adminList.onValueChanged.AddListener(delegate { DropdownItemsSelected(list, _index); });
		}
		//Load Employees
        ClearEmployeesGO();
        if (DataHolder.superAdminClass.listSucursals[_index].listEmployee.Count > 0)
        {
            //Instantiate employees
            foreach (UserEmployee u in DataHolder.superAdminClass.listSucursals[_index].listEmployee)
            {
                GameObject employee = Instantiate(employeePrefab, employeesHolder);
                employee.GetComponent<PrefabEmployee>().AssignEmployee(u);
            }
        }
        else //No employees
        {
            Instantiate(noEmployeesPrefab, employeesHolder);
        }
    }
    void DropdownItemsSelected(List<Sucursals> list, int _index)
    {
        int adminIndex = adminList.value;
		if(adminIndex >=  DataHolder.superAdminClass.listAdmins.Count) {return;}
		print("writing");
        list[_index].sucursalManager = DataHolder.superAdminClass.listAdmins[adminIndex];
		DataHolder.instance.WriteNakamaAdmUser(AuthenticationHandler.instance.superUserAdminEmail);
    }
    public void ClearSucursalGO()
    {
        foreach (Transform child in contentSucursals)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ClearEmployeesGO()
    {
        foreach (Transform child in employeesHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
