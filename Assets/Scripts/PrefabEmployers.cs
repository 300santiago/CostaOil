using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Nakama;
using System.Linq;

public class PrefabEmployers : MonoBehaviour
{
    [SerializeField] TMP_Text nameEmployer;
    [SerializeField] TMP_Text textInfoName;
    [SerializeField] TMP_Text textInfoEmail;
    [SerializeField] TMP_Text textSucursal;

    [SerializeField] GameObject panelInfo;
    [SerializeField] GameObject panelEmployers;

    private string _nameEmployer;
    private string emailEmployer;
    private string sucursalEmployer;
    private string idEmployer;
    


    public BasicUserEmployee thisBasicUserEmployer = new BasicUserEmployee();
    

    public void AssignEmployers(BasicUserEmployee userEmployer)
    {
        thisBasicUserEmployer = userEmployer;
        thisBasicUserEmployer.emailEmployee = userEmployer.emailEmployee;
        thisBasicUserEmployer.idEmployee = userEmployer.idEmployee;
    }

    public async void ReadUserEmployer()
    {
         IApiReadStorageObjectId[] objectsId = 
        {
            new StorageObjectId
            {
                Collection = thisBasicUserEmployer.emailEmployee,
                Key = "UserInfo",
                UserId = thisBasicUserEmployer.idEmployee,
            },
        };
        IApiStorageObjects objects = await DataHolder.instance.client.ReadStorageObjectsAsync(DataHolder.instance.session, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                DataHolder.userEmployer = JsonUtility.FromJson<UserEmployee>(userData[i].Value);
                Debug.Log(DataHolder.userEmployer.nameEmployee);
                textInfoEmail.text = DataHolder.userEmployer.emailEmployee;
                textInfoName.text = DataHolder.userEmployer.nameEmployee;
            }
        }
    }
}
