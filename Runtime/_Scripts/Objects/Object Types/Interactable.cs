/*  (c) 2019 matthew fairchild
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
	public class Interactable : MonoBehaviour
	{
	    #region VARIABLES
	    #endregion
	
	    #region UNITY LIFECYCLE
	
	    public virtual void Awake()
	    {
	        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
	    }
	
	    #endregion
	
	    #region INTERACTABLE FUNCTIONS
	
	    public virtual void on_overlap_start(MotionController n_hand) { GameEvents.execute_interactable_overlapped(this); }
	
	    public virtual void on_overlap_end(MotionController n_hand){ GameEvents.execute_interactable_overlap_stopped(this); }
	
	    public virtual void on_overlap_trigger_pressed(MotionController n_hand){ GameEvents.execute_interactable_trigger_pressed(this); }
	
	    public virtual void on_overlap_trigger_released(MotionController n_hand){ GameEvents.execute_interactable_trigger_released(this); }
	
	    public virtual void on_overlap_grip_pressed(MotionController n_hand){ GameEvents.execute_interactable_grip_pressed(this); }
	
	    public virtual void on_overlap_grip_released(MotionController n_hand){ GameEvents.execute_interactable_grip_released(this); }
	
	    public virtual void on_overlap_A_pressed(MotionController n_hand){ GameEvents.execute_interactable_A_pressed(this); }
	
	    public virtual void on_overlap_A_released(MotionController n_hand){ GameEvents.execute_interactable_A_released(this); }
	
	    public virtual void on_overlap_B_pressed(MotionController n_hand){ GameEvents.execute_interactable_B_pressed(this); }
	
	    public virtual void on_overlap_B_released(MotionController n_hand){ GameEvents.execute_interactable_B_released(this); }
	
	    public virtual void on_focus_start(MotionController n_hand){}
	
	    public virtual void on_focus_end(MotionController n_hand){}
	
	    public virtual void on_focus_trigger_pressed(MotionController n_hand){}
	
	    public virtual void on_focus_trigger_released(MotionController n_hand){}
	
	    protected virtual string interactable_info() { return ""; }
	
	    #endregion
	}
}
