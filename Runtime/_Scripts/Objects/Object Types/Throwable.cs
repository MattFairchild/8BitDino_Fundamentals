/*  (c) 2019 matthew fairchild
 *  
 *  derived grabbale that will also maintain constant velociy when being released
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitDinosaur
{
	[RequireComponent(typeof(Rigidbody))]
	public class Throwable : Grabable
	{
	    #region VARIABLES
	
	    // the velocity that results from the position in two consecutive frames
	    private Vector3 m_velocity;
	    // variables to calculate the throw direction, since the direction in one single (last) frame is way inprecise
	    private Vector3 m_previous_position;
	
	    [SerializeField]
	    public int m_evaluated_position_count = 5;
	    private Vector3[] m_pos_list;
	    private int m_current_index = 0;
	
	    private Coroutine m_tracking_coroutine;
	
	    #endregion
	
	    #region UNITY LIFECYCLE
	
	    // Start is called before the first frame update
	    new void Awake()
	    {
	        base.Awake();
	
	        // initialize previous position and rb
	        m_previous_position = this.transform.position;
	
	        m_pos_list = new Vector3[m_evaluated_position_count];
	    }
	
	    #endregion
	
	    #region THROWABLE FUNCTIONS
	
	    /// <summary>
	    /// override grab, in which we only call base function and start tracking coroutine
	    /// </summary>
	    /// <param name="n_hand"></param>
	    public override void grab(MotionController n_hand)
	    {
	        base.grab(n_hand);
	
	        // stop any velocity that could be on the object and start tracking positions
	        m_rb.velocity = Vector3.zero;
	        m_tracking_coroutine = StartCoroutine(tracking_coroutine());
	    }
	
	    /// <summary>
	    /// overrides the grabbale release and adds the calculation of velocity to previous frame and starts the movement coroutine (right after base.release)
	    /// </summary>
	    /// <returns>returns whether there was a connected object that we have released ourselves from</returns>
	    public override bool release()
	    {
	        bool parent_ret =  base.release();
	
	        // calculate the direction and velocity, and then set the velocity on the rigidbody accordingly
	        Vector3 direction = m_pos_list[m_evaluated_position_count-1] - m_pos_list[m_evaluated_position_count-2];
	
	        float total_length = 0.0f;
	        for (int i = 1; i < m_evaluated_position_count; i++)
	        {
	            total_length += (m_pos_list[i] - m_pos_list[i - 1]).magnitude; 
	        }
	
	        float speed = total_length / ((m_evaluated_position_count-1) * Time.fixedDeltaTime);
	        
	        m_rb.velocity = direction.normalized * speed;
	
	        StopCoroutine(m_tracking_coroutine);
	
	        return parent_ret;
	    }
	
	    private IEnumerator tracking_coroutine()
	    {
	        while (m_grabbed)
	        {
	            m_pos_list[m_current_index] = this.transform.position;
	            m_current_index = ++m_current_index % m_evaluated_position_count;
	
	            yield return null;
	        }
	    }
	
	    #endregion
	}
}
