using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SuperAdminClass
{
    public string nameAdmin = "SuperAdmin";
    public List<Sucursals> listSucursals = new List<Sucursals>();
    public List<BasicUserManager> listAdmins = new List<BasicUserManager>();
    public SuperAdminClass() {}
}

