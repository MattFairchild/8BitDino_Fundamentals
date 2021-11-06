/*  (c) 2019 matthew fairchild
 *  
 *  singleton class 
 *  deals with anything related to combat that needs to be globally spawned or setup in the scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitDinosaur
{
	public class CombatManager : MonoBehaviour
	{
	    #region VARIABLES
	
	    private static CombatManager m_instance;
	    /// <summary>
	    /// singleton instance of the combat manager
	    /// </summary>
	    public static CombatManager Instance
	    {
	        get { return m_instance; }
	    }
	
	    // prefab for the combat text. setup of scales, font sizes etc would be too much dynamically
	    [SerializeField]
	    private GameObject m_combattext_go;
	
	    // list of all the spawned combat text
	    private List<CombatText> m_combat_text_list;
	
	    // variable to hold the coroutine, just in case
	    private Coroutine m_update_routine;
	
	    #endregion
	
	    #region UNITY LIFECYCLE
	
	    // Start is called before the first frame update
	    void Awake()
	    {
	        if (m_instance != null)
	        {
	            Destroy(this.gameObject);
	        }
	        else
	        {
	            m_instance = this;
	        }
	
	        m_combattext_go = m_combattext_go ?? Resources.Load<GameObject>("Combattext");
	        m_combat_text_list = new List<CombatText>();
	
	        m_update_routine = StartCoroutine(update_coroutine());
	    }
	
	    // on shutting this down (e.g. when level changes), turn off the coroutine. New level should have its own CombatManager that will start up one again
	    private void OnDisable()
	    {
	        if (m_update_routine != null)
	        {
	            StopCoroutine(m_update_routine);
	        }
	    }
	
	    #endregion
	
	    #region CombatManager FUNCTIONS
	
	    /// <summary>
	    /// spawns a combat text object
	    /// </summary>
	    /// <param name="n_position">the position at which to spawn the combat text message</param>
	    /// <param name="n_look_at">the position towards which the combat text forward direction should point to (quasi fake billboarding)</param>
	    /// <param name="n_combat_text">the text of the combat message</param>
	    /// <param name="n_color">the color of the text. default white</param>
	    /// <param name="n_lifetime">the lifetime of the combat message before it gets destroyed. default 1 seconds</param>
	    /// <param name="n_speed">the speed at which the text moves upwards. default 0.3 m/S</param>
	    public void spawn_combattext(Vector3 n_position, Vector3 n_look_at, string n_combat_text, Color n_color, float n_lifetime = 1.0f, float n_speed = 0.25f)
	    {
	        GameObject inst = Instantiate(m_combattext_go);
	        inst.transform.position = n_position;
	
	        CombatText text = inst.GetComponent<CombatText>();
	        text.Combattext.text = n_combat_text;
	        text.AscendSpeed = n_speed;
	        text.Duration = n_lifetime;
	        text.Combattext.color = n_color;
	
	        // add combat text to the list
	        m_combat_text_list.Add(text);
	    }
	
	    /// <summary>
	    /// coroutine that is the substitute to update method that will manage all the continuous operations
	    /// </summary>
	    /// <returns></returns>
	    private IEnumerator update_coroutine()
	    {
	        while (true)
	        {
	            // remove all dead enemies from the list
	            m_combat_text_list.RemoveAll(item => item == null);
	
	            // go through all enemies and update them
	            foreach (CombatText ct in m_combat_text_list)
	            {
	                ct.update_combattext();
	            }
	
	            yield return new WaitForEndOfFrame();
	        }
	    }
	
	    #endregion
	}
}
