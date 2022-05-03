using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsultEmployers : MonoBehaviour
{
    public GameObject prefabEmployers;
    public RectTransform contentEmployers;
   

    void Start()
    {
         foreach (UserEmployer p in DataHolder.groupEmployers.employers)
        {
            print(p.nameEmployer);
            GameObject tempPrefabEmp = Instantiate(prefabEmployers, contentEmployers);
            tempPrefabEmp.GetComponent<PrefabEmployers>().AssignEmployers(p.nameEmployer);
        }    
        
    }

    public void SceneLoader()
    {
        SceneManager.LoadScene("ManagerScene");
    }
}
