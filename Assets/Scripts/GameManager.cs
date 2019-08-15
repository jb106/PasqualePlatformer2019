using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    //All interfaces ...
    //-------------------------------
    //------------------------------------------------------

    //------------------------------------------------------
    //------------------------------

    //-----ALL VARIABLES
    //-------------------------------------------------------------------
    //-------------------------------------------------------------------------------

    [Header("General game-settings")]
    [SerializeField] private float _cameraSpeed = 5.0f;

    [Header("Registered objects")]
    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _playerCamera = null;
    [SerializeField] private List<InteractableObject> _interactableObjects = new List<InteractableObject>();


    //-------------------------------------------------------------------------------
    //------------------------------------
    //---------------

    private void Start()
    {
        StartCoroutine(VariousUpdates());
    }


    //SETTERS
    //-----------------------------
    //-----------------------------------------------------

    public void RegisterNewInteractableObject(InteractableObject interactableObject)
    {
        _interactableObjects.Add(interactableObject);
    }

    public void RegisterPlayer(GameObject playerObject)
    {
        _player = playerObject;
    }

    public void RegisterPlayerCamera(GameObject cameraObject)
    {
        _playerCamera = cameraObject;
    }


    //GETTERS
    //-----------------------------
    //-----------------------------------------------------
    public List<InteractableObject> GetInteractableObjects()
    {
        return _interactableObjects;
    }

    public GameObject GetPlayer()
    {
        return _player;
    }


    //FUNCTIONS
    //-----------------------------
    //-----------------------------------------------------

    //PUBLIC
    //-----------------------------


    //PRIVATE
    //-----------------------------


    IEnumerator VariousUpdates()
    {
        while(true)
        {

     
            yield return null;
        }
    }



}
