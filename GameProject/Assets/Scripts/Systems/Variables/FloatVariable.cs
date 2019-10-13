using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatReference : RefrenceVariable<float>{}

public abstract class RefrenceVariable<T>
{
    [SerializeField] private bool UseConstant = true;
    [SerializeField] private T ConstantValue;
    [SerializeField] private Variable<T> Variable;

    public T Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}

[CreateAssetMenu(fileName = "Float Value", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
    public float Value;
}

