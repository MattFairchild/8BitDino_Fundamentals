/*  (c) 2019 matthew fairchild
 *  
    Interactable child class dedicated to be able to grab and release objects
 */

using System;
using UnityEngine;
using UnityEngine.Events;

namespace EightBitDinosaur
{
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class Grabable : Interactable
	{
	    #region VARIABLES
	
	    /// <summary>
	    /// whether or not the object should snap to hand position when being grabbed
	    /// </summary>
	    [Tooltip("If true, then the object will snap to the hand transform upon grabbing, otherwise it will stay relative to the grabbed hand")]
	    public bool m_snap_to_hand = false;

		[Tooltip("Flag whether this object should use gravity or not. Also sets trigger flag on collider (trigger != use_gravity)")]
		public bool m_use_gravity = false;

        [Tooltip("whether to turn off collision while grabbing the object")]
        public bool m_turn_off_collision_on_grab = false;

        protected Transform m_initial_parent;
	    protected Rigidbody m_rb;
		
	    protected MotionController m_grabbing_controller;
	    protected bool m_grabbed = false;
	
	    public Action m_release_action;
	    public UnityEvent m_release_event;
	
	    public Action m_grab_action;
	    public UnityEvent m_grab_event;
	
	    #endregion
	
	    #region UNITY LIFECYCLE
	
	    protected new void Awake()
	    {
	        base.Awake();
	
	        m_rb = this.gameObject.GetComponent<Rigidbody>();
			m_rb.useGravity = m_use_gravity;

			m_collider.isTrigger = !m_use_gravity;

			m_initial_parent = this.transform.parent;
	    }
	
	    #endregion
	
	    #region INTERACTABLE OVERRIDDEN FUNCTIONS
	
	    public override void on_overlap_grip_pressed(MotionController n_hand)
	    {
	        base.on_overlap_grip_pressed(n_hand);
	
	        //if we are already grabbed, then release the grip first. Meant for easy switching of hand
	        if (m_grabbing_controller != null)
	        {
	            release();
	        }
	
	        grab(n_hand);
	    }
	
	    public override void on_overlap_grip_released(MotionController n_hand)
	    {
	        base.on_overlap_grip_released(n_hand);
	
	        if (n_hand == m_grabbing_controller)
	        {
	            release();
	        }
	    }
	
	    public override void on_overlap_start(MotionController n_hand)
	    {
	        base.on_overlap_start(n_hand);
	    }
	
	    public override void on_overlap_end(MotionController n_hand)
	    {
	        base.on_overlap_end(n_hand);
	    }
	
	    #endregion
	
	    #region GRABBABLE FUNCTIONS
	
	    /// <summary>
	    /// grab the object by attaching to hand
	    /// </summary>
	    /// <param name="n_hand">the grabbing hand</param>
	    public virtual void grab(MotionController n_hand)
	    {
	        m_grabbing_controller = n_hand;
	        m_grabbed = true;

			m_rb.useGravity = false;

			if (m_turn_off_collision_on_grab)
			{
				m_collider.isTrigger = true;
			}

	        if (m_snap_to_hand)
	        {
	            this.transform.position = n_hand.transform.position;
	            this.transform.rotation = n_hand.transform.rotation;
	        }
	
	        ///*
	        // a bit of a weird hack, as reparenting, even with the worlPOsition = true set that tries to account for parent scales != 1, is wonky
	        // see https://answers.unity.com/questions/1377432/setparent-either-shrinks-my-child-object-or-blows.html
	        // */
	
	        //this.transform.SetParent(null, true);
	
	        ///**/
	
	        this.transform.SetParent(n_hand.transform/*, true*/);
	        
	        n_hand.HandsActive = false;             // turn off controller meshes
	
	        m_grab_action?.Invoke();
	        m_grab_event?.Invoke();
	    }
	
	    /// <summary>
	    /// enforces this object to be released from the grip of a player
	    /// </summary>
	    /// <returns>returns whether there was a connected object that we have released ourselves from</returns>
	    public virtual bool release()
	    {
	        if (!m_grabbed)
	        {
	            return false;
	        }
	
	        m_grabbed = false;
			m_rb.useGravity = m_use_gravity;

            if (m_turn_off_collision_on_grab)
            {
                m_collider.isTrigger = !m_use_gravity;
			}

			bool started_connected = (this.transform.parent != null);
	
	        // releasing a grabable is equal to releasing grip button during collision
	        GameEvents.execute_interactable_grip_released(this);
	
	        // end the grab
	        this.transform.SetParent(m_initial_parent);
	        
	        if (m_grabbing_controller != null)
	        {
	            // turn controller meshes back on
	            m_grabbing_controller.HandsActive = true;
	            m_grabbing_controller = null;
	        }
	
	        m_release_action?.Invoke();
	        m_release_event?.Invoke();
	
	        return (started_connected && this.transform.parent == null);
	    }
	
	    #endregion
	}
}
