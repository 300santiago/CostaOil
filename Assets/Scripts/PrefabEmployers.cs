using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrefabEmployers : MonoBehaviour
{
    [SerializeField] TMP_Text nameEmployer;


    public void AssignEmployers(string name)
    {
        nameEmployer.text = name;
    }



}
