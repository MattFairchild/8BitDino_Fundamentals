/*  (c) 2021 matthew fairchild
 *  
 *  interactable for 3d/VR menu buttons taht can be clicked on
 */

using System;
using UnityEngine;
using UnityEngine.Events;

namespace EightBitDinosaur
{
	public class ButtonInteractable : Interactable
	{
	    public UnityEvent m_button_event;
	
	    public Action Button_Action
	    {
	        get;
	        set;
	    }
	
	    public Color m_text_hover_color;
	    private Color m_text_default_color;
	
	    public Color m_back_hover_color;
	    [SerializeField]private Color m_back_default_color;
	    private Renderer m_renderer;
	
	    /// <summary>
	    /// Size in percent to which the button will grow when being hovered over
	    /// </summary>
	    [Tooltip("Size in percent to which the button will grow when being hovered over")]
	    public float m_hover_growth = 1.0f;
	    private Vector3 m_default_size;
	
	    // the text that ios displayed on the button
	    private TextMesh m_button_text;
	
	    public override void Awake()
	    {
	        base.Awake();
	
	        m_button_text = this.gameObject.GetComponentInChildren<TextMesh>();
	        m_text_default_color = m_button_text.color;
	
	        m_default_size = this.transform.localScale;
	
	        m_renderer = GetComponent<Renderer>();
	        m_back_default_color = m_renderer.material.GetColor("_Color");
	    }
	
	    public void set_button_text(string n_text)
	    {
	        m_button_text.text = n_text;
	    }
	
	    public override void on_focus_start(MotionController n_hand)
	    {
	        base.on_focus_start(n_hand);
	
	        Debug.Log("focusing button");
	        m_button_text.color = m_text_hover_color;
	
	        this.transform.localScale = m_default_size * m_hover_growth;
	    }
	
	    public override void on_focus_end(MotionController n_hand)
	    {
	        base.on_focus_end(n_hand);
	
	        Debug.Log("defocusing button");
	        m_button_text.color = m_text_default_color;
	
	        this.transform.localScale = m_default_size;
	    }
	
	    public override void on_focus_trigger_pressed(MotionController n_hand)
	    {
	        base.on_focus_trigger_pressed(n_hand);
	        Debug.Log("pressed button");
	
	        m_button_event?.Invoke();
	        Button_Action?.Invoke();
	    }
	}
}
