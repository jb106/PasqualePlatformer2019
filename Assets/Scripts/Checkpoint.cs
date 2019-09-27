using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("References")]
    public Transform _spawnEmpty = null;

    private bool _alreadyActivated = false;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void ActivateCheckpoint()
    {
        if (_alreadyActivated)
            return;

        _anim.SetTrigger("pop");

        GameManager.Instance.SetNewCheckpoint();
        _alreadyActivated = true;
    }
}
