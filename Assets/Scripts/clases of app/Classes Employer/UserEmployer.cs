using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UserEmployer
{
    public string nameEmployer;
    public string positionEmployer;
    public string emailEmployer;
    public string sucursalEmployer;
    public bool tutorialFirst;

    public List<CarJobsClass> listCars = new List<CarJobsClass>();

    public UserEmployer()
    {

    }
}
