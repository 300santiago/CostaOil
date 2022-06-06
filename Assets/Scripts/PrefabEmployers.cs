using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Nakama;
using System.Linq;

public class PrefabEmployee : MonoBehaviour
{
    [SerializeField] TMP_Text nameEmployee;
    [SerializeField] TMP_Text positionEmployer;
    [SerializeField] TMP_Text numberJobsEmployer;
    private string _nameEmployer;
    private string emailEmployer;
    private string sucursalEmployer;
    private string idEmployer;
    public UserEmployee userEmployee = new UserEmployee();
    

    public void AssignEmployee(UserEmployee _userEmployee)
    {
        userEmployee = _userEmployee;
        nameEmployee.text = userEmployee.nameEmployee;
        positionEmployer.text = userEmployee.positionEmployee;
        numberJobsEmployer.text = userEmployee.listCars.Count.ToString(); 
    }





    // public async void ReadUserEmployer()
    // {
    //      IApiReadStorageObjectId[] objectsId = 
    //     {
    //         new StorageObjectId
    //         {
    //             Collection = userEmployee.emailEmployee,
    //             Key = "UserInfo",
    //             UserId = userEmployee.idEmployee,
    //         },
    //     };
    //     IApiStorageObjects objects = await DataHolder.instance.client.ReadStorageObjectsAsync(DataHolder.instance.session, objectsId);
    //     IApiStorageObject[] userData = objects.Objects.ToArray();

    //     for (int i = 0; i < userData.Length; i++)
    //     {
    //         if (userData[i].Key == "UserInfo")
    //         {
    //             DataHolder.userEmployer = JsonUtility.FromJson<UserEmployee>(userData[i].Value);
    //             Debug.Log(DataHolder.userEmployer.nameEmployee);
    //         }
    //     }
    // }

}
