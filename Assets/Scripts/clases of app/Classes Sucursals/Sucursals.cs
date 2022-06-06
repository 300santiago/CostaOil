using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Sucursals 
{
    public string nameSucursal;
    public UserManager sucursalManager = new UserManager();
    public List<UserEmployee> listEmployee = new List<UserEmployee>();

    public Sucursals() {}
}

