using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatsPhase { Alive, Dead}

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Settings")]
    public PlayerStatsPhase playerStatsPhase = PlayerStatsPhase.Alive;
    [Header("Global statistics")]
    public GlobalFloat playerHealth = null;
    [Header("Various references")]
    [SerializeField] private Transform _puppetMasterHeadX = null;

    public void TakeDamage(float damage)
    {

    }

    public void Kill()
    {
        playerStatsPhase = PlayerStatsPhase.Dead;

        PlayerController.Instance.SetCanMove(false);
        PlayerInteractions.Instance.SetPlayerCanInteract(false);

        PlayerController.Instance.GetPlayerVirtualCamera().Follow = _puppetMasterHeadX;

        PlayerController.Instance.GetPuppetMaster().state = RootMotion.Dynamics.PuppetMaster.State.Dead;
        PlayerController.Instance.GetPuppetMaster().mappingWeight = 1.0f;
    }

    private void Update()
    {
        //Mess with all the updates for the current phase of the playerstats for example

    }
}
