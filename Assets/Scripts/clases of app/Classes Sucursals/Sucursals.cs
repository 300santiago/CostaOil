using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Sucursals 
{
    public string nameSucursal;
    public BasicUserManager sucursalManager = new BasicUserManager();
    public List<BasicUserEmployee> listEmployee = new List<BasicUserEmployee>();

    public Sucursals() {}
}

