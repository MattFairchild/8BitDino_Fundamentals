/*  (c) 2020 matthew fairchild
 *  
 *  this script saves and loads files from and to binary
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // in default case this is where we will save the file
    private static string SAVEFILE_PATH;

    // default path for config settings
    private static string CONFIG_PATH;

    private static SaveFile m_save;
    /// <summary>
    /// Savefile indicating the items acquired by the player
    /// </summary>
    public static SaveFile Save
    {
        get { return m_save; }
    }

    private static ConfigFile m_config = new ConfigFile();
    /// <summary>
    /// the current settings configuration on this device
    /// </summary>
    public static ConfigFile Config
    {
        get { return m_config; }
    }

    #region UNITY LIFECYCLE

    private void Awake()
    {
        SAVEFILE_PATH = Application.dataPath + "\\SaveFile.dat";
        CONFIG_PATH = Application.dataPath + "\\Config.dat";

        // load up the actual savefile from disk
        m_save = new SaveFile(FileHandler.read_file<bool>(SAVEFILE_PATH));
    }

    private void OnDisable()
    {
        FileHandler.write_file(SAVEFILE_PATH, m_save.m_data);   // dump savefile
        // #matt config dumping                                 // dump config
    }

    #endregion

    #region SAVESYSTEM FUNCTIONS

    /// <summary>
    /// checks for the existence of a given key in the savefile, and if it exists, will return the accompanying value to it
    /// If it does not exist, will add it (assuming that it is new and simply not known yet, i.e. I can start twice and it should be there
    /// </summary>
    /// <param name="n_key">the key which should be checked for</param>
    /// <returns>the value to the given key, or simply false if the eky does not exist</returns>
    public static bool check_savefile(string n_key)
    {
        bool value;

        if (m_save.m_data.TryGetValue(n_key, out value))
        {
            return value;
        }
        else
        {
            // @matt : here we could use a flag in the config file that enables the debug window on the secondary hand e.g.
            Debug.LogWarning("Key " + n_key + " not found, adding it with default value false");
            set_savefile_value(n_key, false);
            return false;
        }
    }

    /// <summary>
    /// writes a given key value pair into the loaded savefile. needs to be dumped for it to then be persistent.
    /// if the key does not exist yet, it will be created. If it does exist, the value will be updated
    /// </summary>
    /// <param name="n_key">key to be added/updated</param>
    /// <param name="n_value">the value to be set</param>
    public static void set_savefile_value(string n_key, bool n_value)
    {
        m_save.set_savefile_value(n_key, n_value);
    }

    #endregion
}
