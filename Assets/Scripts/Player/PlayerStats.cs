using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RootMotion.FinalIK;
using UnityEngine.UI;

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
    [SerializeField] private HitReaction _hitReaction = null;
    [SerializeField] private Image _healthBar = null;


    //Getters
    public HitReaction GetHitReaction() { return _hitReaction; }

    private float _timerSinceDead = 0.0f;
    private float _timerSinceRevive = 0.0f;


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
        //Verification to kill or just substract health from the player
        if (playerHealth.RuntimeValue - damage <= 0.0f)
        {
            playerHealth.RuntimeValue = 0.0f;
            Kill();
        }
        else
            playerHealth.RuntimeValue -= damage;

        UpdateHealthBarUI();
    }

    public void Kill()
    {
        if (playerStatsPhase == PlayerStatsPhase.Dead)
            return;

        playerStatsPhase = PlayerStatsPhase.Dead;

        PlayerController.Instance.SetCanMove(false);
        PlayerInteractions.Instance.SetPlayerCanInteract(false);

        PlayerCamera.Instance.SetDeadCamera();

        PlayerController.Instance.GetPuppetMaster().state = RootMotion.Dynamics.PuppetMaster.State.Dead;
        PlayerController.Instance.GetPuppetMaster().mappingWeight = 1.0f;

        _timerSinceDead = 0.0f;
    }

    public void Revive()
    {
        if (_timerSinceDead <= PlayerSpawn.Instance.GetDelayToRevive())
            return;

        if (playerStatsPhase == PlayerStatsPhase.Alive)
            return;

        PlayerController.Instance.SetRagdollEmitSplash(false);

        PlayerController.Instance.ResetPlayerRigidBodyMass();

        PlayerController.Instance.SetCanMove(true);
        PlayerInteractions.Instance.SetPlayerCanInteract(true);

        PlayerController.Instance.ForcePlayerDirection(false);

        PlayerCamera.Instance.SetAliveCamera();

        PlayerController.Instance.GetPuppetMaster().state = RootMotion.Dynamics.PuppetMaster.State.Alive;
        PlayerController.Instance.GetPuppetMaster().mappingWeight = 0.0f;

        playerStatsPhase = PlayerStatsPhase.Alive;

        PlayerSpawn.Instance.TeleportPlayerAtDefaultPosition();

        playerHealth.RuntimeValue = playerHealth.GetMaxValue();
        UpdateHealthBarUI();

        PlayerInteractions.Instance.startButton.GetComponent<Animator>().SetBool("pop", false);

        _timerSinceRevive = 0.0f;
    }

    private void Update()
    {
        if (playerStatsPhase == PlayerStatsPhase.Dead)
        {
            _timerSinceDead += Time.deltaTime;

            if(_timerSinceDead >= PlayerSpawn.Instance.GetDelayToRevive())
                PlayerInteractions.Instance.startButton.GetComponent<Animator>().SetBool("pop", true);
        }
        else if(playerStatsPhase == PlayerStatsPhase.Alive)
        {
            _timerSinceRevive += Time.deltaTime;

            //Timer to reactivate splash short time after revive, because it was causing bugs with the ragdoll
            if (_timerSinceRevive > 1f && PlayerController.Instance.GetRagdollEmitSplash() == false)
                PlayerController.Instance.SetRagdollEmitSplash(true);
        }
    }

    private void UpdateHealthBarUI()
    {
        _healthBar.fillAmount = playerHealth.RuntimeValue / playerHealth.GetMaxValue();
    }
}
