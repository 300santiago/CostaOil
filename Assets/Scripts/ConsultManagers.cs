using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ConsultManagers : MonoBehaviour
{
    public GameObject prefabManagers;
    public RectTransform contentManagers;
    //public UsersPermissions usersPermissions;

    private void Start()
    {
        foreach (UserManager p in DataHolder.groupManagers.managers)
        {
            print(p.nameManager);
            GameObject tempPrefabManag = Instantiate(prefabManagers, contentManagers);
            tempPrefabManag.GetComponent<PrefabManagers>().AssignManagers(p);
        }    
            
    }

    public void LoadScene()
    {
        UsersPermissions usersPermissions = new UsersPermissions
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
