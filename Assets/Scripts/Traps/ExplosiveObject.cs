using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _explosionPrefab;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != transform.parent.gameObject)
        {

            if (_mask == (_mask | (1 << collider.gameObject.layer)))
            {
                print(collider.gameObject.layer);
                GameObject explosion = Instantiate(_explosionPrefab);
                explosion.transform.position = transform.position;

                if (transform.parent.gameObject.GetComponent<InteractableObject>())
                {
                    transform.parent.gameObject.GetComponent<InteractableObject>().DestroyInteractableObject();
                }
            }
        }
    }
}
