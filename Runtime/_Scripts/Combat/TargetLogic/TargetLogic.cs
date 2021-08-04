/*  (c) 2019 matthew fairchild
 *  
 *  base class for giving an interested component a transform that has been chosen as the result of some desired logic
 */

using UnityEngine;

public abstract class TargetLogic : MonoBehaviour
{
	#region VARIABLES
	
    /// <summary>
    /// the target that results from this object's logic
    /// </summary>
    public GameObject Target
    {
        get { return get_target(); }
    }

    /// <summary>
    /// the position of the target directly. Unspecified behaviour (error) if target is null
    /// </summary>
    public Vector3 TargetPosition
    {
        get { return get_target().transform.position; }
    }
	
	#endregion
    
    #region TargetLogic FUNCTIONS

    /// <summary>
    /// the actual logic that will return a target appropriate for this class' definition
    /// </summary>
    /// <returns></returns>
    protected abstract GameObject get_target();

	#endregion
}
