using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _explosionPrefab;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != transform.parent.gameObject)
        {
            GameObject explosion = Instantiate(_explosionPrefab);
            explosion.transform.position = transform.position;

            if(transform.parent.gameObject.GetComponent<InteractableObject>())
            {
                transform.parent.gameObject.GetComponent<InteractableObject>().DestroyInteractableObject();
            }
        }
        
    }
}
