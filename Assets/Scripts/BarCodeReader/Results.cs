using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Results
{
    public List<CarItems> carItems = new List<CarItems>();
    public Results(List<CarItems> _carItems) 
    {
        carItems = _carItems;
    }
}
