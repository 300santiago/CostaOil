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
    


    public BasicUserEmployer thisBasicUserEmployer = new BasicUserEmployer();
    

    public void AssignEmployers(BasicUserEmployer userEmployer)
    {
        thisBasicUserEmployer = userEmployer;
        thisBasicUserEmployer.emailEmployer = userEmployer.emailEmployer;
        thisBasicUserEmployer.idEmployer = userEmployer.idEmployer;
    }

    public async void ReadUserEmployer()
    {
         IApiReadStorageObjectId[] objectsId = 
        {
            new StorageObjectId
            {
                Collection = thisBasicUserEmployer.emailEmployer,
                Key = "UserInfo",
                UserId = thisBasicUserEmployer.idEmployer,
            },
        };
        IApiStorageObjects objects = await DataHolder.instance.client.ReadStorageObjectsAsync(DataHolder.instance.session, objectsId);
        IApiStorageObject[] userData = objects.Objects.ToArray();

        for (int i = 0; i < userData.Length; i++)
        {
            if (userData[i].Key == "UserInfo")
            {
                DataHolder.userEmployer = JsonUtility.FromJson<UserEmployer>(userData[i].Value);
                Debug.Log(DataHolder.userEmployer.nameEmployer);
                textInfoEmail.text = DataHolder.userEmployer.emailEmployer;
                textInfoName.text = DataHolder.userEmployer.nameEmployer;
            }
        }
    }
}
