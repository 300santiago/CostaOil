using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UserEmployee
{
    public string nameEmployee;
    public string positionEmployee;
    public string emailEmployee;
    public string sucursalEmployee;
    public string idEmployee;
    public string password;
    public bool tutorialFirst;

    public List<CarJobsClass> listCars = new List<CarJobsClass>();

    public UserEmployee() {}
}
