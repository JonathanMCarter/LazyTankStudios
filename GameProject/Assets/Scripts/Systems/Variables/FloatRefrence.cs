using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatReference
{
    [SerializeField]private bool UseConstant = true;
    [SerializeField] private float ConstantValue;
    [SerializeField] private FloatVariable Variable;

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}

[CreateAssetMenu(fileName = "Float Value", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
    public float Value;
}