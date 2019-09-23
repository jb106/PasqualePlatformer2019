using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Dynamics;
using UnityEngine.InputSystem;
using Cinemachine;



public class PlayerController : MonoBehaviour, InputMaster.IPlayerMovementActions
{
    public static PlayerController Instance = null;


    [Header("Player settings")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJumpHeight;
    [SerializeField] private float _minimumMovementValue;
    [SerializeField] private float _distanceJumpAnticipation;
    [SerializeField] private float _minimumDistanceForAnticipationAnimation;
    [SerializeField] private float _minimumDistanceForLandingAnimationWithoutJump = 1.0f;
    [SerializeField] private float _playerAccelerationSpeed = 1.0f;
    [SerializeField] private bool _isGrounded = false;
    

    [Header("Objects references")]
    [SerializeField] private Transform _playerCamera = null;
    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera = null;

    [Header("Components variables")]
    [SerializeField] private Rigidbody _rigid = null;
    [SerializeField] private CapsuleCollider _collider = null;
    [SerializeField] private Animator _playerAnimation = null;
    [SerializeField] private FullBodyBipedIK _fullBodyBipedIK = null;
    [SerializeField] private PuppetMaster _puppetMaster = null;
 

    [Header("Other variables")]
    [SerializeField] private Vector3 _moveDirection = new Vector3();
    [SerializeField] private GameObject _objectFloor = null;
    [SerializeField] private bool _jumpInProgress = false;


    public float sphereRadius;
    public float maxDistance;
    public Vector3 origin;
    public Vector3 direction;
    public Vector3 offsetOrigin;
    public LayerMask _layerMaskForGrounded;
    public float currentHitDistance;
    public float _distanceToGroundSaved = -1.0f;
    public PressurePlatform _pressurePlatform = null;

    //Not serialized and private variables
    private float _horizontalAxis = 0;
    private float _playerDirection = 90;
    private bool _isPlayerMoving = false;
    private float _accelerationValue = 0.0f;
    private bool _previouslyGrounded = false;
    private float _fallingDistanceBase = 0.0f;
    private float _fallingTimer = 0.0f;
    private bool _skipLandingAnimation = false;

    private float _timerFacingDirection = 0.0f;

    private Vector3 _predictionPosition = new Vector3();

    private bool _hittingLeftWall;
    private bool _hittingRightWall;

    private bool _canMove = true;

    private LeftRight _playerFacingDirection = LeftRight.Right;
    private InputMaster _inputMaster = null;



    //Getter for certain private variables
    public FullBodyBipedIK GetFullBodyBipedIK() { return _fullBodyBipedIK; }
    public LeftRight GetPlayerFacingDirection() { return _playerFacingDirection; }
    public Transform GetPlayerModel() { return transform; }
    public PuppetMaster GetPuppetMaster() { return _puppetMaster; }
    public CinemachineVirtualCamera GetPlayerVirtualCamera() { return _playerVirtualCamera; }
    public float GetTimerFacingDirection() { return _timerFacingDirection; }
    public bool GetPlayerJumpStatus() { return _jumpInProgress; }

    //And Setters
    public void SetPlayerCanMove(bool active) { _canMove = active; }


    //Register the player to the GameManager 
    private void Awake()
    {
        Instance = this;

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
                _skipLandingAnimation = false;
            }
            else
            {
                _skipLandingAnimation = true;
                if (_playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("Paoli_jump_middle"))
                    _playerAnimation.SetTrigger("skip_jump_end");
            }
        }


        //Set the  timer relatively to the player grounded bool
        if (_isGrounded)
            _fallingTimer = 0.0f;
        else
            _fallingTimer += Time.deltaTime;

        //Reset the acceleration value if the movement is null
        if (_horizontalAxis == 0)
            _accelerationValue = 0.0f;
        else
            _accelerationValue = Mathf.Clamp(_accelerationValue + Time.deltaTime * _playerAccelerationSpeed, 0.0f, 1.0f);

        //Send the value to the animator
        //With lerp <3
        float lerpAcceleration = Mathf.Lerp(_playerAnimation.GetFloat("acceleration"), _accelerationValue, Time.deltaTime * 4f);
        _playerAnimation.SetFloat("acceleration", lerpAcceleration);


        //If the player is not anymore grounded
        if (!_isGrounded && _previouslyGrounded)
            _fallingDistanceBase = transform.position.y;

        _previouslyGrounded = _isGrounded;


        CheckGrounded();
        CheckObstacleDirection();
        PlayerModelRotation();


        //Before movement, we need to apply the acceleration value to perform a smooth start on the animation
        Vector3 acceleratedMoveDirection = _moveDirection * Time.fixedDeltaTime;
        acceleratedMoveDirection.x = acceleratedMoveDirection.x * _accelerationValue;

        //If _canMove is set to false, we need to set the movement to 0 to stop the player
        if (!_canMove)
            acceleratedMoveDirection = Vector3.zero;

