using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComparisonType
{
    IsInRange,
    IsEqual,
    IsLessThan,
    IsGreaterThan,
    IsLessThanOrEqual,
    IsGreaterThanOrEqual,
    IsTrue, //0
    IsFalse //1
}

public class Conditions
{

    // NEW CODE august 2023

    

    ///In this moment is not possible to compare between different type of variables just int vs int float vs float etc.
    /// in case that this will be needed is possible to add this new cases...

    //Integer comparison methods
    #region Integer Comparisons
    public static bool IsInRange(Variable<int> variable, int minValue, int maxValue) 
    {
        return variable.Value >= minValue && variable.Value <= maxValue;
    }

    public static bool IsEqual(Variable<int> variable, int value) 
    {
        return variable.Value == value;
    }

    public static bool IsEqual(Variable<int> variable1, Variable<int> variable2)
    {
        return variable1.Value == variable2.Value;
    }

    public static bool IsLessThan(Variable<int> variable, int value)
    {
        return variable.Value < value;
    }

    public static bool IsLessThan(Variable<int> variable1, Variable<int> variable2)
    {
        return variable1.Value < variable2.Value;
    }

    public static bool IsGreaterThan(Variable<int> variable, int value)
    {
        return variable.Value > value;
    }

    public static bool IsGreaterThan(Variable<int> variable1, Variable<int> variable2)
    {
        return variable1.Value > variable2.Value;
    }

    public static bool IsLessThanOrEqual(Variable<int> variable, int value)
    {
        return variable.Value <= value;
    }

    public static bool IsLessThanOrEqual(Variable<int> variable1, Variable<int> variable2)
    {
        return variable1.Value <= variable2.Value;
    }

    public static bool IsGreaterThanOrEqual(Variable<int> variable, int value)
    {
        return variable.Value >= value;
    }

    public static bool IsGreaterThanOrEqual(Variable<int> variable1, Variable<int> variable2)
    {
        return variable1.Value >= variable2.Value;
    }
    #endregion

    //Floating-Point Comparisons
    #region Floating-Point Comparisons
    public static bool IsInRange(Variable<float> variable, float minValue, float maxValue)
    {
        return variable.Value >= minValue && variable.Value <= maxValue;
    }

    public static bool IsEqual(Variable<float> variable, float value)
    {
        return Mathf.Approximately (variable.Value, value);
    }

    public static bool IsEqual(Variable<float> variable1, Variable<float> variable2)
    {
        return Mathf.Approximately(variable1.Value, variable2.Value);
    }

    public static bool IsLessThan(Variable<float> variable, float value)
    {
        return variable.Value < value;
    }

    public static bool IsLessThan(Variable<float> variable1, Variable<float> variable2)
    {
        return variable1.Value < variable2.Value;
    }

    public static bool IsGreaterThan(Variable<float> variable, float value)
    {
        return variable.Value > value;
    }

    public static bool IsGreaterThan(Variable<float> variable1, Variable<float> variable2)
    {
        return variable1.Value > variable2.Value;
    }

    public static bool IsLessThanOrEqual(Variable<float> variable, float value)
    {
        return variable.Value <= value;
    }

    public static bool IsLessThanOrEqual(Variable<float> variable1, Variable<float> variable2)
    {
        return variable1.Value <= variable2.Value;
    }

    public static bool IsGreaterThanOrEqual(Variable<float> variable, float value)
    {
        return variable.Value >= value;
    }

    public static bool IsGreaterThanOrEqual(Variable<float> variable1, Variable<float> variable2)
    {
        return variable1.Value >= variable2.Value;
    }
    #endregion

    //Double comparison methods

    #region Double Comparisons
    public static bool IsInRange(Variable<double> variable, double minValue, double maxValue)
    {
        return variable.Value >= minValue && variable.Value <= maxValue;
    }

    public static bool IsEqual(Variable<double> variable, double value)
    {
        return Mathf.Abs((float)(variable.Value - value)) < Mathf.Epsilon;
    }

