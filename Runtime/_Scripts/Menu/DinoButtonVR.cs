/*  (c) 2021 matthew fairchild
 *  
 *  interactable for 3d/VR menu buttons that can be clicked on
 */

using System;
using UnityEngine;
using UnityEngine.Events;

namespace EightBitDinosaur
{
	[RequireComponent(typeof(TextMesh))]
	[ExecuteAlways]
	public class DinoButtonVR : VR_UI_Element
	{
		/// <summary>
		/// event to be executed upon clicking on the button
		/// </summary>
	    public UnityEvent m_click_event;
	    public Action Click_Action
	    {
	        get;
	        set;
	    }

        /// <summary>
        /// event to be executed upon first frame in which this button is in focus, i.e. being pointed at
        /// </summary>
        public UnityEvent m_focus_event;
        public Action Focus_Action
        {
            get;
            set;
        }

        /// <summary>
        /// event to be executed upon first frame in which this button stops being in focus of the player, i.e. has stopped pointing at the button
        /// </summary>
        public UnityEvent m_focus_end_event;
        public Action Focus_End_Action
        {
            get;
            set;
        }

		[Tooltip("color of the button text upon having it in focus")]
        public Color m_text_hover_color;
	    private Color m_text_default_color;

		[Tooltip("color of the background upon having it in focus")]
		public Color m_back_hover_color;
	    [SerializeField]private Color m_back_default_color;
	    private MeshRenderer m_renderer;
	
	    /// <summary>
	    /// Size in percent to which the button will grow when being hovered over
	    /// </summary>
	    [Tooltip("Size in percent to which the button will grow when being hovered over")]
	    public float m_hover_growth = 1.0f;
	    private Vector3 m_default_size;
	
	    // the text that ios displayed on the button
	    private TextMesh m_textmesh;
	
	    public override void Awake()
	    {
			base.Awake();

			// set textmesh properties to our default
			m_textmesh = this.gameObject.GetComponent<TextMesh>();
            m_textmesh.anchor = TextAnchor.MiddleCenter;
            m_textmesh.fontSize = 100;
            m_textmesh.transform.localScale = new Vector3(0.0416f, 0.0416f, 0.0416f);

			// get the defaults by grabbing the starting values
            m_renderer = GetComponent<MeshRenderer>();
            m_back_default_color = m_renderer.sharedMaterial.GetColor("_Color");
            m_text_default_color = m_textmesh.color;
	        m_default_size = this.transform.localScale;

            BoxCollider bc = this.gameObject.GetComponent<BoxCollider>();
			bc.size = new Vector3(2*m_renderer.localBounds.extents.x, 2*m_renderer.localBounds.extents.y, 2.5f);
        }

        public void set_button_text(string n_text)
	    {
	        m_textmesh.text = n_text;
	    }
	
	    public override void on_focus_start(MotionController n_hand)
	    {
	        base.on_focus_start(n_hand);
	
	        Debug.Log("focusing button");
	        m_textmesh.color = m_text_hover_color;
	
	        this.transform.localScale = m_default_size * m_hover_growth;

			m_focus_event?.Invoke();
			Focus_Action?.Invoke();
	    }
	
	    public override void on_focus_end(MotionController n_hand)
	    {
	        base.on_focus_end(n_hand);
	
	        Debug.Log("de-focusing button");
	        m_textmesh.color = m_text_default_color;
	
	        this.transform.localScale = m_default_size;

            m_focus_end_event?.Invoke();
            Focus_End_Action?.Invoke();
        }
	
	    public override void on_focus_trigger_pressed(MotionController n_hand)
	    {
	        base.on_focus_trigger_pressed(n_hand);
	        Debug.Log("pressed button");
	
	        m_click_event?.Invoke();
	        Click_Action?.Invoke();
	    }
	}
}
