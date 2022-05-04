using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsultEmployers : MonoBehaviour
{
    public GameObject prefabEmployers;
    public RectTransform contentEmployers;
    public UsersPermissions usersPermissions;
   

    void Start()
    {
         foreach (UserEmployer p in DataHolder.groupEmployers.employers)
        {
            print(p.nameEmployer);
            GameObject tempPrefabEmp = Instantiate(prefabEmployers, contentEmployers);
            tempPrefabEmp.GetComponent<PrefabEmployers>().AssignEmployers(p);
        }    
        
    }

    public void SceneLoader()
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
}
