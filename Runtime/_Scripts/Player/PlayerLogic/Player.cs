/*  (c) 2019 matthew fairchild
*  
*  the base script that sets up everything for the player, adds all other scripts needed and objects required under this go
*/

using UnityEngine;

namespace EightBitDinosaur
{
	public class Player : MonoBehaviour
	{
		[Tooltip("the gameobject that holds all of the tracked objects under it, i.e. camera and hands")]
	    [SerializeField]
	    private GameObject m_tracked_objects;
		/// <summary>
		/// the gameobject that holds all of the tracked objects under it, i.e. camera and hands
		/// </summary>
		public GameObject Tracked_Objects
		{
			get { return m_tracked_objects; }
		}

		[Tooltip("reference to the tracked camera specifically")]
		[SerializeField]
	    private GameObject m_cam;
	
	    // reference to the player hands object
	    private PlayerHands m_hands;
		/// <summary>
		/// script that manages player hand logic and has different properties to retrieve hands
		/// </summary>
		public PlayerHands Hands
		{
			get { return m_hands; }
		}

		// VR tracked camera
		private VRCamera m_VR_camera;
		public VRCamera VR_Camera
		{
			get { return m_VR_camera; }
		}
	
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

            GameStatics.PlayerCamera = m_cam;
            GameStatics.PlayerScript = this;
        }
    }
}
