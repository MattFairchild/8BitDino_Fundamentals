/*  (c) 2019 matthew fairchild
    Class that will provide information on what the player has "in focus", with focus being dependant on 
    the origin of this component.
    It can be on the player hand, which will mean focus = what the palyer is pointing at,
    or it could be on the player head, resulting in focus meaning what the player is looking at

    By default, will always look into the gameobjects forward direction
 */

using UnityEngine;

namespace EightBitDinosaur
{
	public enum ObjectType { NORMAL = 0, INTERACTABLE }
	
	/// <summary>
	/// class that holds all information of interest regarding an object in focus
	/// </summary>
	public class PlayerFocus
	{
	    public GameObject m_focused_object;
	    public ObjectType m_object_type;
	}
	
	public class Focus : MonoBehaviour
	{
	    #region VARIABLES
	    
	    private PlayerFocus m_focus;
	    /// <summary>
	    /// the last known focus this script has seen. If not updated continuously, will first update the focus
	    /// </summary>
	    public PlayerFocus CurrentFocus
	    {
	        get
	        {
	            if (!m_update_continuously)
	                update_focus();
	                
	            return m_focus;
	        }
	    }
	
	    private bool m_update_continuously = true;
	    /// <summary>
	    /// whether to update the focus every frame or only upon getting it
	    /// </summary>
	    public bool UpdateContinuously
	    {
	        get { return m_update_continuously; }
	        set { m_update_continuously = value; }
	    }
	
	    private bool m_show_linerenderer = false;
	    /// <summary>
	    /// whether or not to visualize the raycast. Will update the "enabled" status of the linerenderer
	    /// </summary>
	    public bool ShowLinerender
	    {
	        get { return m_show_linerenderer; }
	        set { m_show_linerenderer = m_linerender.enabled = value; }
	    }
	
	    private MotionController m_motioncontroller;
	    /// <summary>
	    /// the motioncontroller 
	    /// </summary>
	    public MotionController Motioncontroller
	    {
	        set { m_motioncontroller = value; }
	    }
	
	    // the distance up to which we will raycast foward to see if we hit something
	    private float m_max_distance = 20.0f;
	
	    // the last raycasthit we have observed
	    private RaycastHit m_hit;
	
	    // line renderer for visualizing the raycast
	    private LineRenderer m_linerender;
	    
	
	    #endregion
	
	    #region UNITY LIFECYCLE
	
	    private void Awake()
	    {
	        // setup the linerender component, only used for visualization
	        GameObject lr_object = new GameObject("LR_Object");
	        lr_object.transform.SetParent(this.transform);
	        lr_object.transform.localPosition = Vector3.zero;
	        lr_object.transform.localRotation = Quaternion.identity;
	
	        m_linerender = lr_object.AddComponent<LineRenderer>();
	        m_linerender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
	        m_linerender.positionCount = 2;
	        m_linerender.SetPositions(new Vector3[] { transform.position, transform.position }); // default all positions to a nothing
	        m_linerender.material = Resources.Load<Material>("Lit/M_Black");
	        m_linerender.startWidth = m_linerender.endWidth = 0.005f;
	        m_linerender.enabled = m_show_linerenderer;
	
	        // I need to initialize focus, because it cannot be null, EVER (as it turns out)
	        PlayerFocus new_focus = new PlayerFocus();
	        new_focus.m_focused_object = this.gameObject;
	        new_focus.m_object_type = ObjectType.NORMAL;
	        m_focus = new_focus;
	    }
	
	    // Update is called once per frame
	    void LateUpdate()
	    {
	        // only update every frame if we have set it to do so
	        if (m_update_continuously)
	        {
	            update_focus();
	        }
	
	        // if we show the line, either cast it up to a hit, or in forward direction with m_max_distance length
	        if (m_show_linerenderer)
	        {
	            m_linerender.SetPosition(0, this.transform.position);
	            m_linerender.SetPosition(1, (m_focus != null ? m_hit.point : this.transform.position + this.transform.forward * m_max_distance));
	        }
	    }
	
	    #endregion
	
	    #region FOCUS FUNCTIONS
	
	    // update focus via raycast in forward direction, save the hit, or null if nothiung hit
	    private void update_focus()
	    {
			// the interactable in focus can be destroyed by some action, so we need to sanitize it just in case
			if (m_focus != null && m_focus.m_focused_object == null)
			{
				m_focus = null;
			}

	        if (Physics.Raycast(new Ray(this.transform.position, this.transform.forward), out m_hit, m_max_distance))
	        {
	            // only update if it is a new, valid object we are pointing at, do not do this for same object multiple times
	            if (m_focus != null && (m_hit.transform.gameObject != m_focus.m_focused_object) && (m_focus.m_object_type == ObjectType.INTERACTABLE))
	            {
	                m_focus.m_focused_object.GetComponent<Interactable>().on_focus_end(m_motioncontroller);
	            }
	
	            // 2/3 build and assign the new focus
	            PlayerFocus new_focus = new PlayerFocus();
	            new_focus.m_focused_object = m_hit.transform.gameObject;
	            new_focus.m_object_type = get_object_type(m_hit.transform.gameObject);
	            m_focus = new_focus;
	
	            // 3/3 check for objects that react to being in focus
	            if (m_focus.m_object_type == ObjectType.INTERACTABLE)
	            {
	                m_focus.m_focused_object.GetComponent<Interactable>().on_focus_start(m_motioncontroller);
	            }
	        }
	        else
	        {
	            // defocus interactable if we were pointing at one
	            if (m_focus?.m_object_type == ObjectType.INTERACTABLE)
	            {
	                m_focus.m_focused_object.GetComponent<Interactable>().on_focus_end(m_motioncontroller);
	            }
	
	            m_focus = null;
	        }
	    }
	
	    /// <summary>
	    /// get the object type of a given object in enum form
	    /// </summary>
	    /// <param name="n_object">the object who's type should be determined</param>
	    /// <returns>the type of the given object</returns>
	    private ObjectType get_object_type(GameObject n_object)
	    {
	        ObjectType result = ObjectType.NORMAL;
	
	        if (n_object.GetComponent<Interactable>() != null)
	        {
	            result = ObjectType.INTERACTABLE;
	        }
	
	
	        return result;
	    }
	
	    #endregion
	}
}
