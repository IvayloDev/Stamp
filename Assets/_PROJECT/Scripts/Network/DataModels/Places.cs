using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Places
{
    public int id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public object deletedAt { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string logo { get; set; }
    public string banner { get; set; }
    public string location { get; set; }
    public double stampPrice { get; set; }
}
