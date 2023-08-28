﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable<T>  {

    public string Name { get; set; }
    public T Value { get; set; }


    public Variable(string Name, T Value) 
    {
        this.Name = Name;
        this.Value = Value;
    }

}

public struct Vector2Wrapper 
{ 
    public Vector2 Value { get; set; }

    public Vector2Wrapper(Vector2 value)
    {
        Value = value;
    }
}

public struct Vector3Wrapper 
{ 
    public Vector3 Value { get; set; }

    public Vector3Wrapper(Vector3 value) 
    {
        Value = value;
    }
}
