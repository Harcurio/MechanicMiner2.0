using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum EffectType
{
    Add,
    Subtract,
    Multiply,
    Divide,
    ChangeBool,
}
public class Effect
{
    

    public static void ApplyEffect(ref Variable<int> variable, EffectType effectType, int value)
    {
        ApplyArithmeticEffect(ref variable, effectType, value);
    }

    public static void ApplyEffect(ref Variable<float> variable, EffectType effectType, float value)
    {
        ApplyArithmeticEffect(ref variable, effectType, value);
    }

    public static void ApplyEffect(ref Variable<double> variable, EffectType effectType, double value)
    {
        ApplyArithmeticEffect(ref variable, effectType, value);
    }

    public static void ApplyEffect(ref Variable<bool> variable, EffectType effectType)
    {
        ApplyBoolEffect(ref variable, effectType);
    }

    public static void ApplyEffect(ref Variable<Vector2> variable, EffectType effectType, Vector2 value)
    {
        ApplyVectorEffect(ref variable, effectType, value);
    }

    public static void ApplyEffect(ref Variable<Vector3> variable, EffectType effectType, Vector3 value)
    {
        ApplyVectorEffect(ref variable, effectType, value);
    }

    /*
    public static void ApplyEffect(ref Variable<Transform> variable, EffectType effectType, Transform value)
    {
        ApplyTransformEffect(ref variable, effectType, value);
    }
    */
    private static void ApplyArithmeticEffect(ref Variable<int> variable, EffectType effectType, int value)
    {
        switch (effectType)
        {
            case EffectType.Add:
                variable.Value += value;
                break;
            case EffectType.Subtract:
                variable.Value -= value;
                break;
            case EffectType.Multiply:
                variable.Value *= value;
                break;
            case EffectType.Divide:
                if (value != 0)
                    variable.Value /= value;
                else
                    Debug.LogWarning("Division by zero!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    private static void ApplyArithmeticEffect(ref Variable<float> variable, EffectType effectType, float value)
    {
        switch (effectType)
        {
            case EffectType.Add:
                variable.Value += value;
                break;
            case EffectType.Subtract:
                variable.Value -= value;
                break;
            case EffectType.Multiply:
                variable.Value *= value;
                break;
            case EffectType.Divide:
                if (Mathf.Abs(value) > float.Epsilon)
                    variable.Value /= value;
                else
                    Debug.LogWarning("Division by zero!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    private static void ApplyArithmeticEffect(ref Variable<double> variable, EffectType effectType, double value)
    {
        switch (effectType)
        {
            case EffectType.Add:
                variable.Value += value;
                break;
            case EffectType.Subtract:
                variable.Value -= value;
                break;
            case EffectType.Multiply:
                variable.Value *= value;
                break;
            case EffectType.Divide:
                if (Math.Abs(value) > double.Epsilon)
                    variable.Value /= value;
                else
                    Debug.LogWarning("Division by zero!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    private static void ApplyBoolEffect(ref Variable<bool> variable, EffectType effectType)
    {
        if (effectType == EffectType.ChangeBool)
        {
            variable.Value = !variable.Value;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    private static void ApplyVectorEffect(ref Variable<Vector2> variable, EffectType effectType, Vector2 value)
    {
        switch (effectType)
        {
            case EffectType.Add:
                variable.Value += value;
                break;
            case EffectType.Subtract:
                variable.Value -= value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }


    private static void ApplyVectorEffect(ref Variable<Vector3> variable, EffectType effectType, Vector3 value)
    {
        switch (effectType)
        {
            case EffectType.Add:
                variable.Value += value;
                break;
            case EffectType.Subtract:
                variable.Value -= value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    /*
     * THIS CODE IS GONNA BE SPLITED INTO 3 DIFFERENT VARIABLES... VECTOR3 ALL OF THEM
    private static void ApplyTransformEffect(ref Variable<Transform> variable, EffectType effectType, Transform value)
    {
        Transform dynamicVariableValue = variable.Value;

        switch (effectType)
        {
            case EffectType.MovePosition:
                if (value != null)
                {
                    dynamicVariableValue.position += value.position;
                }
                else
                {
                    Debug.LogWarning("Invalid value for MovePosition effect.");
                }
                break;
            default:
                Debug.LogWarning("Effect not supported for Transform variables.");
                break;
        }
    }*/
}