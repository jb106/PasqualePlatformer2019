using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour, InputMaster.IPlayerInteractionActions
{
    public static PlayerInteractions Instance = null;


    [Header("Interaction settings")]
    [SerializeField] private float _distanceToInteract = 3.0f;
    [SerializeField] private float _bodyLevelWhenCrouching = 2.5f;
    [SerializeField] private float _objectThrowForce = 10f;


    [Header("References")]
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private Transform _carryingObjectPosition;
    public Animator _carryingObjectAnimator;

    [Header("HUD related Settings")]
    [SerializeField] private Vector3 _interactableObjectButtonOffset = new Vector3();
    [SerializeField] private Vector3 _healthBarOffset = new Vector3();

    [Header("HUD related References")]
    [SerializeField] private Canvas _mainHudCanvas = null;
    [SerializeField] private GameObject _yButton = null;
    [SerializeField] public GameObject startButton = null;
    [SerializeField] private GameObject _healthBarParent = null;
    [SerializeField] private Transform _playerHead = null;


    [SerializeField] private GameObject _interactableTarget = null;
    [SerializeField] private GameObject _currentInteractableObjectCarried = null;


    //Private reference to Player Components (assigned in the start function from the GameManager)
    private PlayerController _playerController = null;

    //Other private variables
    private bool _isCaryingSomething = false;
    private Transform _leftHandHandle, _rightHandHandle;

    //This boolean is useful when we have delays on grabing objects so if the thing is processing something we can't react to player input
    private bool _isProcessing = false;

    [SerializeField] private Vector3 _bodyOffsetPosition = new Vector3();
    private float _handsWeight = 0.0f;
    private float _handLerpValue = 0.0f;
    private InputMaster _inputMaster = null;

    private bool _canInteract = true;

    //Routines
    private Coroutine grabRoutine = null;
    private Coroutine releaseRoutine = null;

    //Getters
    public GameObject GetCurrentInteractableObjectCarried() { return _currentInteractableObjectCarried; }
    public bool CheckIfPlayerIsCarryingSomething() { return _isCaryingSomething; }

    //Setter
    public void SetPlayerCanInteract(bool active) { _canInteract = active; }

    void Awake()
    {
        Instance = this;

        _inputMaster = new InputMaster();
        _inputMaster.PlayerInteraction.SetCallbacks(this);
    }

    //------ INTERFACES
    //---------------------------------------
    //-----------------------------------------


    void OnEnable()
    {
        _inputMaster.PlayerInteraction.Enable();
    }

    void OnDisable()
    {
        _inputMaster.PlayerInteraction.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //Security to avoid executing the code multiple time if he is already in a coroutine instance
        if (_isProcessing)
            return;

        if (!_canInteract)
            return;

        //Key to release an object
        if (_isCaryingSomething)
        {
            releaseRoutine = StartCoroutine(StartReleaseObject());
        }
        else
        {
            if (_interactableTarget != null)
            {
                //Check if the player is facing the object to avoid bug because the player is not turned toward the object
                if (_interactableTarget.GetComponent<InteractableObject>().playerSide != _playerController.GetPlayerFacingDirection() && PlayerController.Instance.GetTimerFacingDirection() > 0.5f && !PlayerController.Instance.GetPlayerJumpStatus())
                    grabRoutine = StartCoroutine(StartGrabingObject());
            }
        }
    }



    //-----------------------------------------
    //---------------------------------------
    //------

    private void Start()
    {
        //Getting all player components needed
        _playerController = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();

        StartCoroutine(CheckingAndUpdatingStuff());
    }

    private void LateUpdate()
    {
        _playerController.GetFullBodyBipedIK().solver.bodyEffector.positionOffset = _bodyOffsetPosition;

        //Lerp the weights off the hands for grabing animation...
        float lerpSpeed = 6f;

        _handLerpValue = Mathf.Lerp(_handLerpValue, _handsWeight, Time.deltaTime * lerpSpeed);

        //Assign the hand position
        _playerController.GetFullBodyBipedIK().solver.leftHandEffector.positionWeight = _handLerpValue;
        _playerController.GetFullBodyBipedIK().solver.rightHandEffector.positionWeight = _handLerpValue;

        //Assign the hand rotation
        _playerController.GetFullBodyBipedIK().solver.leftHandEffector.rotationWeight = _handLerpValue;
        _playerController.GetFullBodyBipedIK().solver.rightHandEffector.rotationWeight = _handLerpValue;

        if (_isProcessing)
            return;

        if(!_canInteract)
        {
            if (_isCaryingSomething)
            {
                StartCoroutine(StartReleaseObject());
            }
        }

        
        if(_isCaryingSomething)
        {
            _leftHandTarget.position = _leftHandHandle.position;
            _rightHandTarget.position = _rightHandHandle.position;

            //Copy also the rotation of the handles
            _leftHandTarget.rotation = _leftHandHandle.rotation;
            _rightHandTarget.rotation = _rightHandHandle.rotation;
        }
    }


    //-----------------------
    //-------------------------------------

    private void GrabObject()
    {
        _currentInteractableObjectCarried = _interactableTarget;
        _isCaryingSomething = true;

        //This variable is usefull to check on any InteractableObject if it's carried or not
        _currentInteractableObjectCarried.GetComponent<InteractableObject>().thisIsCarried = true;

        //Assign the hands handles
        _leftHandHandle = _currentInteractableObjectCarried.GetComponent<InteractableObject>().GetLeftHandle();
        _rightHandHandle = _currentInteractableObjectCarried.GetComponent<InteractableObject>().GetRightHandle();

        _handsWeight = 1.0f;

    }
    public void ReleaseObject()
    {
        if (_currentInteractableObjectCarried)
        {
            //This variable is usefull to check on any InteractableObject if it's carried or not
            _currentInteractableObjectCarried.GetComponent<InteractableObject>().thisIsCarried = false;

            _currentInteractableObjectCarried.transform.parent = null;
            _currentInteractableObjectCarried.GetComponent<Rigidbody>().isKinematic = false;
            _currentInteractableObjectCarried.GetComponent<Collider>().isTrigger = false;

            //Apply a force relative to the player movement
            _currentInteractableObjectCarried.GetComponent<Rigidbody>().AddForce(_playerController.GetMoveDirection() * Time.fixedDeltaTime * _objectThrowForce * 100f);

            //Random rotation at throwing
            Vector3 torque = new Vector3();
            torque.x = Random.Range(-200, 200);
            torque.y = Random.Range(-200, 200);
            torque.z = Random.Range(-200, 200);
            _currentInteractableObjectCarried.GetComponent<Rigidbody>().AddTorque(torque);

            //Weight things with the rigid body
            PlayerController.Instance.ChangePlayerRigidBodyMass(-_currentInteractableObjectCarried.GetComponent<Rigidbody>().mass);
            if (PlayerController.Instance._pressurePlatform)
                PlayerController.Instance._pressurePlatform.ChangeCurrentWeight(-_currentInteractableObjectCarried.GetComponent<Rigidbody>().mass);

        }

        _currentInteractableObjectCarried = null;
        _isCaryingSomething = false;
    }

    IEnumerator CheckingAndUpdatingStuff()
    {
        while (true)
        {
            _interactableTarget = null;
            List<InteractableObject> _nearestObjects = new List<InteractableObject>();

            foreach ( InteractableObject obj in GameManager.Instance.GetInteractableObjects())
            {
                if (obj.distanceToPlayer <= _distanceToInteract && obj.playerSide != PlayerController.Instance.GetPlayerFacingDirection())
                    _nearestObjects.Add(obj);
            }

            float nearestDistance = -1f;
            InteractableObject nearestObject = null;

            foreach (InteractableObject obj in _nearestObjects)
            {
                if(nearestDistance == -1f)
                {
                    nearestObject = obj;
                    nearestDistance = Vector3.Distance(transform.position, obj.transform.position);
                }
                else
                {
                    float newDistance = Vector3.Distance(transform.position, obj.transform.position);

                    if(newDistance < nearestDistance)
                    {
                        nearestObject = obj;
                        nearestDistance = newDistance;
                    }
                }
            }

            if (nearestObject)
                _interactableTarget = nearestObject.gameObject;
            else
                _interactableTarget = null;

            //HUD PART BELOW
            //----------------
            //--------------------------------


            //Set healthBar near the player head
            SetCanvasElementOnTarget(_healthBarParent, _playerHead, _healthBarOffset, true, 15f);

            if (PlayerStats.Instance.playerStatsPhase == PlayerStatsPhase.Alive)
            {
                if (_healthBarParent.activeSelf == false)
                {
                    _healthBarParent.SetActive(true);
                }
            }
            else if (PlayerStats.Instance.playerStatsPhase == PlayerStatsPhase.Dead)
            {
                if (_healthBarParent.activeSelf == true)
                {
                    _healthBarParent.SetActive(false);
                }
            }


            if(_interactableTarget)
            {
                if(_isCaryingSomething || PlayerStats.Instance.playerStatsPhase == PlayerStatsPhase.Dead)
                    _yButton.GetComponent<Animator>().SetBool("pop", false);
                else
                    _yButton.GetComponent<Animator>().SetBool("pop", true);
                

                SetCanvasElementOnTarget(_yButton, _interactableTarget.transform, _interactableObjectButtonOffset);
            }
            else
            {
                _yButton.GetComponent<Animator>().SetBool("pop", false);
            }

            yield return null;
        }
    }

    public void SetCanvasElementOnTarget(GameObject element, Transform target, Vector3 offset, bool lerp = false, float lerpSpeed = 0.0f)
    {
        RectTransform CanvasRect = _mainHudCanvas.GetComponent<RectTransform>();

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target.position + offset);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        if (lerp)
            element.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(element.GetComponent<RectTransform>().anchoredPosition, WorldObject_ScreenPosition, Time.deltaTime * lerpSpeed);
        else
            element.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }


    IEnumerator StartGrabingObject()
    {
        _isProcessing = true;
        _playerController.SetCanMove(false);

        GrabObject();

        float crouchSpeed = 4f;

        //Lerp the crouch
        float timerLerp = 0.0f;
        while(true)
        {
            float newLevelValue = Mathf.Lerp(_bodyOffsetPosition.y, _bodyLevelWhenCrouching, timerLerp);
            _bodyOffsetPosition = new Vector3(0, newLevelValue, 0);

            _leftHandTarget.position = _leftHandHandle.position;
            _rightHandTarget.position = _rightHandHandle.position;

            timerLerp += Time.deltaTime * crouchSpeed;
            if (timerLerp >= 1.0f)
                break;

            yield return null;
        }

        _currentInteractableObjectCarried.transform.rotation = _currentInteractableObjectCarried.GetComponent<InteractableObject>().GetDefaultRotation();
        _currentInteractableObjectCarried.transform.parent = _carryingObjectPosition;
        
        _currentInteractableObjectCarried.GetComponent<Rigidbody>().isKinematic = true;
        _currentInteractableObjectCarried.GetComponent<Collider>().isTrigger = true;

        //Lerp the stand up
        timerLerp = 0.0f;
        while (true)
        {
            float newLevelValue = Mathf.Lerp(_bodyOffsetPosition.y, 0, timerLerp);
            _bodyOffsetPosition = new Vector3(0, newLevelValue, 0);

            //Smoothly lerp the position and rotation of the grabbed object
            _currentInteractableObjectCarried.transform.localPosition = Vector3.Lerp(_currentInteractableObjectCarried.transform.localPosition, Vector3.zero, timerLerp);

            _leftHandTarget.position = _leftHandHandle.position;
            _rightHandTarget.position = _rightHandHandle.position;

            timerLerp += Time.deltaTime * crouchSpeed;
            if (timerLerp >= 1.0f)
                break;

            yield return null;
        }

        //Weight things with the rigid body
        PlayerController.Instance.ChangePlayerRigidBodyMass(_currentInteractableObjectCarried.GetComponent<Rigidbody>().mass);
        if (PlayerController.Instance._pressurePlatform)
            PlayerController.Instance._pressurePlatform.ChangeCurrentWeight(_currentInteractableObjectCarried.GetComponent<Rigidbody>().mass);

        _playerController.SetCanMove(true);
        _isProcessing = false;

        grabRoutine = null;
    }

    IEnumerator StartReleaseObject()
    {
        _isProcessing = true;


        if (_playerController.GetMoveDirection().magnitude != 0)
        {
            _carryingObjectAnimator.SetTrigger("throw");
            float timer = 0.0f;
            while (true)
            {
                _leftHandTarget.position = _leftHandHandle.position;
                _rightHandTarget.position = _rightHandHandle.position;

                timer += Time.deltaTime;

                if (timer > 0.28f)
                    break;

                yield return null;
            }
        }

        ReleaseObject();
        _handsWeight = 0.0f;

        //Security wait before doing the next action which is grab object (if not the script will release and instant grab back the object)
        yield return new WaitForSeconds(0.5f);

        _isProcessing = false;
        releaseRoutine = null;
    }

    public void ForceStopGrabCoroutineAndRelease()
    {
        if (grabRoutine == null)
            return;

        StopCoroutine(grabRoutine);
        grabRoutine = null;

        _bodyOffsetPosition = Vector3.zero;

        ReleaseObject();
        _handsWeight = 0.0f;
        _isProcessing = false;
    }
}
