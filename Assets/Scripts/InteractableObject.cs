using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeftRight { Left, Right }

public class InteractableObject : MonoBehaviour
{
    [Header("Do not touch theses variables")]
    public float distanceToPlayer = 0.0f;
    public LeftRight playerSide = LeftRight.Left;
    public bool thisIsCarried = false;

    [Header("Settings")]
    public Vector3 rotationOffset = new Vector3();
   
    [Header("References")]
    public InteractableObjectData interactableObjectData;
    public Transform bulletSpawner = null;

    //Private variables...
    private List<Transform> handles = new List<Transform>();
    private Quaternion _defaultRotation = Quaternion.identity;
    private GameObject _player;


   

    void Start()
    {
        GameManager.Instance.RegisterNewInteractableObject(this);
        _player = GameManager.Instance.GetPlayer();

        foreach (Transform handle in transform)
            handles.Add(handle);

        _defaultRotation = transform.rotation * Quaternion.Euler(rotationOffset);
        UpdateHandles();
    }

    public List<Transform> GetHandles()
    {
        return handles;
    }

    public Transform GetLeftHandle()
    {
        return handles[1];

    }

    public Transform GetRightHandle()
    {
        return handles[0];
    }

    public Quaternion GetDefaultRotation()
    {
        Quaternion defaultRotation = _defaultRotation;

        if (playerSide == LeftRight.Right)
        {
            Vector3 convertedDefaultRotation = defaultRotation.eulerAngles;
            convertedDefaultRotation.y += 180;
            defaultRotation = Quaternion.Euler(convertedDefaultRotation);
        }

        return defaultRotation;
    }

    private void Update()
    {
        if (!_player)
            return;

        distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        playerSide = transform.position.x >= _player.transform.position.x ? LeftRight.Left : LeftRight.Right;

        if (thisIsCarried)
        {

            //Part to save the new position / rotation 
            SaveHandlesToData();

            //Always set the position / rotation of the handles regardless to the scriptable object data
            UpdateHandles();

        }
        
    }

    private void SaveHandlesToData()
    {
        return;

        interactableObjectData.leftHandleDefaultRotation = GetLeftHandle().localRotation.eulerAngles;
        interactableObjectData.rightHandleDefaultRotation = GetRightHandle().localRotation.eulerAngles;

        interactableObjectData.leftHandleDefaultPosition = GetLeftHandle().localPosition;
        interactableObjectData.rightHandleDefaultPosition = GetRightHandle().localPosition;
    }

    private void UpdateHandles()
    {
        GetLeftHandle().localRotation = Quaternion.Euler(interactableObjectData.leftHandleDefaultRotation);
        GetRightHandle().localRotation = Quaternion.Euler(interactableObjectData.rightHandleDefaultRotation);

        GetLeftHandle().localPosition = interactableObjectData.leftHandleDefaultPosition;
        GetRightHandle().localPosition = interactableObjectData.rightHandleDefaultPosition;
    }

}