    public static bool IsEqual(Variable<double> variable1, Variable<double> variable2)
    {
        return Mathf.Abs((float)(variable1.Value - variable2.Value)) < Mathf.Epsilon;
    }

    public static bool IsLessThan(Variable<double> variable, double value)
    {
        return variable.Value < value;
    }

    public static bool IsLessThan(Variable<double> variable1, Variable<double> variable2)
    {
        return variable1.Value < variable2.Value;
    }

    public static bool IsGreaterThan(Variable<double> variable, double value)
    {
        return variable.Value > value;
    }

    public static bool IsGreaterThan(Variable<double> variable1, Variable<double> variable2)
    {
        return variable1.Value > variable2.Value;
    }

    public static bool IsLessThanOrEqual(Variable<double> variable, double value)
    {
        return variable.Value <= value;
    }

    public static bool IsLessThanOrEqual(Variable<double> variable1, Variable<double> variable2)
    {
        return variable1.Value <= variable2.Value;
    }

    public static bool IsGreaterThanOrEqual(Variable<double> variable, double value)
    {
        return variable.Value >= value;
    }

    public static bool IsGreaterThanOrEqual(Variable<double> variable1, Variable<double> variable2)
    {
        return variable1.Value >= variable2.Value;
    }
    #endregion

    //Bolean comparison methods
    #region Boolean Comparisons
    public static bool IsTrue(Variable<bool> variable) 
    {
        return variable.Value;
    }

    public static bool IsFalse(Variable<bool> variable)
    {
        return !variable.Value;
    }
    #endregion

    //Vector2 comparison methods 
    #region Vector2 comparisons
    public static bool IsEqual(Variable<Vector2> variable, Vector2 value)
    {
        return variable.Value == value;
    }

    public static bool IsEqual(Variable<Vector2> variable1, Variable<Vector2> variable2) //need to check...
    {
        return variable1.Value == variable2.Value;
    }

    public static bool IsLessThan(Variable<Vector2> variable, Vector2 value)
    {
        return variable.Value.x < value.x && variable.Value.y < value.y;
    }

    public static bool IsLessThan(Variable<Vector2> variable1, Variable<Vector2> variable2)
    {
        return variable1.Value.x < variable2.Value.x && variable1.Value.y < variable2.Value.y;
    }

    public static bool IsGreaterThan(Variable<Vector2> variable, Vector2 value)
    {
        return variable.Value.x > value.x && variable.Value.y > value.y;
    }

    public static bool IsGreaterThan(Variable<Vector2> variable1, Variable<Vector2> variable2)
    {
        return variable1.Value.x > variable2.Value.x && variable1.Value.y > variable2.Value.y;
    }

    public static bool IsLessThanOrEqual(Variable<Vector2> variable, Vector2 value)
    {
        return variable.Value.x <= value.x && variable.Value.y <= value.y;
    }

    public static bool IsLessThanOrEqual(Variable<Vector2> variable1, Variable<Vector2> variable2)
    {
        return variable1.Value.x <= variable2.Value.x && variable1.Value.y <= variable2.Value.y;
    }

    public static bool IsGreaterThanOrEqual(Variable<Vector2> variable, Vector2 value)
    {
        return variable.Value.x >= value.x && variable.Value.y >= value.y;
    }

    public static bool IsGreaterThanOrEqual(Variable<Vector2> variable1, Variable<Vector2> variable2)
    {
        return variable1.Value.x >= variable2.Value.x && variable1.Value.y >= variable2.Value.y;
    }
    #endregion

    // Vector3 comparison methods
    #region Vector3 Comparisons

    public static bool IsEqual(Variable<Vector3> variable, Vector3 value)
    {
        return variable.Value == value;
    }

    public static bool IsEqual(Variable<Vector3> variable1, Variable<Vector3> variable2)
    {
        return variable1.Value == variable2.Value;
    }

