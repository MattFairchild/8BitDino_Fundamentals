/*  (c) 2019 matthew fairchild
    
    class that will handle the overlapped objects of the player to trigger overlap  starts and ends
 */

using System.Collections.Generic;
using UnityEngine;

namespace EightBitDinosaur
{
	public class CollisionManager
	{
	    #region VARIABLES
	
	    private Interactable m_active_interactable;
	    /// <summary>
	    /// the last interactbale we have collided with, thus making it our "active" component
	    /// </summary>
	    public Interactable ActiveInteractable
	    {
	        get { return m_active_interactable; }
	    }
	
	    // the hand that this collision manager belongs to. Set in constructor
	    private MotionController m_hand;
	
	    // needed since we might interact with multiple colliders at once, e.g. a ship with 2 wings on it
	    private List<Interactable> m_interactable_list;
	    #endregion
	
	    #region COLLISIONMANAGER FUNCTIONS
	
	    public CollisionManager(MotionController n_hand)
	    {
	        m_hand = n_hand;
	        m_interactable_list = new List<Interactable>();
	    }
	
	    /// <summary>
	    /// Multis-step process:
	    /// prerequisite: cleanup list, and then make sure we dont already have the interactable in the list => happens after releasing, since collider is turned back on
	    /// 1. active interactable is "turned off", IFF there is already one (since it might be the first collision)
	    /// 2. set the new interactable to be the active one
	    /// 3. add the new interactable to the list. (if it is not there yet)
	    /// 4. "turn on" the newest interactable
	    /// </summary>
	    /// <param name="n_interactable"></param>
	    public void begin_collision(Interactable n_interactable)
	    {
	        cleanup_list();
	        if (m_interactable_list.Contains(n_interactable)) // prerequisite
	        {
	            return;
	        }
	
	        if (m_active_interactable != null)
	        {
	            m_active_interactable.on_overlap_end(m_hand);   // (1)
	        }
	
	        m_active_interactable = n_interactable;         // (2)
	        m_interactable_list.Add(n_interactable);        // (3)
	
	        m_active_interactable.on_overlap_start(m_hand); // (4)

#if DINO_DEBUG
            Debug.Log($"begin collision called with active interactable {m_active_interactable.name}, with interactable list holding {m_interactable_list.Count} items");
#endif
		}

		/// <summary>
		/// when a collision with an interactable has ended, then we have to do multiple things:
		/// - check if it is the active one
		/// - if not, we can simply remove it from our list of overlapping interactables
		/// - if so, call the overlap end on it, and check if we are still colliding with another one of the known interactables
		/// </summary>
		/// <param name="n_interactable"></param>
		public void end_collision(Interactable n_interactable)
	    {
	        // log if this happens, which it should not
	        if (!m_interactable_list.Contains(n_interactable))
	        {
	            Debug.LogError("How did we end an overlap with " + n_interactable.name + " without starting it?");
	            return;
	        }
	
	        if (n_interactable == m_active_interactable)
	        {
	            // end the overlap and remove from list
	            m_active_interactable.on_overlap_end(m_hand);
	            m_interactable_list.Remove(n_interactable);
	
	            // if there is still at least one interactable in the list, we make the most recent one the new active interactable
	            if (m_interactable_list.Count > 0)
	            {
	                // the last, most recent overlap will become the new active interactable
	                m_active_interactable = m_interactable_list[m_interactable_list.Count - 1];
	                m_active_interactable.on_overlap_start(m_hand);
#if DINO_DEBUG
					Debug.Log($"ending collision with active interactable. List has other interactables we are still colliding with. New active interactable is {m_active_interactable.name}");
#endif
				}
				else
	            {
	                m_active_interactable = null;
	            }
	        }
	        else
	        {
	            m_interactable_list.Remove(n_interactable);
	        }

#if DINO_DEBUG
            Debug.Log($"end collision called with for interactable {n_interactable.name}, with interactable list holding {m_interactable_list.Count} items");
#endif
		}

		/// <summary>
		/// remove all null occurences in the list. can happen when grabbing / using objects that destroy themselves upon doing so
		/// </summary>
		private void cleanup_list()
	    {
	        m_interactable_list.RemoveAll(item => item == null);
	    }
	
	    #endregion
	
	}
}
