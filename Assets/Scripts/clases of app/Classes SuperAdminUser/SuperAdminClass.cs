using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SuperAdminClass
{
    public string nameAdmin = "SuperAdmin";
    public int adminCounter;
    public int employeeCounter;
    public List<Sucursals> listSucursals = new List<Sucursals>();
    public List<UserManager> listAdmins = new List<UserManager>();
    public List<UserEmployee> listEmployees = new List<UserEmployee>();
    public SuperAdminClass() {}
}

