using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable<T>  {

    /*
    bool isInt = false;
    bool isFloat = false;
    bool isBool = false;


    public int valueInt { get; set; }
    public float valueFloat { get; set; }
    public bool valueBool { get; set; }


    public string nameVariable{ get; set; }

    public Variable(string nameVariable, int valueInt )
    {
        this.nameVariable = nameVariable;
        this.isInt = true;
        this.valueInt = valueInt;

    }

    public Variable(string nameVariable, float valueFloat)
    {
        this.nameVariable = nameVariable;
        this.isFloat = true;
        this.valueFloat = valueFloat;

    }

    public Variable(string nameVariable, bool Valuebool)
    {
        this.nameVariable = nameVariable;
        this.isBool = true;

        this.valueBool = Valuebool;

    }


    public bool isINT(){
        if (isInt)
        {
            return true;
        }
        return false;
    }

    public bool isFLOAT()
    {
        if (isFloat)
        {
            return true;
        }
        return false;
    }

    public bool isBOOL()
    {
        if (isBool)
        {
            return true;
        }
        return false;
    }*/
    // THIS IS THE NEW CODE FOR MM2.0

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
