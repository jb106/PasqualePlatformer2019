using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, InputMaster.IPlayerCombatActions
{
    //Private reference to Player Components (assigned in the start function from the GameManager)
    private PlayerController _playerController = null;
    private PlayerInteractions _playerInteractions = null;

    //Other private variables
    private InputMaster _inputMaster = null;
    private float _currentFireTimer = 0.0f;

    void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.PlayerCombat.SetCallbacks(this);
    }

    //------ INTERFACES
    //---------------------------------------
    //-----------------------------------------


    void OnEnable()
    {
        _inputMaster.PlayerCombat.Enable();
    }

    void OnDisable()
    {
        _inputMaster.PlayerCombat.Disable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Fire();
    }


    //-----------------------------------------
    //---------------------------------------
    //------

    private void Start()
    {
        //Getting all player components needed
        _playerController = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        _playerInteractions = GameManager.Instance.GetPlayer().GetComponent<PlayerInteractions>();

        StartCoroutine(WeaponUpdate());
    }


    //The main loop for combat script
    IEnumerator WeaponUpdate()
    {
        while(true)
        {
            //Always increment the fire timer, it is used to get the fire rate of each weapon
            _currentFireTimer += Time.deltaTime;

            yield return null;
        }
    }

    private void Fire()
    {

        //If the player is not carrying any weapon we don't need to go any further
        if (_playerInteractions.CheckIfPlayerIsCarryingSomething())
        {
            if (GetInteractableObject().interactableObjectData.interactableObjectType == InteractableObjectType.Weapon)
            {
                if (_currentFireTimer >= GetInteractableObject().interactableObjectData.fireRate)
                {
                    _playerInteractions._carryingObjectAnimator.SetTrigger("fire");
                    _playerController.GetPlayerModel().gameObject.GetComponent<Recoil>().Fire(1f);

                    //For multiple bullet firing
                    int bulletIndex = 0;

                    while (bulletIndex < GetInteractableObject().interactableObjectData.bulletsNumber)
                    {
                        GameObject newBullet = Instantiate(GetInteractableObject().interactableObjectData.bulletPrefab);
                        newBullet.transform.position = GetInteractableObject().bulletSpawner.position;
                        newBullet.transform.rotation = GetInteractableObject().bulletSpawner.rotation;

                        //Assign a random scale to the projectile
                        float randomScaleValue = Random.Range(-5f, 15f);
                        newBullet.transform.localScale = new Vector3(newBullet.transform.localScale.x + randomScaleValue, newBullet.transform.localScale.y + randomScaleValue, newBullet.transform.localScale.z + randomScaleValue);

                        newBullet.GetComponent<Rigidbody>().AddForce(GetInteractableObject().bulletSpawner.forward * GetInteractableObject().interactableObjectData.bulletSpeed);

                        bulletIndex++;
                    }

                    //Reset the timer for the next shoot
                    _currentFireTimer = 0.0f;
                }
            }

        }
    }

    private InteractableObject GetInteractableObject()
    {
        if (!_playerInteractions.CheckIfPlayerIsCarryingSomething())
            return null;

        return _playerInteractions.GetCurrentInteractableObjectCarried().GetComponent<InteractableObject>();
    }

    
}
