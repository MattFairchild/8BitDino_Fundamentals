/*  (c) 2019 matthew fairchild
 *  
 *  this class holds properties that are relevant for a single scene
*/

using System.Collections;
using UnityEngine;

namespace EightBitDinosaur
{
	public class LevelProperties : MonoBehaviour
	{
	    //----------------------------------- VARIABLES -----------------------------------------
	    #region VARIABLES
	
	    #region singleton
	    private static LevelProperties m_instance;
	    /// <summary>
	    /// singleton instance of the combat manager
	    /// </summary>
	    public static LevelProperties Instance
	    {
	        get { return m_instance; }
	    }
	    #endregion
	    
	    private bool m_running = false;
	    /// <summary>
	    /// whether the level is running
	    /// </summary>
	    public bool Running
	    {
	        get { return m_running; }
	    }
	
	    private bool m_paused = false;
	    /// <summary>
	    /// whether the level is paused. Will take the 'running' flag into consideration. Can only be true if level is running as well as being paused
	    /// </summary>
	    public bool Paused
	    {
	        get { return m_paused; }
	    }
	
	    // the coroutine that runs and manages the level timer, spawn timer etc.
	    private Coroutine m_coroutine;
	
	    #endregion
	
	    //-------------------------------- UNITY LIFECYCLE---------------------------------------
	    #region UNITY LIFECYCLE 
	
	    private void Awake()
	    {
	        if (m_instance != null)
	        {
	            Destroy(this.gameObject);
	        }
	        else
	        {
	            m_instance = this;
	        }
	
	        this.gameObject.tag = "LevelProperties";
	    }
	
	    private void OnDisable()
	    {
	        if (m_instance == this)
	        {
	            m_instance = null;
	        }
	
	        if (m_coroutine != null)
	        {
	            StopCoroutine(m_coroutine);
	        }
	    }
	
	    #endregion
	
	    //-------------------------- LEVELPROPERTGIES FUNCTIONS ---------------------------------
	    #region LEVELPROPERTIES FUNCTIONS
	
	    /// <summary>
	    /// the main loop that will run and take care of everything we need to be continuously updated
	    /// </summary>
	    /// <returns></returns>
	    private IEnumerator run_level()
	    {
	        while (true)
	        {
	            // the level has to be running and not paused for us to do the work
	            if (m_running || !m_paused)
	            {
	                
	            }
	
	            yield return new WaitForEndOfFrame();
	        }
	    }
	
	    #endregion
	}
}
