using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _mask;
    


    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    private float _height = -10.0f;

    private bool _willRespawn = false;
    private float _willRespawnDelay = 3f;

    Rigidbody _rigid;

    private void Start()
    {
        _defaultPosition = transform.position;
        _defaultRotation = transform.rotation;

        _rigid = GetComponent<Rigidbody>();

        StartCoroutine(RespawnBehaviour());
    }

    void RespawnAtDefaultPosition()
    {
        transform.position = _defaultPosition;
        transform.rotation = _defaultRotation;

        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_mask == (_mask | (1 << other.gameObject.layer)))
        {
            if (!_willRespawn)
                _willRespawn = true;
        }
    }

    IEnumerator RespawnBehaviour()
    {
        float respawnTimer = 0.0f;

        while(true)
        {
            //Respawn conditions
            if (transform.position.y <= _height)
                RespawnAtDefaultPosition();

            if(_willRespawn)
            {
                respawnTimer += Time.deltaTime;
            }

            if(_willRespawn && respawnTimer > _willRespawnDelay)
            {
                RespawnAtDefaultPosition();
                _willRespawn = false;
                respawnTimer = 0.0f;
            }

            yield return null;
        }
    }

}
