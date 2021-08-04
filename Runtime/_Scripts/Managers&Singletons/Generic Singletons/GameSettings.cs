/*  (c) 2019 matthew fairchild
 *  
 *  Singleton Class
 *  DontDestroyOnLoad
 *  Settings class for scene independent settings that apply to the entire app
*/

using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings m_instance;
    /// <summary>
    /// singleton instance of level properties
    /// </summary>
    public static GameSettings Instance
    {
        get { return m_instance; }
    }

    [SerializeField]
    private UnityEngine.XR.InputDeviceCharacteristics m_hand_preference = UnityEngine.XR.InputDeviceCharacteristics.Right;
    /// <summary>
    /// handedness of the user
    /// </summary>
    public UnityEngine.XR.InputDeviceCharacteristics Hand_Preference
    {
        get { return m_hand_preference; }
    }

    //-------------------------------- UNITY LIFECYCLE---------------------------------------
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
    }

    #endregion
}
