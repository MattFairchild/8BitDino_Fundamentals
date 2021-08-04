/*  (c) 2019 matthew fairchild
 *  
 *  Singleton
 *  DontDestroyOnLoad
 *  object to switch to a given level with via the transition level
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    #region VARIABLES

    private static LevelSwitcher m_instance;
    /// <summary>
    /// singleton instance of the combat manager
    /// </summary>
    public static LevelSwitcher Instance
    {
        get { return m_instance; }
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

    #region LevelSwitcher FUNCTIONS

    /// <summary>
    /// initiate the switch to a given level by name, through the transition level
    /// </summary>
    /// <param name="n_new_level_name">the name of the level we want to end up in (not transition level. that is taken care of) </param>
    /// <returns>whether we had to instantiate the LevelSwitcher GO in the process</returns>
    public static bool switch_level(string n_new_level_name, bool n_use_transition = false)
    {
        bool result = false;

        // make sure we have an actual object in the scene
        if (m_instance == null)
        {
            GameObject level_switcher = new GameObject("Level Switcher");
            level_switcher.AddComponent<LevelSwitcher>();
            result = true;
        }

        // make sure the player does not get deleted when switching levels
        DontDestroyOnLoad(GameStatics.Instance.Player);

        // if we want to load transition level first, then load it and cascade the desired level after it. otherwise load the passed level directly
        if (n_use_transition)
        {
            // load the transition level and tell it's completion callback to then start loading the actual level we want
            AsyncOperation async_loading = SceneManager.LoadSceneAsync("TransitionLevel");
            async_loading.allowSceneActivation = true;
            async_loading.completed += (asl) =>
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(n_new_level_name);
                async.allowSceneActivation = true;
            };
        }
        else
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(n_new_level_name);
            async.allowSceneActivation = true;
        }


        return result;
    }

    #endregion
}
