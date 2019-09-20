using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStatsPhase { Alive, Dead}

public class PlayerStats : MonoBehaviour, InputMaster.IPlayerOtherControlsActions
{
    public static PlayerStats Instance = null;

    private InputMaster _inputMaster = null;

    private void Awake()
    {
        Instance = this;

        _inputMaster = new InputMaster();
        _inputMaster.PlayerOtherControls.SetCallbacks(this);
    }

    [Header("Settings")]
    public PlayerStatsPhase playerStatsPhase = PlayerStatsPhase.Alive;
    [Header("Global statistics")]
    public GlobalFloat playerHealth = null;
    [Header("Various references")]
    [SerializeField] private Transform _playerControllerHeadX = null;
    [SerializeField] private Transform _puppetMasterHeadX = null;


    void OnEnable()
    {
        _inputMaster.PlayerOtherControls.Enable();
    }

    void OnDisable()
    {
        _inputMaster.PlayerOtherControls.Disable();
    }

    public void OnRevive(InputAction.CallbackContext context)
    {
        Revive();
    }


    public void TakeDamage(float damage)
    {
        if (playerHealth.RuntimeValue - damage <= 0.0f)
        {
            playerHealth.RuntimeValue = 0.0f;
            Kill();
        }
        else
            playerHealth.RuntimeValue -= damage;
    }

    public void Kill()
    {
        if (playerStatsPhase == PlayerStatsPhase.Dead)
            return;

        playerStatsPhase = PlayerStatsPhase.Dead;

        PlayerController.Instance.SetCanMove(false);
        PlayerInteractions.Instance.SetPlayerCanInteract(false);

        PlayerController.Instance.GetPlayerVirtualCamera().Follow = _puppetMasterHeadX;

        PlayerController.Instance.GetPuppetMaster().state = RootMotion.Dynamics.PuppetMaster.State.Dead;
        PlayerController.Instance.GetPuppetMaster().mappingWeight = 1.0f;
    }

    public void Revive()
    {
        if (playerStatsPhase == PlayerStatsPhase.Alive)
            return;

        PlayerController.Instance.SetCanMove(true);
        PlayerInteractions.Instance.SetPlayerCanInteract(true);

        PlayerController.Instance.ForcePlayerDirection(false);

        PlayerController.Instance.GetPlayerVirtualCamera().Follow = _playerControllerHeadX;

        PlayerController.Instance.GetPuppetMaster().state = RootMotion.Dynamics.PuppetMaster.State.Alive;
        PlayerController.Instance.GetPuppetMaster().mappingWeight = 0.0f;

        playerStatsPhase = PlayerStatsPhase.Alive;

        PlayerSpawn.Instance.TeleportPlayerAtDefaultPosition(true);
    }

    private void Update()
    {
        //Mess with all the updates for the current phase of the playerstats for example
        if (Input.GetKeyDown(KeyCode.P))
            Revive();
    }
}
