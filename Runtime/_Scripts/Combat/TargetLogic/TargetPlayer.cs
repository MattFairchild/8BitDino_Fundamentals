/*  (c) 2019 matthew fairchild
 *  
 *  
 */

using UnityEngine;

namespace EightBitDinosaur
{
	public class TargetPlayer : TargetLogic
	{
	    #region TargetPlayer FUNCTIONS
	
	    /// <summary>
	    /// return player ship object or null if player is not grabbing any ship atm
	    /// </summary>
	    /// <returns>player ship object or null</returns>
	    protected override GameObject get_target()
	    {
	        return GameStatics.Instance.Player;
	    }
	
	    #endregion
	}
}
