/*  (c) 2019 matthew fairchild
    This class bundles and manages everything that has to do with the player hands,
    e.g. controller setup, getters for things attached to hands etc.
 */

using UnityEngine;

namespace EightBitDinosaur
{
	public enum EHand { RIGHT = 0, LEFT}

	public class PlayerHands : MonoBehaviour
	{
	    /// <summary>
	    /// getters for hands and controllers in different ways
	    /// </summary>
	    #region HANDS
	
	    [SerializeField]
	    private GameObject m_hand_right;
	
	    [SerializeField]
	    private GameObject m_hand_left;
	
	    private MotionController m_motioncontroller_right;
	    private MotionController m_motioncontroller_left;
	
	    public GameObject RightHand
	    {
	        get { return m_hand_right; }
	    }
	
	    public GameObject LeftHand
	    {
	        get { return m_hand_left; }
	    }
	
	    public MotionController RightController
	    {
	        get { return m_motioncontroller_right; }
	    }
	
	    public MotionController LeftController
	    {
	        get { return m_motioncontroller_left; }
	    }
	
	    public GameObject PrimaryHand
	    {
	        get { return GameSettings.Instance.Hand_Preference == UnityEngine.XR.InputDeviceCharacteristics.Left ? m_hand_left : m_hand_right; }
	    }
	
	    public GameObject SecondaryHand
	    {
	        get { return GameSettings.Instance.Hand_Preference == UnityEngine.XR.InputDeviceCharacteristics.Left ? m_hand_right : m_hand_left; }
	    }
	
	    public MotionController PrimaryController
	    {
	        get { return GameSettings.Instance.Hand_Preference == UnityEngine.XR.InputDeviceCharacteristics.Left ? m_motioncontroller_left : m_motioncontroller_right; }
	    }
	
	    public MotionController SecondaryController
	    {
	        get { return GameSettings.Instance.Hand_Preference == UnityEngine.XR.InputDeviceCharacteristics.Left ? m_motioncontroller_right : m_motioncontroller_left; }
	    }
	
	    #endregion
	
	    //-------------------------------- UNITY LIFECYCLE---------------------------------------
	    #region UNITY LIFECYCLE
	
	    void Awake()
	    {
	        m_motioncontroller_right = m_hand_right.AddComponent<MotionController>();
	        m_motioncontroller_right.Hands = this;
	        m_motioncontroller_right.HandCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Controller | UnityEngine.XR.InputDeviceCharacteristics.Right;
	
	        m_motioncontroller_left = m_hand_left.AddComponent<MotionController>();
	        m_motioncontroller_left.Hands = this;
	        m_motioncontroller_left.HandCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Controller | UnityEngine.XR.InputDeviceCharacteristics.Left;

			Teleport tp = this.gameObject.AddComponent<Teleport>();
			tp.LeftRayOrigin = m_hand_left.transform;
			tp.RightRayOrigin = m_hand_right.transform;
	    }
	
	    #endregion
	}
}
