using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ConsultEmployers : MonoBehaviour
{
    public GameObject prefabEmployers;
    public RectTransform contentEmployers;
    public UsersPermissions usersPermissions;
    public UsersPermissions usersPermissions2;
    [Header("titles")]
    [SerializeField] TMP_Text titleText;
    private int count = 0;   

    void Start()
    {
        //condition for instantiate prefabs of prefabs
        if (DataHolder.usersPermissions.createNewSucursals == true && DataHolder.usersPermissions.createNewWorkCar == true && DataHolder.usersPermissions.createUserEmployer == true && DataHolder.usersPermissions.createUserManager == true)
        {
            titleText.text = $"Employers for alls sucursals: {DataHolder.superAdminClass.listEmployers.Count}";
            foreach (BasicUserEmployer p in DataHolder.superAdminClass.listEmployers)
            {
                GameObject tempPrefabEmp = Instantiate(prefabEmployers, contentEmployers);
                tempPrefabEmp.GetComponent<PrefabEmployers>().AssignEmployers(p);
                //tempPrefabEmp.GetComponent<PrefabEmployers>().ReadInfoEmployers(p);
            }

        }

        else if (DataHolder.usersPermissions.createNewSucursals == false && DataHolder.usersPermissions.createNewWorkCar == false && DataHolder.usersPermissions.createUserEmployer == true && DataHolder.usersPermissions.createUserManager == false)
        {
            foreach (BasicUserEmployer p in DataHolder.superAdminClass.listEmployers)
            {
                if (p.sucursalEmployer == DataHolder.basicUserManager.sucursalManager)
                {
                    count = count + 1;
                    GameObject tempPrefabEmp = Instantiate(prefabEmployers, contentEmployers);
                    tempPrefabEmp.GetComponent<PrefabEmployers>().AssignEmployers(p);

                }
            }
            
             titleText.text = $"Employers for this sucursal {DataHolder.basicUserManager.sucursalManager} : {count}";
        }
        
    }

    public void SceneLoader()
    {
        if (DataHolder.usersPermissions.createNewSucursals == true && DataHolder.usersPermissions.createNewWorkCar == true && DataHolder.usersPermissions.createUserEmployer == true && DataHolder.usersPermissions.createUserManager == true)
        {
            usersPermissions = new UsersPermissions
            {
                createUserEmployer = true,
                createUserManager = true,
                createNewSucursals = true,
                createNewWorkCar = true,
            };
        DataHolder.usersPermissions = usersPermissions;
        SceneManager.LoadScene("ManagerScene");
        }

        else if (DataHolder.usersPermissions.createNewSucursals == false && DataHolder.usersPermissions.createNewWorkCar == false && DataHolder.usersPermissions.createUserEmployer == true && DataHolder.usersPermissions.createUserManager == false)
        {
            usersPermissions2 = new UsersPermissions
            {
                createUserEmployer = true,
                createUserManager = false,
                createNewSucursals = false,
                createNewWorkCar = false,
            };
        DataHolder.usersPermissions = usersPermissions2;
        SceneManager.LoadScene("ManagerScene");
        }
    }
}