    public static bool IsLessThan(Variable<Vector3> variable, Vector3 value)
    {
        return variable.Value.x < value.x && variable.Value.y < value.y && variable.Value.z < value.z;
    }

    public static bool IsLessThan(Variable<Vector3> variable1, Variable<Vector3> variable2)
    {
        return variable1.Value.x < variable2.Value.x && variable1.Value.y < variable2.Value.y && variable1.Value.z < variable2.Value.z;
    }

    public static bool IsGreaterThan(Variable<Vector3> variable, Vector3 value)
    {
        return variable.Value.x > value.x && variable.Value.y > value.y && variable.Value.z > value.z;
    }

    public static bool IsGreaterThan(Variable<Vector3> variable1, Variable<Vector3> variable2)
    {
        return variable1.Value.x > variable2.Value.x && variable1.Value.y > variable2.Value.y && variable1.Value.z > variable2.Value.z;
    }

    public static bool IsLessThanOrEqual(Variable<Vector3> variable, Vector3 value)
    {
        return variable.Value.x <= value.x && variable.Value.y <= value.y && variable.Value.z <= value.z;
    }

    public static bool IsLessThanOrEqual(Variable<Vector3> variable1, Variable<Vector3> variable2)
    {
        return variable1.Value.x <= variable2.Value.x && variable1.Value.y <= variable2.Value.y && variable1.Value.z <= variable2.Value.z;
    }

    public static bool IsGreaterThanOrEqual(Variable<Vector3> variable, Vector3 value)
    {
        return variable.Value.x >= value.x && variable.Value.y >= value.y && variable.Value.z >= value.z;
    }

    public static bool IsGreaterThanOrEqual(Variable<Vector3> variable1, Variable<Vector3> variable2)
    {
        return variable1.Value.x >= variable2.Value.x && variable1.Value.y >= variable2.Value.y && variable1.Value.z >= variable2.Value.z;
    }

    #endregion
    // TRansform and Vector comparison methods
    // THIS WILL BE CHANGED ALL FOR VECTOR3 SO...
    #region Transform and Vector Comparisons

    public static bool IsPositionEqual(Variable<Transform> variable, Transform transform)
    {
        return variable.Value.position == transform.position;
    }

    public static bool IsPositionEqual(Variable<Transform> variable1, Variable<Transform> variable2)
    {
        return variable1.Value.position == variable2.Value.position;
    }

    public static bool IsPositionInRange(Variable<Transform> variable, Vector3 minPosition, Vector3 maxPosition)
    {
        Vector3 position = variable.Value.position;
        return position.x >= minPosition.x && position.x <= maxPosition.x &&
               position.y >= minPosition.y && position.y <= maxPosition.y &&
               position.z >= minPosition.z && position.z <= maxPosition.z;
    }

    public static bool IsPositionInRange(Variable<Transform> variable, Vector2 minPosition, Vector2 maxPosition)
    {
        Vector3 position = variable.Value.position;
        return position.x >= minPosition.x && position.x <= maxPosition.x &&
               position.y >= minPosition.y && position.y <= maxPosition.y;
    }

    public static bool IsPositionLeftOf(Variable<Transform> variable, Transform referenceTransform)
    {
        return variable.Value.position.x < referenceTransform.position.x;
    }

    public static bool IsPositionRightOf(Variable<Transform> variable, Transform referenceTransform)
    {
        return variable.Value.position.x > referenceTransform.position.x;
    }

    public static bool IsPositionAbove(Variable<Transform> variable, Transform referenceTransform)
    {
        return variable.Value.position.y > referenceTransform.position.y;
    }

    public static bool IsPositionBelow(Variable<Transform> variable, Transform referenceTransform)
    {
        return variable.Value.position.y < referenceTransform.position.y;
    }

    #endregion

