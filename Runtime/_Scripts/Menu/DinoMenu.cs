/*  (c) 2019 matthew fairchild
 *  
 *  factory for menus
 *  will load a given menu basic template and fill it with what we need
 */

using UnityEngine;

namespace EightBitDinosaur
{
	public class DinoMenu
	{
	    #region VARIABLES
	
	    private static Menu m_active_menu;
	    public static Menu ActiveMenu
	    {
	        get { return m_active_menu; }
	        private set { m_active_menu = value; }
	    }
	
	    #endregion
	
	    #region MENU FUNCTIONS
	
	    public static GameObject spawn_endpanel()
	    {
	        if (ActiveMenu != null)
	        {
	            ActiveMenu.close();
	            ActiveMenu = null;
	        }
	
	        GameObject result = null;
	        GameObject prefab = Resources.Load<GameObject>("Menu1Button");
	        result = GameObject.Instantiate(prefab);
	
	        ActiveMenu = result.GetComponent<Menu>() ?? result.AddComponent<Menu>();
	        LoadLevel loadlevel_script = result.AddComponent<LoadLevel>();
	
	        ActiveMenu.assign_button(1, () => { loadlevel_script.load_level(0); });
	        
	        return result;
	    }
	
	    #endregion
	}
}
