using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public GameObject splashPrefab = null;

    private bool _isInWater = false;
    private GameObject _currentWater = null;
    private Rigidbody _rigid = null;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            _rigid.drag = 5.0f;
            _rigid.angularDrag = 5.0f;
            _currentWater = other.gameObject;

            //If there is a splash, add it
            if (splashPrefab)
            {
                //Do it only if the magnitude (speed) of the object is above 10
                if (_rigid.velocity.magnitude >= 10.0f)
                {
                    GameObject newSplash = Instantiate(splashPrefab);
                    newSplash.transform.position = new Vector3(transform.position.x, _currentWater.transform.GetChild(0).position.y, transform.position.z);
                }
            }

            _isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Water" && _isInWater)
        {
            _rigid.drag = 0f;
            _rigid.angularDrag = 0.5f;
            _currentWater = null;
            _isInWater = false;
        }
    }

    private void FixedUpdate()
    {
        if(_isInWater)
        {
            float upForceMultiplicator = _currentWater.transform.GetChild(0).position.y - transform.position.y;

            float addedForce = upForceMultiplicator >= 0.5f ? 2f : 1.0f;

            _rigid.AddForce(Vector3.up * -Physics.gravity.y * addedForce);
        }
    }
}
