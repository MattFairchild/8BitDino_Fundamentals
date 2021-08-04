/*  (c) 2020 matthew fairchild
 *  
 *  representation of a save file that can be saved and loaded
 */

using System.Collections.Generic;

public class SaveFile
{
    public Dictionary<string, bool> m_data;

    public SaveFile()
    {
        m_data = new Dictionary<string, bool>();
    }

    public SaveFile(Dictionary<string, bool> n_data)
    {
        m_data = n_data;
    }

    /// <summary>
    /// sets the value of a given key to the given value. If key does not exist yet it will be added
    /// </summary>
    /// <param name="n_key">the key to set / add</param>
    /// <param name="n_val">the value to be set for the given key</param>
    public void set_savefile_value(string n_key, bool n_val)
    {
        m_data[n_key] = n_val;
    }

    /// <summary>
    /// resets the savefile to the initial default
    /// </summary>
    public void reset()
    {
        // reset all and only make default ship true
        foreach (var pair in m_data)
        {
            m_data[pair.Key] = false;
        }

        set_savefile_value("Ship_Base_1", true);
    }
}
