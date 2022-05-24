using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CarItems
{
    public string Value, ValueId, Variable;
    public int VariableId;
    public CarItems(string _value, string _valueId, string _variable, int _variableId) 
    {
        Value = _value;
        ValueId = _valueId;
        Variable = _variable;
        VariableId = _variableId;
    }
}
