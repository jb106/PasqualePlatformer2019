﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance = null;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Settings")]
    [SerializeField] private float ToDelete;

    private Vector3 _defaultPosition;

    //Getters
    public Vector3 GetPlayerDefaultPosition() { return _defaultPosition; }

    private void Start()
    {
        _defaultPosition = transform.position;
    }

    public void TeleportPlayerAtDefaultPosition(bool teleportPuppetMaster = false)
    {
        transform.position = _defaultPosition;

        if (teleportPuppetMaster)
            PlayerController.Instance.GetPuppetMaster().transform.position = _defaultPosition;
    }

}
