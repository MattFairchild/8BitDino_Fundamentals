using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace EightBitDinosaur
{
	public class DinoVRDevice : MonoBehaviour
	{
        // used as temp holders for updating device positions and rotations
        private Vector3 m_temp_pos;
        private Quaternion m_temp_rot;

        [SerializeField]
        protected InputDeviceCharacteristics m_hand_characteristics;
        /// <summary>
        /// the characteristics of this controller. we will always give it controller combined with either left or right
        /// </summary>
        public InputDeviceCharacteristics HandCharacteristics
        {
            get { return m_hand_characteristics; }
            set { m_hand_characteristics = value; }
        }

        private void Update()
        {
            if (DinoXR.get_device_position(m_hand_characteristics, out m_temp_pos)) this.transform.localPosition = m_temp_pos;
            if (DinoXR.get_device_rotation(m_hand_characteristics, out m_temp_rot)) this.transform.localRotation = m_temp_rot;
        }
    }
}
