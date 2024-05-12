using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Data 
{
    public List<DataFromDB> data;

    public Data()
    {
        data = new List<DataFromDB>();
    }
}
