/*  (c) 2019 matthew fairchild
 *  
 *  Singleton
 *  DontDestroyOnLoad
 *  class that holds runtime references to relevant gameobjects needed by multiple classes
 */

using UnityEngine;

public class GameStatics : MonoBehaviour
{
    #region VARIABLES

    #region singleton
    private static GameStatics m_instance;
    /// <summary>
    /// singleton instance of the combat manager
    /// </summary>
    public static GameStatics Instance
    {
        get { return m_instance; }
    }
    #endregion

    /// <summary>
    /// flag that indicates if the actual game loop is running or not (not = in construction / menu etc.)
    /// </summary>
    public bool GameRunning
    {
        get;
        set;
    }

    /// <summary>
    /// the camera of the player
    /// </summary>
    public GameObject PlayerCamera
    {
        get;
        set;
    }

    /// <summary>
    /// the root player object
    /// </summary>
    public GameObject Player
    {
        get;
        set;
    }

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
    }

    #endregion
}
