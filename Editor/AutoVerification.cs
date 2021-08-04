/*  (c) 2021 matthew fairchild
 *  
 *  automatically check for, and fix, existence of scripts we depend on and use, i.e. singletons that hold data or do some logic for us
*/

using EightBitDinosaur;
using UnityEngine;

[UnityEditor.InitializeOnLoad]
public static class AutoVerification
{
    // object onto which our DIno Scripts will be placed
    private static GameObject m_singletons_parent;

    // variables to hold data for log output
    private static int m_problems_found;
    private static int m_problems_fixed;

    static AutoVerification()
    {
        UnityEditor.SceneManagement.EditorSceneManager.sceneSaving += scene_is_saving;
        UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += scene_opened;
    }

    private static void scene_is_saving(UnityEngine.SceneManagement.Scene n_scene, string n_path)
    {
        verify();
    }

    private static void scene_opened(UnityEngine.SceneManagement.Scene n_scene, UnityEditor.SceneManagement.OpenSceneMode n_mode)
    {
        verify();
    }

    static void verify()
    {
        m_problems_found = 0;
        m_problems_fixed = 0;

        // get a parent transform to better organize all the singletons
        m_singletons_parent = GameObject.Find("---8BitDinosaur---");
        if (m_singletons_parent == null)
        {
            m_singletons_parent = new GameObject("---8BitDinosaur---");
        }
        DontDestroyOnLoadScript ddols = m_singletons_parent.GetComponent<DontDestroyOnLoadScript>() ?? m_singletons_parent.AddComponent<DontDestroyOnLoadScript>();

        verify_layers();
        verify_input();
        verify_gamesettings();
        verify_audio();
        verify_savesystem();
        verify_gamestatics();
        verify_gameevents();

        Debug.Log("8BitDinosaur verification complete");
    }

    /// <summary>
    /// make sure the needed layers are defined
    /// </summary>
    private static void verify_layers()
    {
        LayerVerification.add_layer("Interactable");
        LayerVerification.add_layer("Teleport");
    }

    /// <summary>
    /// make sure that some required scripts are present in the scene (singletons etc.)
    /// </summary>
    private static void verify_input()
    {
        if (GameObject.FindObjectOfType<DinoInput>() == null)
        {
            Debug.LogWarning("DinoInput script not present in the scene. Input might not register.");
            m_problems_found++;
        }
    }

    /// <summary>
    /// make sure we have a gamesettings class in the scene with at least hand preference set
    /// </summary>
    private static void verify_gamesettings()
    {
        // verify existence and setting of game settings
        GameSettings settings = m_singletons_parent.GetComponent<GameSettings>();
        if (settings == null)
        {
            m_singletons_parent.AddComponent<GameSettings>();
        }
    }

    /// <summary>
    /// make sure we have an Audio class in the scene that manages and plays audio
    /// </summary>
    private static void verify_audio()
    {
        AudioManager audio = m_singletons_parent.GetComponent<AudioManager>();
        if (audio == null)
        {
            m_singletons_parent.AddComponent<AudioManager>();
        }
    }

    /// <summary>
    /// make sure we have an Audio class in the scene that manages and plays audio
    /// </summary>
    private static void verify_savesystem()
    {
        SaveSystem savesystem = m_singletons_parent.GetComponent<SaveSystem>();
        if (savesystem == null)
        {
            m_singletons_parent.AddComponent<SaveSystem>();
        }
    }

    /// <summary>
    /// check for game statics existence
    /// </summary>
    private static void verify_gamestatics()
    {
        GameStatics statics = m_singletons_parent.GetComponent<GameStatics>();
        if (statics == null)
        {
            m_singletons_parent.AddComponent<GameStatics>();
        }
    }

    /// <summary>
    /// check for game events existence
    /// </summary>
    private static void verify_gameevents()
    {
        GameEvents gameevents = m_singletons_parent.GetComponent<GameEvents>();
        if (gameevents == null)
        {
            m_singletons_parent.AddComponent<GameEvents>();
        }
    }
}
