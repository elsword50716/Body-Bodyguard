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
                    ""type"": ""Value"",
                    ""id"": ""85f90b04-d2a3-430e-a676-d1e694b703e7"",
                    ""expectedControlType"": ""Vector2"",
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
                    ""id"": ""ffee2d5f-e9d7-45ee-8104-eac6f07db611"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""id"": ""01c2b263-25be-4298-b339-bb27dd703c28"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""id"": ""11f51eba-e349-47bf-bc32-72fb3c03b7ec"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""name"": """",
                    ""id"": ""815766a1-0d47-488b-818f-5863a43136da"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""7d98f00e-32e3-4feb-b719-72dadf3fabb0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OnControlParts"",
            ""id"": ""240ece6b-aabb-4cf8-ada8-b792420dfa3a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""49ccf253-0430-4f2f-b649-5c41fede0e09"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9110ea76-6973-4231-8eb5-086958c47589"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""79218e54-6088-41a6-a969-566f2f9fa527"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""42aff43a-f8ae-4be3-9431-27b8d5585ed2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interactions"",
                    ""type"": ""Button"",
                    ""id"": ""6aa13cad-7f34-40d0-a32f-c53a7eb5a010"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""72655fb0-d181-4d12-9ecb-aef38099b33d"",
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
                    ""id"": ""7dc9fee0-3cf3-4934-a022-30edca07a6bb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd0260d0-15e2-409c-9960-4ee95dc0608a"",
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
                    ""id"": ""c00eccc5-88f7-42cb-95f6-270555e9df81"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04ca4f5d-78a3-44b5-abcd-752e0fe7aec7"",
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
                    ""id"": ""65d5af3e-88ad-4382-97fd-a6a5d8dffcaa"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""184d6978-75fe-4c5e-a489-bd691df3b6a9"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interactions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2832606-721e-48d3-99ad-64627839a50b"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interactions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a9af814a-bad8-4f89-816a-1d32f43bbfac"",
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
                    ""id"": ""4b65f330-02b9-4b91-9a4a-f445bdc73c0b"",
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
                    ""id"": ""3585e781-34dc-46de-9849-87ea2c219a33"",
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
                    ""id"": ""53444c23-46fd-4740-a47e-7b348f24bb5e"",
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
                    ""id"": ""55dc5a04-38ab-47e4-b949-7754e544628e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8719b0b0-eb4a-4e75-9d18-5a6811c03a62"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
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
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
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
