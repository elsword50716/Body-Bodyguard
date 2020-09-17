// GENERATED AUTOMATICALLY FROM 'Assets/InputManager/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7af385c6-d83b-4bfb-bf2a-7c06fa7c59ae"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""85f90b04-d2a3-430e-a676-d1e694b703e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e8d44d8b-5637-44c1-a7b6-b2585e02412b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""4d8e0f04-bf1f-4744-b6e3-454960d52aa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""ecde769d-e7ba-43d6-9faa-695781634a62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interactions"",
                    ""type"": ""Button"",
                    ""id"": ""66ed691a-d251-4677-ade1-ae69c6460e4a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6a060f87-786e-4bda-90b5-581f50de5f97"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a748619b-913e-42e1-94ff-de5a9cbc0270"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88c455ea-4746-4826-a11c-e1aa60e6c16f"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e13505f-4814-450f-a39a-80976233d10a"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interactions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a8d1d8ac-3f90-434f-93bc-6470c2b020c2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1c2c8828-7201-424c-bae0-c230c12da433"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""66cbb0d2-fd5f-4b12-9bbe-2f5a389afe6d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1e790cf5-68ae-4ee4-885a-f7c73e951941"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b0af7646-11f2-4915-8d7a-a8a853ba4443"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""OnControlParts"",
            ""id"": ""20c32727-8fb6-41c7-b233-7d4b9e4e04c7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ff5d7d44-d804-48a4-b19f-0ef07f3172d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a20d9cc6-5bb7-43a5-9070-d2d71cb2a8ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""33b3967e-bdfb-4b28-8c7e-65355801169d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""4de42849-a582-4485-aa9c-0091074f6262"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interactions"",
                    ""type"": ""Button"",
                    ""id"": ""8812a868-3bd5-4c61-b747-bbab054d12b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8d82b231-d24e-4336-b354-47c495b143dd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0358d6b9-b609-4788-ad6e-182041d03f65"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78a907fb-e0be-43b3-bc3b-e4bcf2a648a6"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""521bb910-b611-4000-a9b3-a3407d026a8c"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interactions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""34ff616b-b3b8-405d-ad15-b3b088069b30"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""04694537-6e5c-48f5-b905-731203d37bed"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6ea7aa6b-9658-4c24-9de5-62d6c72963bf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e2f429df-252d-493a-890b-c0eea1ec45b0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4b288d47-cad4-4728-8267-0737c731ce37"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Back = m_Player.FindAction("Back", throwIfNotFound: true);
        m_Player_Interactions = m_Player.FindAction("Interactions", throwIfNotFound: true);
        // OnControlParts
        m_OnControlParts = asset.FindActionMap("OnControlParts", throwIfNotFound: true);
        m_OnControlParts_Move = m_OnControlParts.FindAction("Move", throwIfNotFound: true);
        m_OnControlParts_Jump = m_OnControlParts.FindAction("Jump", throwIfNotFound: true);
        m_OnControlParts_Attack = m_OnControlParts.FindAction("Attack", throwIfNotFound: true);
        m_OnControlParts_Back = m_OnControlParts.FindAction("Back", throwIfNotFound: true);
        m_OnControlParts_Interactions = m_OnControlParts.FindAction("Interactions", throwIfNotFound: true);
    }

    public void Dispose()
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Back;
    private readonly InputAction m_Player_Interactions;
    public struct PlayerActions
    {
        private @InputManager m_Wrapper;
        public PlayerActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Back => m_Wrapper.m_Player_Back;
        public InputAction @Interactions => m_Wrapper.m_Player_Interactions;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Back.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBack;
                @Interactions.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractions;
                @Interactions.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractions;
                @Interactions.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractions;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Interactions.started += instance.OnInteractions;
                @Interactions.performed += instance.OnInteractions;
                @Interactions.canceled += instance.OnInteractions;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // OnControlParts
    private readonly InputActionMap m_OnControlParts;
    private IOnControlPartsActions m_OnControlPartsActionsCallbackInterface;
    private readonly InputAction m_OnControlParts_Move;
    private readonly InputAction m_OnControlParts_Jump;
    private readonly InputAction m_OnControlParts_Attack;
    private readonly InputAction m_OnControlParts_Back;
    private readonly InputAction m_OnControlParts_Interactions;
    public struct OnControlPartsActions
    {
        private @InputManager m_Wrapper;
        public OnControlPartsActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_OnControlParts_Move;
        public InputAction @Jump => m_Wrapper.m_OnControlParts_Jump;
        public InputAction @Attack => m_Wrapper.m_OnControlParts_Attack;
        public InputAction @Back => m_Wrapper.m_OnControlParts_Back;
        public InputAction @Interactions => m_Wrapper.m_OnControlParts_Interactions;
        public InputActionMap Get() { return m_Wrapper.m_OnControlParts; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnControlPartsActions set) { return set.Get(); }
        public void SetCallbacks(IOnControlPartsActions instance)
        {
            if (m_Wrapper.m_OnControlPartsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnAttack;
                @Back.started -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnBack;
                @Interactions.started -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnInteractions;
                @Interactions.performed -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnInteractions;
                @Interactions.canceled -= m_Wrapper.m_OnControlPartsActionsCallbackInterface.OnInteractions;
            }
            m_Wrapper.m_OnControlPartsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Interactions.started += instance.OnInteractions;
                @Interactions.performed += instance.OnInteractions;
                @Interactions.canceled += instance.OnInteractions;
            }
        }
    }
    public OnControlPartsActions @OnControlParts => new OnControlPartsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnInteractions(InputAction.CallbackContext context);
    }
    public interface IOnControlPartsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnInteractions(InputAction.CallbackContext context);
    }
}
