// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                    ""id"": ""50cc44f8-9028-4082-905d-3e4b045d374b"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""d463bebc-aba0-4648-bfc0-a52c663a07fb"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
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
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""qd"",
                    ""id"": ""55092a71-4deb-4802-9827-26e2f83f53bf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
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
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
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
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""PlayerInteraction"",
            ""id"": ""5b51c9fb-190c-45b9-b8a4-29b42cba8949"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""id"": ""2b9a4acd-87b1-4933-8b8a-58aaa74c5589"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
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
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""PlayerCombat"",
            ""id"": ""a392f44d-243a-4152-a61f-1a77c4864eac"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""id"": ""6ab54a18-0785-43cf-aeac-139bf7930228"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
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
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
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

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

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
    private InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private InputAction m_PlayerMovement_Jump;
    private InputAction m_PlayerMovement_Movement;
    public struct PlayerMovementActions
    {
        private InputMaster m_Wrapper;
        public PlayerMovementActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump { get { return m_Wrapper.m_PlayerMovement_Jump; } }
        public InputAction @Movement { get { return m_Wrapper.m_PlayerMovement_Movement; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
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
    public PlayerMovementActions @PlayerMovement
    {
        get
        {
            return new PlayerMovementActions(this);
        }
    }

    // PlayerInteraction
    private InputActionMap m_PlayerInteraction;
    private IPlayerInteractionActions m_PlayerInteractionActionsCallbackInterface;
    private InputAction m_PlayerInteraction_Interact;
    public struct PlayerInteractionActions
    {
        private InputMaster m_Wrapper;
        public PlayerInteractionActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact { get { return m_Wrapper.m_PlayerInteraction_Interact; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerInteraction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
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
    public PlayerInteractionActions @PlayerInteraction
    {
        get
        {
            return new PlayerInteractionActions(this);
        }
    }

    // PlayerCombat
    private InputActionMap m_PlayerCombat;
    private IPlayerCombatActions m_PlayerCombatActionsCallbackInterface;
    private InputAction m_PlayerCombat_Fire;
    public struct PlayerCombatActions
    {
        private InputMaster m_Wrapper;
        public PlayerCombatActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire { get { return m_Wrapper.m_PlayerCombat_Fire; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerCombat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
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
    public PlayerCombatActions @PlayerCombat
    {
        get
        {
            return new PlayerCombatActions(this);
        }
    }
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
