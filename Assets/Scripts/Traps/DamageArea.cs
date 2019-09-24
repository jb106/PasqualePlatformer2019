using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9) //Player Layer
        {
            PlayerStats.Instance.TakeDamage(_damageAmount);
        }
    }
}
