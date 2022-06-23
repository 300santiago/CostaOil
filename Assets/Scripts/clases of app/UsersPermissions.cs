using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum WorkerKind {superUser, admin, employee}
[Serializable]
public class UsersPermissions
{
    public bool createUserEmployee;
    public bool createUserManager;
    public bool createNewSucursals;
    public bool createNewWorkCar;
    public WorkerKind workerKind;

}
