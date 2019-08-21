// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMaster : IInputActionCollection
{
    private InputActionAsset asset;
    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""3ae040d5-c0e2-4c75-a4b3-f26efd89ac03"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""50cc44f8-9028-4082-905d-3e4b045d374b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""d463bebc-aba0-4648-bfc0-a52c663a07fb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5c30602f-358e-4a53-985e-83732bb934c6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acf6608d-695a-4fb9-b559-20773f9e0f69"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""qd"",
                    ""id"": ""55092a71-4deb-4802-9827-26e2f83f53bf"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9b069167-abb7-4ea3-bf28-0ed9ee90cc48"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""dbf14efe-6c9a-49d4-9e02-b67ac8898f1d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""arrows"",
                    ""id"": ""16496565-990c-4a07-9bc5-cf76bbf6d19b"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4c3924cd-d2d3-4d8a-a5e5-c1ea9925d584"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""80f1f04e-5874-444d-a75b-ea623e14b1b0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1966dd0c-509f-45ee-a801-855841dc7a2a"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInteraction"",
            ""id"": ""5b51c9fb-190c-45b9-b8a4-29b42cba8949"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Value"",
                    ""id"": ""2b9a4acd-87b1-4933-8b8a-58aaa74c5589"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d80412f7-6aca-43f9-85c5-037b52e57414"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerCombat"",
            ""id"": ""a392f44d-243a-4152-a61f-1a77c4864eac"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Value"",
                    ""id"": ""6ab54a18-0785-43cf-aeac-139bf7930228"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3fe6b238-9899-4225-8a86-f771e374a05a"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.GetActionMap("PlayerMovement");
        m_PlayerMovement_Jump = m_PlayerMovement.GetAction("Jump");
        m_PlayerMovement_Movement = m_PlayerMovement.GetAction("Movement");
        // PlayerInteraction
        m_PlayerInteraction = asset.GetActionMap("PlayerInteraction");
        m_PlayerInteraction_Interact = m_PlayerInteraction.GetAction("Interact");
        // PlayerCombat
        m_PlayerCombat = asset.GetActionMap("PlayerCombat");
        m_PlayerCombat_Fire = m_PlayerCombat.GetAction("Fire");
    }

    ~InputMaster()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Movement;
    public struct PlayerMovementActions
    {
        private InputMaster m_Wrapper;
        public PlayerMovementActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerInteraction
    private readonly InputActionMap m_PlayerInteraction;
    private IPlayerInteractionActions m_PlayerInteractionActionsCallbackInterface;
    private readonly InputAction m_PlayerInteraction_Interact;
    public struct PlayerInteractionActions
    {
        private InputMaster m_Wrapper;
        public PlayerInteractionActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_PlayerInteraction_Interact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInteraction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInteractionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInteractionActions instance)
        {
            if (m_Wrapper.m_PlayerInteractionActionsCallbackInterface != null)
            {
                Interact.started -= m_Wrapper.m_PlayerInteractionActionsCallbackInterface.OnInteract;
                Interact.performed -= m_Wrapper.m_PlayerInteractionActionsCallbackInterface.OnInteract;
                Interact.canceled -= m_Wrapper.m_PlayerInteractionActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerInteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                Interact.started += instance.OnInteract;
                Interact.performed += instance.OnInteract;
                Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerInteractionActions @PlayerInteraction => new PlayerInteractionActions(this);

    // PlayerCombat
    private readonly InputActionMap m_PlayerCombat;
    private IPlayerCombatActions m_PlayerCombatActionsCallbackInterface;
    private readonly InputAction m_PlayerCombat_Fire;
    public struct PlayerCombatActions
    {
        private InputMaster m_Wrapper;
        public PlayerCombatActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_PlayerCombat_Fire;
        public InputActionMap Get() { return m_Wrapper.m_PlayerCombat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerCombatActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerCombatActions instance)
        {
            if (m_Wrapper.m_PlayerCombatActionsCallbackInterface != null)
            {
                Fire.started -= m_Wrapper.m_PlayerCombatActionsCallbackInterface.OnFire;
                Fire.performed -= m_Wrapper.m_PlayerCombatActionsCallbackInterface.OnFire;
                Fire.canceled -= m_Wrapper.m_PlayerCombatActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_PlayerCombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                Fire.started += instance.OnFire;
                Fire.performed += instance.OnFire;
                Fire.canceled += instance.OnFire;
            }
        }
    }
    public PlayerCombatActions @PlayerCombat => new PlayerCombatActions(this);
    public interface IPlayerMovementActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IPlayerInteractionActions
    {
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IPlayerCombatActions
    {
        void OnFire(InputAction.CallbackContext context);
    }
}
