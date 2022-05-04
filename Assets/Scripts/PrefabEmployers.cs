using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrefabEmployers : MonoBehaviour
{
    [SerializeField] TMP_Text nameEmployer;
    //public UserEmployer thisUserEmployer = new UserEmployer();


    public void AssignEmployers(UserEmployer userEmployer)
    {
        nameEmployer.text = userEmployer.nameEmployer;
    }
}
