using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GlobalFloat", order = 1)]
public class GlobalFloat : ScriptableObject
{
    public float InitialValue;

    [HideInInspector]
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize() { }
}