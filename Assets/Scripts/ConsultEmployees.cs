using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultEmployees : MonoBehaviour
{
    public static ConsultEmployees instance;
    [Header("Prefab")]
    public GameObject prefabEmployee;
    public Transform employeeContainer;
    

    private void Awake() {
        instance = this;
    }

}
