/*  (c) 2020 matthew fairchild
 *  
 *  Abstraction for all of the Input querying and changes in Input Systems so I do not have to change things everywhere all the time,
 *  since Unity is surely going to be changing everything again in a dew weeks/months.
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace EightBitDinosaur
{
	public class DinoInput : MonoBehaviour
	{
	    //----------------------------------- VARIABLES -----------------------------------------
	    #region VARIABLES
	
	    private static DinoInput m_instance;
	    /// <summary>
	    /// the singleton instance 
	    /// </summary>
	    public DinoInput Instance
	    {
	        get { return m_instance; }
	    }
	
	    // instance of the new input system.
	    // see Assets/_Input/VulcanInput for modifying input declarations
	    DinoInputActions m_input;
	
	    // actions performed on certain input events
	    public static Action TriggerAction;
	    public static Action TriggerReleaseAction;
	    public static Action GrabAction;
	    public static Action GrabReleaseAction;
	    public static Action PrimaryButtonPressed;
	    public static Action PrimaryButtonReleased;
	    public static Action SecondaryButtonPressed;
	    public static Action SecondaryButtonReleased;
	    public static Action<Vector2> ThumbstickR;
	    public static Action<Vector2> ThumbstickL;
	
	    #endregion
	
	    //-------------------------------- UNITY LIFECYCLE---------------------------------------
	    #region UNITY LIFECYCLE
	
	    private void Awake()
	    {
	        // deal with the static instance. standard unity singleton things
	        if (m_instance != null)
	        {
	            Destroy(this);
	        }
	        else
	        {
	            m_instance = this;
	        }
	    }
	
	    private void OnEnable()
	    {
	        // instantiate the input class and bind to its events
	        m_input = new DinoInputActions();
	        m_input.gameplay.trigger.performed += fire_performed;
	        m_input.gameplay.trigger.canceled += fire_canceled;
	        m_input.gameplay.grip.performed += grab_performed;
	        m_input.gameplay.grip.canceled += grab_canceled;
	        m_input.gameplay.primarybutton.performed += primarybutton_performed;
	        m_input.gameplay.primarybutton.canceled += primarybutton_canceled;
	        m_input.gameplay.secondarybutton.performed += secondarybutton_performed;
	        m_input.gameplay.secondarybutton.canceled += secondarybutton_canceled;
	        m_input.gameplay.Thumbstick_R.performed += thumbstick_R_performed;
	        m_input.gameplay.Thumbstick_L.performed += thumbstick_L_performed;
	
	        m_input?.Enable();
	    }
	
	    private void OnDisable()
	    {
	        m_input.gameplay.trigger.performed -= fire_performed;
	        m_input.gameplay.trigger.canceled -= fire_canceled;
	        m_input.gameplay.grip.performed -= grab_performed;
	        m_input.gameplay.grip.canceled -= grab_canceled;
	        m_input.gameplay.primarybutton.performed -= primarybutton_performed;
	        m_input.gameplay.primarybutton.canceled -= primarybutton_canceled;
	        m_input.gameplay.secondarybutton.performed -= secondarybutton_performed;
	        m_input.gameplay.secondarybutton.canceled -= secondarybutton_canceled;
	        m_input.gameplay.Thumbstick_R.performed -= thumbstick_R_performed;
	        m_input.gameplay.Thumbstick_L.performed -= thumbstick_L_performed;
	
	        m_input?.Disable();
	    }
	
	    #endregion
	
	    //------------------------------- CALLBACK FUNCTIONS ------------------------------------
	    #region CALLBACK FUNCTIONS
	
	    private void thumbstick_R_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        Vector2 vec2value = obj.ReadValue<Vector2>();
	        ThumbstickR?.Invoke(vec2value);
	    }
	
	    private void thumbstick_L_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        Vector2 vec2value = obj.ReadValue<Vector2>();
	        ThumbstickL?.Invoke(vec2value);
	    }
	
	    private static void secondarybutton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        SecondaryButtonPressed?.Invoke();
	    }
	
	    private static void secondarybutton_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        SecondaryButtonReleased?.Invoke();
	    }
	
	    private static void primarybutton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        PrimaryButtonPressed?.Invoke();
	    }
	
	    private static void primarybutton_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        PrimaryButtonReleased?.Invoke();
	    }
	
	    private static void grab_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        GrabAction?.Invoke();
	    }
	
	    private static void grab_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        GrabReleaseAction?.Invoke();
	    }
	
	    private static void fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        TriggerAction?.Invoke();
	    }
	
	    private static void fire_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	    {
	        TriggerReleaseAction?.Invoke();
	    }
	
	    #endregion
	
	    //--------------------------------- MISC QUERIES ----------------------------------------
	    #region INPUT QUERYING
	
	    /// <summary>
	    /// query for a boolean input (e.g. button)
	    /// </summary>
	    /// <param name="n_characteristic">characteristics of the device that the input should be queried for (combine with bitwise or operator '|'</param>
	    /// <param name="n_usage">the input to query. Use UnityEngine.XR.CommonUsages for basic inputs</param>
	    /// <returns>boolean value for the given query</returns>
	    public static bool get_input(InputDeviceCharacteristics n_characteristic, InputFeatureUsage<bool> n_usage)
	    {
	        bool result = false;
	
	        List<InputDevice> hand_controllers = new List<InputDevice>();
	        InputDevices.GetDevicesWithCharacteristics(n_characteristic, hand_controllers);
	        foreach (InputDevice ctrl in hand_controllers)
	        {
	            ctrl.TryGetFeatureValue(n_usage, out result); // I'm assuming there will be only one
	        }
	
	        return result;
	    }
	
	    /// <summary>
	    /// query for a float input (e.g. trigger value for how far pressed)
	    /// </summary>
	    /// <param name="n_characteristic">characteristics of the device that the input should be queried for (combine with bitwise or operator '|'</param>
	    /// <param name="n_usage">the input to query. Use UnityEngine.XR.CommonUsages for basic inputs</param>
	    /// <returns>float value for the given query</returns>
	    public static float get_input(InputDeviceCharacteristics n_characteristic, InputFeatureUsage<float> n_usage)
	    {
	        float result = 0.0f;
	
	        List<InputDevice> hand_controllers = new List<InputDevice>();
	        InputDevices.GetDevicesWithCharacteristics(n_characteristic, hand_controllers);
	        foreach (InputDevice ctrl in hand_controllers)
	        {
	            ctrl.TryGetFeatureValue(n_usage, out result); // I'm assuming there will be only one
	        }
	
	        return result;
	    }
	
	    /// <summary>
	    /// query for a 2d Vector input (e.g. analog stick values)
	    /// </summary>
	    /// <param name="n_characteristic">characteristics of the device that the input should be queried for (combine with bitwise or operator '|'</param>
	    /// <param name="n_usage">the input to query. Use UnityEngine.XR.CommonUsages for basic inputs</param>
	    /// <returns>Vector2D value for the given query</returns>
	    public static Vector2 get_input(InputDeviceCharacteristics n_characteristic, InputFeatureUsage<Vector2> n_usage)
	    {
	        Vector2 result = Vector2.zero;
	
	        List<InputDevice> hand_controllers = new List<InputDevice>();
	        InputDevices.GetDevicesWithCharacteristics(n_characteristic, hand_controllers);
	        foreach (InputDevice ctrl in hand_controllers)
	        {
	            ctrl.TryGetFeatureValue(n_usage, out result); // I'm assuming there will be only one
	        }
	
	        return result;
	    }
	
	    #endregion
	}
}
