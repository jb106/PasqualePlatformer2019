using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstruction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private InteractableObjectData _interactableObjectRequired = null;
    [SerializeField] private List<Transform> _anchorPoints = new List<Transform>();

    [SerializeField] private int _anchorPointsIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InteractableObject>())
        {
            if(other.GetComponent<InteractableObject>().interactableObjectData == _interactableObjectRequired)
            {
                if (_anchorPointsIndex < _anchorPoints.Count - 1)
                {
                    if (other.GetComponent<Rigidbody>().isKinematic)
                        return;

                    print("executed");
                    InteractableObject rondin = other.GetComponent<InteractableObject>();

                    rondin.GetComponent<Rigidbody>().isKinematic = true;

                    rondin.canBeCarried = false;

                    rondin.transform.position = _anchorPoints[_anchorPointsIndex].position;
                    rondin.transform.rotation = _anchorPoints[_anchorPointsIndex].rotation;

                    rondin.transform.parent = _anchorPoints[_anchorPointsIndex];

                    _anchorPointsIndex++;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
