using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _objectToContact;
    [SerializeField] private string _functionToCallOnEnter;
    [SerializeField] private string _functionToCallOnExit;
    [SerializeField] private LayerMask _mask;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>())
            return;

        if (_mask == (_mask | (1 << other.gameObject.layer)))
        {
            _objectToContact.SendMessage(_functionToCallOnEnter, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>())
            return;

        if (_mask == (_mask | (1 << other.gameObject.layer)))
        {
            _objectToContact.SendMessage(_functionToCallOnExit, other);
        }
    }
}
