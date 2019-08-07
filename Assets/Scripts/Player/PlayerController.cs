using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputMaster.IPlayerMovementActions
{

    [Header("Player settings")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJumpHeight;
    [SerializeField] private bool _isGrounded = false;
    

    [Header("Objects references")]
    [SerializeField] private Transform _playerModel = null;
    [SerializeField] private Transform _playerCamera = null;

    [Header("Components variables")]
    [SerializeField] private Rigidbody _rigid = null;
    [SerializeField] private CapsuleCollider _collider = null;
    [SerializeField] private Animator _playerAnimation = null;
    [SerializeField] private FullBodyBipedIK _fullBodyBipedIK = null;


    [Header("Other variables")]
    [SerializeField] private Vector3 _moveDirection = new Vector3();
    [SerializeField] private GameObject _objectFloor = null;


    public float sphereRadius;
    public float maxDistance;
    public Vector3 origin;
    public Vector3 direction;
    public Vector3 offsetOrigin;
    public LayerMask _layerMaskForGrounded;
    public float currentHitDistance;

    //Not serialized and private variables
    private float _horizontalAxis = 0;
    private float _playerDirection = 90;
    private bool _jumpToggle = false;
    private bool _previouslyGrounded = false;
    private float _fallingDistanceBase = 0.0f;
    private float _fallingTimer = 0.0f;

    private bool _hittingLeftWall;
    private bool _hittingRightWall;

    private bool _canMove = true;

    private LeftRight _playerFacingDirection = LeftRight.Right;
    private InputMaster _inputMaster = null;



    //Getter for certain private variables
    public FullBodyBipedIK GetFullBodyBipedIK() { return _fullBodyBipedIK; }
    public LeftRight GetPlayerFacingDirection() { return _playerFacingDirection; }
    public Transform GetPlayerModel() { return _playerModel; }


    //Register the player to the GameManager 
    private void Awake()
    {
        GameManager.Instance.RegisterPlayer(gameObject);
        GameManager.Instance.RegisterPlayerCamera(_playerCamera.gameObject);

        _inputMaster = new InputMaster();
        _inputMaster.PlayerMovement.SetCallbacks(this);
    }

    //------ INTERFACES
    //---------------------------------------
    //-----------------------------------------


    void OnEnable()
    {
        _inputMaster.PlayerMovement.Enable();
    }

    void OnDisable()
    {
        _inputMaster.PlayerMovement.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        PlayerJump();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        PlayerMovement(context.ReadValue<float>());
    }


    //-----------------------------------------
    //---------------------------------------
    //------

    private void Start()
    {
        //Register input actions relative to the InputMaster

    }
   

    private void Update()
    {
        PlayerAnimation();
        _isGrounded = CheckGrounded();


        if (!_previouslyGrounded && _isGrounded)
        {
            if (_fallingTimer > 0.1f)
            {
                //Do something if  he drops from a certain height (after a delay)
            }

            //Calcul de la distance totale du saut en hauteur
            if (_fallingDistanceBase > transform.position.y)
            {
                float fallingDistance = _fallingDistanceBase - transform.position.y;
            }
        }


        //Set the  timer relatively to the player grounded bool
        if (_isGrounded) _fallingTimer = 0.0f;
        else
            _fallingTimer += Time.deltaTime;


        //If the player is not anymore grounded
        if (!_isGrounded && _previouslyGrounded)
            _fallingDistanceBase = transform.position.y;

        _previouslyGrounded = _isGrounded;


    }

    private void FixedUpdate()
    {
        CheckGrounded();
        CheckObstacleDirection();
        PlayerModelRotation();

        _rigid.MovePosition(transform.position + _moveDirection);
    }


    private bool CheckGrounded()
    {
        origin = transform.position + offsetOrigin;
        direction = -transform.up;
        bool isGrounded = false;
        RaycastHit hit;
        if(Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, _layerMaskForGrounded, QueryTriggerInteraction.UseGlobal))
        {
            _objectFloor = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            isGrounded = true;
        }
        else
        {
            _objectFloor = null;
            currentHitDistance = maxDistance;
        }

        return isGrounded;
    }

    void CheckObstacleDirection()
    {
        Vector3 offset = new Vector3(0, 2, 0);
        float rayLength = 2.5f;

        RaycastHit hit;

        if(Physics.Raycast(transform.position + offset, -transform.right, out hit, rayLength))
        {
            _hittingLeftWall = true;
        }
        else
        {
            _hittingLeftWall = false;
        }


        if (Physics.Raycast(transform.position + offset, transform.right, out hit, rayLength))
        {
            _hittingRightWall = true;
        }
        else
        {
            _hittingRightWall = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius); 

    }

    private void PlayerMovement(float movementValue)
    {
        if (!_rigid)
            return;

        //Get player movement
        _horizontalAxis = movementValue;

        //If _canMove is set to false, we need to set the movement to 0 to stop the player
        if (!_canMove)
            _horizontalAxis = 0.0f;

        //Limit movement to prevent the player from running in walls (friction issue)
        if (_horizontalAxis < 0)
            if (_hittingLeftWall)
                _horizontalAxis = 0;
        if (_horizontalAxis > 0)
            if (_hittingRightWall)
                _horizontalAxis = 0;

        Vector3 desiredMove = new Vector3(_horizontalAxis, 0, 0);

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 2f))
        {
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
        }

        _moveDirection.x = desiredMove.x * _playerSpeed;


        //Check if the object where the player is on is moving and add this velocity to the player movement
        if(_objectFloor && _isGrounded)
        {
            if(_objectFloor.GetComponent<MovablePlatform>())
            {
                _moveDirection += _objectFloor.GetComponent<MovablePlatform>().movement;
            }
        }

        _moveDirection = _moveDirection * Time.deltaTime;
    }

    private void PlayerJump()
    {
        if (_canMove)
        {
            _jumpToggle = true;

            if (_isGrounded)
            {
                if (_jumpToggle)
                {
                    //_moveDirection.y = _playerJumpHeight;
                    _rigid.AddForce(Vector3.up * _playerJumpHeight);
                    _playerAnimation.Play("Paoli_jump");

                    _jumpToggle = false;
                }
            }
        }
    }

    private void PlayerModelRotation()
    {
        if (!_playerModel)
            return;

        //Get the current player local rotation
        Vector3 localPlayerModelRotation = _playerModel.transform.localRotation.eulerAngles;

        //Decide in which direction the player is facing
        if (_horizontalAxis > 0)
        {
            _playerDirection = 90;
            _playerFacingDirection = LeftRight.Right;
        }
        else if (_horizontalAxis < 0)
        {
            _playerDirection = 270;
            _playerFacingDirection = LeftRight.Left;
        }

        //Lerp direction 
        float currentDirection = Mathf.Lerp(localPlayerModelRotation.y, _playerDirection, Time.deltaTime * 10f);

        //Assign the new direction
        localPlayerModelRotation.y = currentDirection;
        _playerModel.localRotation = Quaternion.Euler(localPlayerModelRotation);
    }

    private void PlayerAnimation()
    {
        if (!_playerAnimation)
            return;

        _playerAnimation.SetBool("grounded", _isGrounded);

        bool isWalking = false;
        if (_horizontalAxis != 0)
            isWalking = true;


        _playerAnimation.SetBool("walk", isWalking);
    }



    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }


    //Getters
    public Vector3 GetMoveDirection()
    {
        return _moveDirection;
    }

}
