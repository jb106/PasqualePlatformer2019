using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void Start()
    {
        Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null)
            {
                if (r.gameObject.name == "PlayerController")
                {
                    PlayerStats.Instance.Kill();
                }
                else
                {
                    r.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                }
            }
        }
    }
}
