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
            if (collider.gameObject == transform.gameObject)
                print("lui meme mon cher");

            GameObject explosion = Instantiate(_explosionPrefab);
            explosion.transform.position = transform.position;
            Destroy(transform.parent.gameObject);
        }
        
    }
}
