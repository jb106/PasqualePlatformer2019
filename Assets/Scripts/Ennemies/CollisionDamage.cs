using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _damage;

    private bool _singleDamage = false;
    private Rigidbody _rigid = null;


    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_singleDamage)
            return;

        if(collision.gameObject.name == "PlayerController")
        {
            PlayerStats.Instance.TakeDamage(_damage);
            _singleDamage = true;
        }
        else if (collision.gameObject.layer == 10) //Ragdoll tag
        {
            PlayerStats.Instance.GetHitReaction().Hit(collision.collider, _rigid.velocity, collision.GetContact(0).point);
        }
    }
}
