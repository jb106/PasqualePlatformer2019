﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _explosionBaseDamage;
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
                    //PlayerStats.Instance.Kill();
                    float distanceWithPlayer = Vector3.Distance(transform.position, r.transform.position);
                    float damageMultiplier = -((distanceWithPlayer / _explosionRadius) - 1);

                    float damageCalculation = _explosionBaseDamage * damageMultiplier;

                    PlayerStats.Instance.TakeDamage(damageCalculation);
                }
                else if(r.gameObject.layer == 10) //Ragdoll tag
                {
                    float distanceWithRagdollPart = Vector3.Distance(transform.position, r.transform.position);
                    float forceMultiplier = -((distanceWithRagdollPart / _explosionRadius) - 1);

                    forceMultiplier = Mathf.Clamp(forceMultiplier, 0.15f, 1.0f);


                    PlayerStats.Instance.GetHitReaction().Hit(h, -(transform.position - r.transform.position), r.position);
                }
                else
                {
                    r.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                }
            }
        }
    }
}
