// GENERATED AUTOMATICALLY FROM 'Assets/Script/InputAssets/SwordInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SwordInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SwordInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SwordInputs"",
    ""maps"": [
        {
            ""name"": ""SwordActions"",
            ""id"": ""6478556a-c86c-43c3-8570-670a1a673d7c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""d9082bf1-7e36-45e8-818f-55d17d9586f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strike"",
                    ""type"": ""Button"",
                    ""id"": ""f1aecaf4-abe5-437a-a871-05be4cce821f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a9f610da-be7b-40a6-8ff4-29255c0659ce"",
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
                    ""id"": ""cc6677d6-10a0-44b5-a6bc-3d9bcc376922"",
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
                    ""id"": ""298126a1-e727-400f-9233-a427868382bb"",
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
                    ""id"": ""99c66877-9f85-4d18-8f85-3c754f1e2870"",
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
                    ""id"": ""b2bd81ce-6b86-44f4-8d24-e196958b516b"",
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
                    ""id"": ""f22b944f-9e52-48d6-82e3-1adc0b37c55e"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // SwordActions
        m_SwordActions = asset.FindActionMap("SwordActions", throwIfNotFound: true);
        m_SwordActions_Move = m_SwordActions.FindAction("Move", throwIfNotFound: true);
        m_SwordActions_Strike = m_SwordActions.FindAction("Strike", throwIfNotFound: true);
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

    // SwordActions
    private readonly InputActionMap m_SwordActions;
    private ISwordActionsActions m_SwordActionsActionsCallbackInterface;
    private readonly InputAction m_SwordActions_Move;
    private readonly InputAction m_SwordActions_Strike;
    public struct SwordActionsActions
    {
        private @SwordInputs m_Wrapper;
        public SwordActionsActions(@SwordInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_SwordActions_Move;
        public InputAction @Strike => m_Wrapper.m_SwordActions_Strike;
        public InputActionMap Get() { return m_Wrapper.m_SwordActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SwordActionsActions set) { return set.Get(); }
        public void SetCallbacks(ISwordActionsActions instance)
        {
            if (m_Wrapper.m_SwordActionsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_SwordActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_SwordActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_SwordActionsActionsCallbackInterface.OnMove;
                @Strike.started -= m_Wrapper.m_SwordActionsActionsCallbackInterface.OnStrike;
                @Strike.performed -= m_Wrapper.m_SwordActionsActionsCallbackInterface.OnStrike;
                @Strike.canceled -= m_Wrapper.m_SwordActionsActionsCallbackInterface.OnStrike;
            }
            m_Wrapper.m_SwordActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Strike.started += instance.OnStrike;
                @Strike.performed += instance.OnStrike;
                @Strike.canceled += instance.OnStrike;
            }
        }
    }
    public SwordActionsActions @SwordActions => new SwordActionsActions(this);
    public interface ISwordActionsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnStrike(InputAction.CallbackContext context);
    }
}
