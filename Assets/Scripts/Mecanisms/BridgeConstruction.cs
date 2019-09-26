using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstruction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private InteractableObjectData _interactableObjectRequired = null;
    [SerializeField] private List<Transform> _anchorPoints = new List<Transform>();
    [SerializeField] private AnimationCurve _lerpCurve;

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

                    InteractableObject rondin = other.GetComponent<InteractableObject>();

                    rondin.GetComponent<Rigidbody>().isKinematic = true;

                    rondin.canBeCarried = false;

                    StartCoroutine(SetObjectAtPositionRotation(rondin.transform, _anchorPoints[_anchorPointsIndex], 0.5f));

                    //rondin.transform.parent = _anchorPoints[_anchorPointsIndex];
                    _anchorPointsIndex++;

                }
            }
        }
    }

    IEnumerator SetObjectAtPositionRotation(Transform obj, Transform target, float speedMultiplier)
    {
        float lerpTimer = 0.0f;

        while(true)
        {
            lerpTimer += Time.deltaTime * speedMultiplier;
            float calculatedLerp = _lerpCurve.Evaluate(lerpTimer);

            obj.position = Vector3.Lerp(obj.position, target.position, calculatedLerp);
            obj.rotation = Quaternion.Lerp(obj.rotation, target.rotation, calculatedLerp);

            if (lerpTimer > 1.0f)
                break;

            yield return null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
