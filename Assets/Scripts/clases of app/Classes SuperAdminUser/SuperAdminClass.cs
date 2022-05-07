using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SuperAdminClass
{
    public string nameAdmin;
    public List<Sucursals> listSucursals = new List<Sucursals>();
    public List<UserEmployer> listEmployers = new List<UserEmployer>();
    public List<UserManager> listManagers = new List<UserManager>();
    public SuperAdminClass()
    {

    }
}

