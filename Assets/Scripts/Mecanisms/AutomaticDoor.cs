using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private float _speed;
    [Header("References")]
    [SerializeField] private Transform _topPositionEmpty;
    [SerializeField] private Transform _bottomPositionEmpty;

    private Vector3 _defaultPosition;

    private void Start()
    {
        _defaultPosition = transform.position;
    }

    public void OpenDoor()
    {
        _isOpen = true;
    }

    public void CloseDoor()
    {
        _isOpen = false;
    }

    private void Update()
    {
        Vector3 desiredPosition;

        if (_isOpen)
        {
            float offsetHeight = _topPositionEmpty.position.y - _bottomPositionEmpty.position.y;
            desiredPosition = _defaultPosition + new Vector3(0, offsetHeight, 0);
        }
        else
        {
            desiredPosition = _defaultPosition;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * _speed);
    }
}
