/*  (c) 2021 matthew fairchild
 *  
 *  helper class that wraps the new XR functionality
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace EightBitDinosaur
{
	public static class DinoXR
	{
	    // the device used in XRNode calls. member variable to avoid frequent GC
	    private static List<InputDevice> m_devices = new List<InputDevice>();
	
	    public static bool get_device_rotation(InputDeviceCharacteristics n_characteristics, out Quaternion n_rot)
	    {
	        InputDevices.GetDevicesWithCharacteristics(n_characteristics, m_devices);
	
	        if (m_devices.Count == 0)
	        {
	            Debug.LogWarning($"No devices found with the given characteristics; {n_characteristics}.");
	            n_rot = Quaternion.identity;
	            return false;
	        }
	
	        if (m_devices.Count > 1)
	        {
	            Debug.LogWarning($"More than One Device found with characteristics; {n_characteristics}.\nProceeding with first one found");
	        }
	
	        if (m_devices[0].isValid)
	        {
	            if (m_devices[0].TryGetFeatureValue(CommonUsages.deviceRotation, out n_rot))
	            {
	                return true;
	            }
	        }
	
	        // in this case there was no result for the given device
	        n_rot = Quaternion.identity;
	        return false;
	    }
	
	    public static bool get_device_position(InputDeviceCharacteristics n_characteristics, out Vector3 n_pos)
	    {
	        InputDevices.GetDevicesWithCharacteristics(n_characteristics, m_devices);
	
	        if (m_devices.Count == 0)
	        {
	            Debug.LogWarning($"No devices found with the given characteristics; {n_characteristics}.");
	            n_pos = Vector3.zero;
	            return false;
	        }
	
	        if (m_devices.Count > 1)
	        {
	            Debug.LogWarning($"More than One Device found with characteristics; {n_characteristics}.\nProceeding with first one found");
	        }
	
	        if (m_devices[0].isValid)
	        {
	            if (m_devices[0].TryGetFeatureValue(CommonUsages.devicePosition, out n_pos))
	            {
	                return true;
	            }
	        }
	
	        // in this case there was no result for the given device
	        n_pos = Vector3.zero;
	        return false;
	    }
	}
}
