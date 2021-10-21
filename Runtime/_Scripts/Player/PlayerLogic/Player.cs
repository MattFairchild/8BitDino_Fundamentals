/*  (c) 2019 matthew fairchild
*  
*  the base script that sets up everything for the player, adds all other scripts needed and objects required under this go
*/

using UnityEngine;

namespace EightBitDinosaur
{
	public class Player : MonoBehaviour
	{
	    [SerializeField]
	    private GameObject m_tracked_objects;
	
	    [SerializeField]
	    private GameObject m_cam;
	
	    // reference to the player's hands object
	    private PlayerHands m_hands;

		// VR tracked camera
		private VRCamera m_VR_camera;
	
	    void Awake()
	    {
	        this.gameObject.tag = "Player";
			this.transform.set_layers_recursive(LayerMask.NameToLayer("Dino_Player"));

			if (m_cam.GetComponent<AudioListener>() == null)
	        {
	            m_cam.AddComponent<AudioListener>();
	        }
	
	        m_hands = this.gameObject.GetComponentInChildren<PlayerHands>();
			
			m_VR_camera = m_cam.AddComponent<VRCamera>();
			m_VR_camera.HandCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeadMounted;
        }

        private void Start()
        {
            GameStatics.Instance.Player = this.gameObject;
            GameStatics.Instance.PlayerCamera = m_cam;
        }
    }
}
