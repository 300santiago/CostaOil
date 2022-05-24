using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GeneralBody
{
    public int Count;
    public string Message, SearchCriteria;
    public List<CarItems> Results = new List<CarItems>();
    
    public GeneralBody(int _count, string _message, string _searchCriteria, List<CarItems> _results)
    {
        Count = _count;
        Message = _message;
        SearchCriteria = _searchCriteria;
        Results = _results;
    }
}
