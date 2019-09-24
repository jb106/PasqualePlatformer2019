using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance = null;
    private void Awake()
    {
        Instance = this;
    }

    [Header("References")]
    [SerializeField] private GameObject _aliveCamera;
    [SerializeField] private GameObject _deadCamera;

    private void Start()
    {
        SetAliveCamera();
    }

    public void SetAliveCamera()
    {
        _aliveCamera.SetActive(true);
        _deadCamera.SetActive(false);
    }

    public void SetDeadCamera()
    {
        _deadCamera.SetActive(true);
        _aliveCamera.SetActive(false);
    }
}