        //_rigid.MovePosition(transform.position + acceleratedMoveDirection);
        _rigid.velocity = new Vector3(acceleratedMoveDirection.x, _rigid.velocity.y, _rigid.velocity.z);


    }

    private bool CheckGrounded()
    {
        //init values for the ground detection
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

    private float GetDistanceToTheGround()
    {
        //init values for the ground detection
        origin = transform.position + offsetOrigin;
        direction = -transform.up;
        RaycastHit hit;
        if(Physics.SphereCast(origin, sphereRadius, direction, out hit, 1000f, _layerMaskForGrounded, QueryTriggerInteraction.UseGlobal))
        {
            _predictionPosition = hit.point;
            return hit.distance;
        }
        return 0.0f;
    }

    void CheckObstacleDirection()
    {
        /* BUG HERE BE CAREFUL WITH THIS PIECE OF CODE
        Vector3 offset = new Vector3(0, 2, 0);
        float rayLength = 2.5f;

        RaycastHit hit;

        if(Physics.Raycast(transform.position + offset, -transform.right, out hit, rayLength))
        {
            _hittingLeftWall = true;
            print("gauche toutEEEE");
        }
        else
        {
            _hittingLeftWall = false;
        }


        if (Physics.Raycast(transform.position + offset, transform.right, out hit, rayLength))
        {
            _hittingRightWall = true;
            print("droiiite touUUUTEEE");
        }
        else
        {
            _hittingRightWall = false;
        }
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_predictionPosition, 1.0f);

    }

    private void PlayerMovement(float movementValue)
    {
        if (!_rigid)
            return;

        float processedMovementValue = 0.0f;

        if (movementValue > 0 && movementValue < _minimumMovementValue)
            processedMovementValue = _minimumMovementValue;
        else if (movementValue < 0 && movementValue > -_minimumMovementValue)
            processedMovementValue = -_minimumMovementValue;
        else
            processedMovementValue = movementValue;

        //Get player movement
        _horizontalAxis = processedMovementValue;


        _isPlayerMoving = _horizontalAxis == 0.0f ? false : true;

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


        float multiplier_stick = processedMovementValue;
        if (multiplier_stick < 0) multiplier_stick = -multiplier_stick;

        //Set the stick speed (ANIMATION PART) / wr = WALK RUN
        _playerAnimation.SetFloat("movement_value", Mathf.Clamp(multiplier_stick + _minimumMovementValue, 0, 1.0f));
        _playerAnimation.SetFloat("blend_wr_value", multiplier_stick);

        _moveDirection = desiredMove * _playerSpeed * multiplier_stick;

        //Check if the object where the player is on is moving and add this velocity to the player movement
        if(_objectFloor && _isGrounded)
        {
            if (_objectFloor.GetComponent<MovablePlatform>())
            {
                _moveDirection += _objectFloor.GetComponent<MovablePlatform>().movement;
            }
        }
    }

    private void PlayerJump()
    {
        if (_canMove)
        {
            if (_isGrounded && !_jumpInProgress)
            {
                _rigid.velocity = new Vector3(_rigid.velocity.x, 0, _rigid.velocity.z);
                _rigid.AddForce(Vector3.up * _playerJumpHeight);
                _playerAnimation.Play("Paoli_jump");
                _jumpInProgress = true;

            }
        }
    }

    private void PlayerModelRotation()
    {
        _timerFacingDirection += Time.fixedDeltaTime;

        if (!_canMove)
            return;

        //Get the current player local rotation
        Vector3 localPlayerModelRotation = transform.rotation.eulerAngles;

        //Decide in which direction the player is facing
        if (_horizontalAxis > 0 && _playerFacingDirection != LeftRight.Right)
        {
            _playerDirection = 90;
            _playerFacingDirection = LeftRight.Right;

            _timerFacingDirection = 0.0f;
        }
        else if (_horizontalAxis < 0 && _playerFacingDirection != LeftRight.Left)
        {
            _playerDirection = 270;
            _playerFacingDirection = LeftRight.Left;

            _timerFacingDirection = 0.0f;
        }

        //Lerp direction 
        float currentDirection = Mathf.Lerp(localPlayerModelRotation.y, _playerDirection, Time.deltaTime * 10f);

        //Assign the new direction
        localPlayerModelRotation.y = currentDirection;
        transform.rotation = Quaternion.Euler(localPlayerModelRotation);
    }

    private void PlayerAnimation()
    {
        if (!_playerAnimation)
            return;

        _playerAnimation.SetBool("grounded", _isGrounded);

        if (!_isGrounded)
        {
            if(_rigid.velocity.y < 0.0f)
            {
                if(_distanceToGroundSaved==-1.0f)
                {
                    _distanceToGroundSaved = GetDistanceToTheGround();
                }

                if (!_jumpInProgress && !_playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("Paoli_jump_middle"))
                    //Additionnal condition to check the minimum distance required to trigger the landing animation without jump
                    if(GetDistanceToTheGround() > _minimumDistanceForLandingAnimationWithoutJump)
                        _playerAnimation.CrossFade("Paoli_jump_middle", 0.1f);

            }
        }
        else
        {
            if (_playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("Paoli_jump_middle"))
            {
                if(!_skipLandingAnimation)
                    _playerAnimation.Play("Paoli_jump_end");

                if (_jumpInProgress)
                    _jumpInProgress = false;
            }

            _playerAnimation.SetFloat("jump_progression", 0.0f);
            _distanceToGroundSaved = -1.0f;
        }

        float jumpProgression = GetDistanceToTheGround() / _distanceToGroundSaved;
        jumpProgression = jumpProgression - 1.0f;
        jumpProgression = -jumpProgression;

        if (_distanceToGroundSaved != -1.0f)
            _playerAnimation.SetFloat("jump_progression", jumpProgression);

        //Assign the movement animation (walk)
        bool isWalking = false;
        if (_horizontalAxis != 0 && _canMove)
            isWalking = true;
        _playerAnimation.SetBool("walk", isWalking);
    }



    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }

    public void ForcePlayerDirection(bool left)
    {
        if (left)
            _playerDirection = 270;
        else
            _playerDirection = 90;
    }


    public void ChangePlayerRigidBodyMass(float value)
    {
        _rigid.mass += value;
    }

    //Getters
    public Vector3 GetMoveDirection()
    {
        return _moveDirection;
    }

}
