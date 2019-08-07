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
    [SerializeField] private List<Board> _boards = new List<Board>();

    private int _currentBoardIndex = -1;
    private Transform _currentCameraTarget = null;

    //-------------------------------------------------------------------------------
    //------------------------------------
    //---------------

    private void Start()
    {
        InitBoards();
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
    public void SetBoard(int boardIndex, bool next)
    {
        SetBoardConfiguration(boardIndex, next);

        print("New Board " + boardIndex + " Set !");
    }

    public void SetPreviousBoard()
    {
        if (_currentBoardIndex - 1 < 0)
            return;

        SetBoard(_currentBoardIndex - 1, false);
    }

    public void SetNextBoard()
    {
        if (_currentBoardIndex + 1 > _boards.Count-1)
            return;

        SetBoard(_currentBoardIndex + 1, true);
    }

    //PRIVATE
    //-----------------------------

    private void InitBoards()
    {
        foreach (Board board in _boards)
            board.SetActiveBoard(false);

        SetBoard(0, true);
    }

    IEnumerator VariousUpdates()
    {
        while(true)
        {
            //We manage here the camera position. To get a smooth lerp between each board, we need to set a target to the camera and set up a lerp
            if(_currentCameraTarget)
            {
                _playerCamera.transform.position = Vector3.Lerp(_playerCamera.transform.position, _currentCameraTarget.position, Time.deltaTime * _cameraSpeed);
            }

            yield return null;
        }
    }

    //The bool next is here to know if the mother function was a previous or a next boad function
    private void SetBoardConfiguration(int boardIndex, bool next)
    {
        //First, disable the previous board
        if (_currentBoardIndex != -1)
            _boards[_currentBoardIndex].SetActiveBoard(false);

        //print("Board " + _currentBoardIndex + " is now disabled");

        //We update to the new board 
        _currentBoardIndex = boardIndex;

        //We activate the new board
        _boards[_currentBoardIndex].SetActiveBoard(true);
        //print("Board " + _currentBoardIndex + " is now enabled");

        //We assign the position with the new board
        //If this function was start to access the previous board, just spawn the player on the playerExitSpawnPosition 
        Transform spawn = next == true ? _boards[_currentBoardIndex].playerEntranceSpawnPosition : _boards[_currentBoardIndex].playerExitSpawnPosition;

        _player.transform.position = spawn.position;
        _currentCameraTarget = _boards[_currentBoardIndex].cameraPointOfView;
    }

}
