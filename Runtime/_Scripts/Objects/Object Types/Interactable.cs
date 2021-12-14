/*  (c) 2019 matthew fairchild
 *  
    base class for classes that want to be able to react to:
    - focused (pointed at)
    - de-focused
    - trigger pressed and released while in focus
    - overlap with hand started and ended
    - trigger pressed & released while overlapped with hand
 */

using UnityEngine;

namespace EightBitDinosaur
{
	[RequireComponent(typeof(Collider))]
	public class Interactable : MonoBehaviour
	{
		#region VARIABLES

		protected Collider m_collider;

		#endregion

		#region UNITY LIFECYCLE

		public virtual void Awake()
	    {
	        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
			m_collider = GetComponent<Collider>();
			m_collider.isTrigger = true;
	    }
	
	    #endregion
	
	    #region INTERACTABLE FUNCTIONS
	
		/// <summary>
		/// event called on frame that player hand overlaps with this object'S collider
		/// </summary>
		/// <param name="n_hand">the hand that has overlapped</param>
	    public virtual void on_overlap_start(MotionController n_hand) { GameEvents.execute_interactable_overlapped(this); }

		/// <summary>
		/// event called on frame that player hand stops overlapping with this object'S collider
		/// </summary>
		/// <param name="n_hand">the hand that has overlapped</param>
		public virtual void on_overlap_end(MotionController n_hand){ GameEvents.execute_interactable_overlap_stopped(this); }

        /// <summary>
        /// event called when the trigger button is pressed on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_trigger_pressed(MotionController n_hand){ GameEvents.execute_interactable_trigger_pressed(this); }

        /// <summary>
        /// event called when the trigger button is released on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_trigger_released(MotionController n_hand){ GameEvents.execute_interactable_trigger_released(this); }

        /// <summary>
        /// event called when the grip button is pressed on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_grip_pressed(MotionController n_hand){ GameEvents.execute_interactable_grip_pressed(this); }

        /// <summary>
        /// event called when the grip button is released on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_grip_released(MotionController n_hand){ GameEvents.execute_interactable_grip_released(this); }

        /// <summary>
        /// event called when the A(primary) button is pressed on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_A_pressed(MotionController n_hand){ GameEvents.execute_interactable_A_pressed(this); }

        /// <summary>
        /// event called when the A(primary) button is released on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_A_released(MotionController n_hand){ GameEvents.execute_interactable_A_released(this); }

        /// <summary>
        /// event called when the B(secondary) button is pressed on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_B_pressed(MotionController n_hand){ GameEvents.execute_interactable_B_pressed(this); }

        /// <summary>
        /// event called when the B(secondary) button is released on a motioncontroller object that is currently overlapping with this object's collider
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_overlap_B_released(MotionController n_hand){ GameEvents.execute_interactable_B_released(this); }

        /// <summary>
        /// event called on the frame in which this object starts being in focus, i.e is being pointed at by any of the users hands (collider taken as reference, size will affect this event)
        /// </summary>
        /// <param name="n_hand">the hand that has overlapped</param>
        public virtual void on_focus_start(MotionController n_hand){}

        /// <summary>
        /// event called on the frame in which this object stops being in focus, i.e is being pointed at by any of the users hands (collider taken as reference, size will affect this event)
        /// </summary>
        /// <param name="n_hand">the hand that was focusing</param>
        public virtual void on_focus_end(MotionController n_hand){}

        /// <summary>
        /// event called on the frame in which the trigger button on a motioncontroller object is being pressed while having this object in focus,i.e is being pointed at
        /// </summary>
        /// <param name="n_hand">the hand that is focusing this object</param>
        public virtual void on_focus_trigger_pressed(MotionController n_hand){}

        /// <summary>
        /// event called on the frame in which the trigger button on a motioncontroller object is being released while having this object in focus,i.e is being pointed at
        /// </summary>
        /// <param name="n_hand">the hand that is focusing this object</param>
        public virtual void on_focus_trigger_released(MotionController n_hand){}

        /// <summary>
        /// a way to retrieve interactable object specific information that will vary depending on child implementation
        /// </summary>
        /// <returns>a string with any given info regarding this object</returns>
        protected virtual string interactable_info() { return ""; }
	
	    #endregion
	}
}