    void ExampleUsage()
    {
        // Creating variables for different types
        Variable<int> intVar1 = new Variable<int>("intVar1", 10);
        Variable<int> intVar2 = new Variable<int>("intVar2", 15);

        Variable<float> floatVar1 = new Variable<float>("floatVar1", 3.14f);
        Variable<float> floatVar2 = new Variable<float>("floatVar2", 2.71f);

        Variable<double> doubleVar1 = new Variable<double>("doubleVar1", 123.456);
        Variable<double> doubleVar2 = new Variable<double>("doubleVar2", 789.012);

        Variable<bool> boolVar1 = new Variable<bool>("boolVar1", true);
        Variable<bool> boolVar2 = new Variable<bool>("boolVar2", false);

        Variable<Vector2> vector2Var1 = new Variable<Vector2>("vector2Var1", new Vector2(2.0f, 3.0f));
        Variable<Vector2> vector2Var2 = new Variable<Vector2>("vector2Var2", new Vector2(1.0f, 1.5f));

        Variable<Vector3> vector3Var1 = new Variable<Vector3>("vector3Var1", new Vector3(1.0f, 2.0f, 3.0f));
        Variable<Vector3> vector3Var2 = new Variable<Vector3>("vector3Var2", new Vector3(2.0f, 2.0f, 2.0f));

        // Replace 'someTransform' and 'someOtherTransform' with actual Transform instances
        //Variable<Transform> transformVar1 = new Variable<Transform>("transformVar1", someTransform);
        //Variable<Transform> transformVar2 = new Variable<Transform>("transformVar2", someOtherTransform);

        // Vector comparison example
        if (IsLessThan(vector2Var1, vector2Var2))
        {
            Debug.Log("vector2Var1 is less than vector2Var2");
        }

        // Vector3 comparison example
        if (IsGreaterThan(vector3Var2, vector3Var1))
        {
            Debug.Log("vector3Var2 is greater than vector3Var1");
        }

        // Transform position comparison example
        //if (IsPositionEqual(transformVar1, transformVar2))
        //{
        //    Debug.Log("The positions of transformVar1 and transformVar2 are equal");
        //}

        // Integer comparison example
        if (IsLessThan(intVar1, intVar2))
        {
            Debug.Log("intVar1 is less than intVar2");
        }

        // Float comparison example
        if (IsGreaterThan(floatVar1, floatVar2))
        {
            Debug.Log("floatVar1 is greater than floatVar2");
        }

        // Double comparison example
        if (IsGreaterThan(doubleVar2, doubleVar1))
        {
            Debug.Log("doubleVar2 is greater than doubleVar1");
        }

        // Boolean comparison example
        if (IsTrue(boolVar1))
        {
            Debug.Log("boolVar1 is true");
        }

        if (IsFalse(boolVar2))
        {
            Debug.Log("boolVar2 is false");
        }

        // Using IsInRange for integers
        if (IsInRange(intVar1, 5, 20))
        {
            Debug.Log("intVar1 is within the range of 5 to 20");
        }

        // Using IsInRange for floats
        if (IsInRange(floatVar1, 3.0f, 4.0f))
        {
            Debug.Log("floatVar1 is within the range of 3.0 to 4.0");
        }

        // Using IsEqual for doubles
        if (IsEqual(doubleVar1, 123.456))
        {
            Debug.Log("doubleVar1 is equal to 123.456");
        }

        // Using IsPositionInRange for transforms
        /*
        Vector3 minPosition = new Vector3(-2, -2, -2);
        Vector3 maxPosition = new Vector3(2, 2, 2);
        if (IsPositionInRange(transformVar1, minPosition, maxPosition))
        {
            Debug.Log("transformVar1 is within the specified position range");
        }

        // Using IsPositionAbove for transforms
        if (IsPositionAbove(transformVar2, transformVar1))
        {
            Debug.Log("transformVar2 is positioned above transformVar1");
        }

        // Using IsPositionBelow for transforms
        if (IsPositionBelow(transformVar1, transformVar2))
        {
            Debug.Log("transformVar1 is positioned below transformVar2");
        }*/
    }
}
