/*  (c) 2019 matthew fairchild
 *  
 *  class for any menus parent object, which has simple and general menu functionality
 */

using System;
using UnityEngine;

namespace EightBitDinosaur
{
	public class Menu : MonoBehaviour
	{
	    // Start is called before the first frame update
	    void Start()
	    {
	        this.gameObject.tag = "Menu";
	        this.transform.position = GameStatics.Instance.PlayerCamera.transform.position + (GameStatics.Instance.PlayerCamera.transform.forward);
	        this.transform.LookAt(GameStatics.Instance.PlayerCamera.transform, Vector3.up);
	
	        GameStatics.Instance.GameRunning = false;
	    }
	
	    /// <summary>
	    /// destroys the menu, but might also have some shutdown logic before
	    /// </summary>
	    public void close()
	    {
	        GameStatics.Instance.GameRunning = true;
	
	        Destroy(this.gameObject);
	    }
	
	    public void assign_button(int n_button_number, Action n_action)
	    {
	        Transform button = transform.Find("Background/Button" + n_button_number);
	        ButtonInteractable button_script = button.gameObject.GetComponent<ButtonInteractable>();
	        button_script.Button_Action += n_action;
	    }
	}
}
