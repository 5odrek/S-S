// GENERATED AUTOMATICALLY FROM 'Assets/Script/InputAssets/ShieldInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ShieldInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShieldInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ShieldInputs"",
    ""maps"": [
        {
            ""name"": ""ShieldActions"",
            ""id"": ""4c7c069f-41c4-41e0-9aac-1f8174566330"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""4cb4c5ec-85ec-4c47-a952-d242d25d7901"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RaiseShield"",
                    ""type"": ""Button"",
                    ""id"": ""a4a91eed-a4ed-469e-93d8-8930b22ef03a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""76229c22-6875-4c05-86ab-163a60e65c35"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ddcc8a77-490d-429e-acf0-b067e96cdc1b"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f8f962c8-67ae-4c75-853e-b1723a91435f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f174a461-6347-4bcf-9e27-f75bc1606fa2"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""461b3deb-79f1-4404-8626-5a61204bf3b4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb93a225-ecf3-41d2-83f8-7b1ec9b9ccdf"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RaiseShield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ShieldActions
        m_ShieldActions = asset.FindActionMap("ShieldActions", throwIfNotFound: true);
        m_ShieldActions_Move = m_ShieldActions.FindAction("Move", throwIfNotFound: true);
        m_ShieldActions_RaiseShield = m_ShieldActions.FindAction("RaiseShield", throwIfNotFound: true);
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

    // ShieldActions
    private readonly InputActionMap m_ShieldActions;
    private IShieldActionsActions m_ShieldActionsActionsCallbackInterface;
    private readonly InputAction m_ShieldActions_Move;
    private readonly InputAction m_ShieldActions_RaiseShield;
    public struct ShieldActionsActions
    {
        private @ShieldInputs m_Wrapper;
        public ShieldActionsActions(@ShieldInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_ShieldActions_Move;
        public InputAction @RaiseShield => m_Wrapper.m_ShieldActions_RaiseShield;
        public InputActionMap Get() { return m_Wrapper.m_ShieldActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShieldActionsActions set) { return set.Get(); }
        public void SetCallbacks(IShieldActionsActions instance)
        {
            if (m_Wrapper.m_ShieldActionsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ShieldActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ShieldActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ShieldActionsActionsCallbackInterface.OnMove;
                @RaiseShield.started -= m_Wrapper.m_ShieldActionsActionsCallbackInterface.OnRaiseShield;
                @RaiseShield.performed -= m_Wrapper.m_ShieldActionsActionsCallbackInterface.OnRaiseShield;
                @RaiseShield.canceled -= m_Wrapper.m_ShieldActionsActionsCallbackInterface.OnRaiseShield;
            }
            m_Wrapper.m_ShieldActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RaiseShield.started += instance.OnRaiseShield;
                @RaiseShield.performed += instance.OnRaiseShield;
                @RaiseShield.canceled += instance.OnRaiseShield;
            }
        }
    }
    public ShieldActionsActions @ShieldActions => new ShieldActionsActions(this);
    public interface IShieldActionsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRaiseShield(InputAction.CallbackContext context);
    }
}
