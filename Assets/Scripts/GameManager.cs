using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState { Intro, InGame, End }

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
    [SerializeField] private bool _resetBestScore = false;

    [Header("References")]
    [SerializeField] private Transform _beginLevelHud = null;
    [SerializeField] private TextMeshProUGUI _recordTextBegin = null;
    [SerializeField] private Transform _endLevelHud = null;
    [SerializeField] private TextMeshProUGUI _recordTextEnd = null;
    [SerializeField] private TextMeshProUGUI _deadText = null;
    [SerializeField] private Transform _newRecord = null;

    [Header("Registered objects")]
    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _playerCamera = null;
    [SerializeField] private List<InteractableObject> _interactableObjects = new List<InteractableObject>();
    [SerializeField] private List<Checkpoint> _checkpoints = new List<Checkpoint>();

    private int _checkpointIndex = -1;
    private Checkpoint _currentCheckpoint = null;
    private int _playerDeath = 0;
    private float _gameTimer = 0.0f;

    private int _lastScore = 0;

    private GameState _gameState = GameState.Intro;

    //-------------------------------------------------------------------------------
    //------------------------------------
    //---------------

    private void Start()
    {
        if (_resetBestScore)
            PlayerPrefs.SetInt("score", 360);

        StartCoroutine(VariousUpdates());

        PlayerController.Instance.SetCanMove(false);

        _lastScore = PlayerPrefs.GetInt("score");
        _recordTextBegin.text = "Record à battre: " + _lastScore + "s";
    }


    //SETTERS
    //-----------------------------
    //-----------------------------------------------------

    public void RegisterNewInteractableObject(InteractableObject interactableObject)
    {
        _interactableObjects.Add(interactableObject);
    }

    public void UnregisterInteractableObject(InteractableObject interactableObject)
    {
        if (_interactableObjects.Contains(interactableObject))
            _interactableObjects.Remove(interactableObject);
    }

    public void RegisterPlayer(GameObject playerObject)
    {
        _player = playerObject;
    }

    public void RegisterPlayerCamera(GameObject cameraObject)
    {
        _playerCamera = cameraObject;
    }

    public void AddPlayerDeath()
    {
        _playerDeath++;
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

    public Checkpoint GetCurrentCheckpoint()
    {
        return _currentCheckpoint;
    }

    public GameState GetGameState()
    {
        return _gameState;
    }


    //FUNCTIONS
    //-----------------------------
    //-----------------------------------------------------

    //PUBLIC
    //-----------------------------

    public void SetNewCheckpoint()
    {
        if (_checkpointIndex >= _checkpoints.Count)
            return;

        _checkpointIndex++;
        _currentCheckpoint = _checkpoints[_checkpointIndex];
    }

    public void SetEndLevel()
    {
        PlayerController.Instance.SetCanMove(false);

        //Calcul du score
        bool newScore = false;
        if(_gameTimer < _lastScore)
        {
            _lastScore = (int) _gameTimer;
            PlayerPrefs.SetInt("score", (int) _gameTimer);
            newScore = true;
        }

        _newRecord.gameObject.SetActive(newScore);

        _endLevelHud.gameObject.SetActive(true);

        _recordTextEnd.text = "Temps actuel: " + _lastScore + "s";
        _deadText.text = "Nombre de morts: " + _playerDeath.ToString();

        _gameState = GameState.End;

    }

    public void StartGame()
    {
        if (_gameState != GameState.Intro)
            return;

        _beginLevelHud.gameObject.SetActive(false);

        PlayerController.Instance.SetCanMove(true);
        _gameState = GameState.InGame;
    }

    public void RestartGame()
    {
        if (_gameState != GameState.End)
            return;

        SceneManager.LoadScene(0);
    }


    //PRIVATE
    //-----------------------------


    IEnumerator VariousUpdates()
    {
        while(true)
        {
            if(_gameState == GameState.InGame)
            {
                _gameTimer += Time.deltaTime;
            }
     
            yield return null;
        }
    }



}
