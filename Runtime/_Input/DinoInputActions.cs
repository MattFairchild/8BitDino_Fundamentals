// GENERATED AUTOMATICALLY FROM 'Assets/_Input/DinoInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DinoInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DinoInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DinoInputActions"",
    ""maps"": [
        {
            ""name"": ""gameplay"",
            ""id"": ""5db4ac71-87e7-4dd0-acef-7748d0e70bfe"",
            ""actions"": [
                {
                    ""name"": ""trigger"",
                    ""type"": ""Button"",
                    ""id"": ""9829f060-7494-40d3-8f63-637b19bc7e77"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""grip"",
                    ""type"": ""Button"",
                    ""id"": ""f48a7538-d853-4e6a-8abd-d65b91f75abc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""secondarybutton"",
                    ""type"": ""Button"",
                    ""id"": ""d738e763-ee7b-41db-8f67-f3ab6f3fbf56"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""primarybutton"",
                    ""type"": ""Button"",
                    ""id"": ""90069fb1-685c-404c-97e6-b1810a9d4105"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Thumbstick_R"",
                    ""type"": ""Value"",
                    ""id"": ""003c7fdc-ac3b-49c2-ba84-f77fa7272b7b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Thumbstick_L"",
                    ""type"": ""Value"",
                    ""id"": ""10a5d068-0979-4203-95ef-9f38f29306e0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""59479622-0a81-43c4-a62a-f1e4d8d7714e"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""441b2fcb-7caa-472f-b684-39b1ea3ca94c"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f994145-e9ce-4d57-8a3f-912427f13a90"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""grip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b95980f-5d9f-455b-ae2b-66fc4939fa16"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""grip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3944721-2015-475a-9a1c-0efb0830c40d"",
                    ""path"": ""<XRController>{LeftHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""secondarybutton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""552e7fda-0db1-4931-aed5-52f55c97dd42"",
                    ""path"": ""<XRController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""secondarybutton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""851e0717-d850-49bc-a806-ffb3e483d254"",
                    ""path"": ""<XRController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""primarybutton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e3ca4bc-7d2a-4216-ab80-bf7f05d9a559"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""primarybutton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8cedd26-7941-45b9-9595-c71c61c1b345"",
                    ""path"": ""<XRController>{RightHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thumbstick_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dbb3be68-f00e-439e-892b-81619b1f0b22"",
                    ""path"": ""<XRController>{LeftHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thumbstick_L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // gameplay
        m_gameplay = asset.FindActionMap("gameplay", throwIfNotFound: true);
        m_gameplay_trigger = m_gameplay.FindAction("trigger", throwIfNotFound: true);
        m_gameplay_grip = m_gameplay.FindAction("grip", throwIfNotFound: true);
        m_gameplay_secondarybutton = m_gameplay.FindAction("secondarybutton", throwIfNotFound: true);
        m_gameplay_primarybutton = m_gameplay.FindAction("primarybutton", throwIfNotFound: true);
        m_gameplay_Thumbstick_R = m_gameplay.FindAction("Thumbstick_R", throwIfNotFound: true);
        m_gameplay_Thumbstick_L = m_gameplay.FindAction("Thumbstick_L", throwIfNotFound: true);
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

    // gameplay
    private readonly InputActionMap m_gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_gameplay_trigger;
    private readonly InputAction m_gameplay_grip;
    private readonly InputAction m_gameplay_secondarybutton;
    private readonly InputAction m_gameplay_primarybutton;
    private readonly InputAction m_gameplay_Thumbstick_R;
    private readonly InputAction m_gameplay_Thumbstick_L;
    public struct GameplayActions
    {
        private @DinoInputActions m_Wrapper;
        public GameplayActions(@DinoInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @trigger => m_Wrapper.m_gameplay_trigger;
        public InputAction @grip => m_Wrapper.m_gameplay_grip;
        public InputAction @secondarybutton => m_Wrapper.m_gameplay_secondarybutton;
        public InputAction @primarybutton => m_Wrapper.m_gameplay_primarybutton;
        public InputAction @Thumbstick_R => m_Wrapper.m_gameplay_Thumbstick_R;
        public InputAction @Thumbstick_L => m_Wrapper.m_gameplay_Thumbstick_L;
        public InputActionMap Get() { return m_Wrapper.m_gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @trigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTrigger;
                @trigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTrigger;
                @trigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTrigger;
                @grip.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrip;
                @grip.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrip;
                @grip.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrip;
                @secondarybutton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondarybutton;
                @secondarybutton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondarybutton;
                @secondarybutton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondarybutton;
                @primarybutton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimarybutton;
                @primarybutton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimarybutton;
                @primarybutton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimarybutton;
                @Thumbstick_R.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThumbstick_R;
                @Thumbstick_R.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThumbstick_R;
                @Thumbstick_R.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThumbstick_R;
                @Thumbstick_L.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThumbstick_L;
                @Thumbstick_L.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThumbstick_L;
                @Thumbstick_L.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThumbstick_L;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @trigger.started += instance.OnTrigger;
                @trigger.performed += instance.OnTrigger;
                @trigger.canceled += instance.OnTrigger;
                @grip.started += instance.OnGrip;
                @grip.performed += instance.OnGrip;
                @grip.canceled += instance.OnGrip;
                @secondarybutton.started += instance.OnSecondarybutton;
                @secondarybutton.performed += instance.OnSecondarybutton;
                @secondarybutton.canceled += instance.OnSecondarybutton;
                @primarybutton.started += instance.OnPrimarybutton;
                @primarybutton.performed += instance.OnPrimarybutton;
                @primarybutton.canceled += instance.OnPrimarybutton;
                @Thumbstick_R.started += instance.OnThumbstick_R;
                @Thumbstick_R.performed += instance.OnThumbstick_R;
                @Thumbstick_R.canceled += instance.OnThumbstick_R;
                @Thumbstick_L.started += instance.OnThumbstick_L;
                @Thumbstick_L.performed += instance.OnThumbstick_L;
                @Thumbstick_L.canceled += instance.OnThumbstick_L;
            }
        }
    }
    public GameplayActions @gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnTrigger(InputAction.CallbackContext context);
        void OnGrip(InputAction.CallbackContext context);
        void OnSecondarybutton(InputAction.CallbackContext context);
        void OnPrimarybutton(InputAction.CallbackContext context);
        void OnThumbstick_R(InputAction.CallbackContext context);
        void OnThumbstick_L(InputAction.CallbackContext context);
    }
}
