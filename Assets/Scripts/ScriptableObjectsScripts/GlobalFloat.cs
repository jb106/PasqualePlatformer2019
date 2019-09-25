using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GlobalFloat", order = 1)]
public class GlobalFloat : ScriptableObject, ISerializationCallbackReceiver
{
    public float InitialValue;
    public float RuntimeValue;

    public float GetMaxValue() { return InitialValue; }

    private void Awake()
    {
        RuntimeValue = InitialValue;
    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize() { }
}