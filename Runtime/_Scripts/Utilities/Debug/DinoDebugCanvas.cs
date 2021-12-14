
/*  (c) 2019 matthew fairchild
 *  
    this class visualizes a 3d canvas that can show debug messages et.al.
 */

using System.Collections.Generic;
using UnityEngine;

public class DinoDebugCanvas : MonoBehaviour
{
    #region VARIABLES

    private static DinoDebugCanvas m_instance;
    /// <summary>
    /// singleton instance of level properties
    /// </summary>
    public static DinoDebugCanvas Instance
    {
        get { return m_instance; }
    }

    /// <summary>
    /// parent transform udner which new messages should spawn
    /// </summary>
    [Tooltip("The parent under which new messages will spawn")]
    public Transform m_msg_parent;

    // a dictionary with message hashes to messages
    private Dictionary<System.UInt64, DinoMessage> m_msg_dict;
    private List<DinoMessage> m_msg_list;

    /// <summary>
    ///  max number of messages stored
    /// </summary>
    [Tooltip("The maxmimum number of messages loaded. If collapsed, then the number will still rise")]
    public int m_max_messages = 20;
    
    /// <summary>
    /// whether or not the logs are currently collapsed or not
    /// </summary>
    [Tooltip("Whether the log messages shoulkd be collapsed if there are multiple identical ones")]
    public bool m_collapsed = false;

    // the prefab for the message
    private GameObject m_msg_prefab;

    // filters for making searching specific logs easier
    [Header("Filters")]
    public bool m_show_log = true;
    public bool m_show_assert = true;
    public bool m_show_warning = true;
    public bool m_show_error = true;
    public bool m_show_exception = true;

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

        m_msg_dict = new Dictionary<System.UInt64, DinoMessage>();
        m_msg_list = new List<DinoMessage>();

        m_msg_prefab = Resources.Load<GameObject>("DinoMessage");
    }

    void OnEnable()
    {
        Application.logMessageReceived += log_callback;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= log_callback;
    }

    #endregion

    #region DinoDebugCanvas FUNCTIONS

    public void spawn_msg(LogType n_type, string n_text)
    {
        // if the specific type of log should not be shown, then just return
        if (filtered(n_type))
        {
            return;
        }

        GameObject instance = Instantiate(m_msg_prefab, m_msg_parent);

        DinoMessage msg = instance.GetComponent<DinoMessage>();
        msg.m_text.text = n_text;
        msg.m_type_icon.sprite = Resources.Load<Sprite>(n_type.ToString().ToLower());
        msg.transform.SetAsFirstSibling();
        
        ulong key = CalculateHash(n_type.ToString() + n_text);

        // if we have the message already, then increase the counter, otherwise its new and just add it to dictionary
        if (m_msg_dict.ContainsKey(key))
        {
            m_msg_dict[key].m_count_value++;
            m_msg_dict[key].m_count.text = m_msg_dict[key].m_count_value.ToString();

            msg.gameObject.SetActive(!m_collapsed); // if we collapse and its a msg we already have, the new one starts hidden
        }
        else
        {
            m_msg_dict.Add(key, msg);
        }

        // always add to list, which is the ordered timeline of logs
        m_msg_list.Add(msg);

        //  if the new message has exceeded the max msg count, then remove oldest one
        if (m_msg_list.Count > m_max_messages)
        {
            Destroy(m_msg_list[m_msg_list.Count - 1]);
            m_msg_list.RemoveAt(m_msg_list.Count - 1);
        }
    }

    /// <summary>
    /// this functions collapses multiple identical log messages into a single one with a counter at the end
    /// </summary>
    /// <param name="n_collapse">whether to log state should be collapsed or not</param>
    public void collapse(bool n_collapse)
    {
        m_collapsed = n_collapse;

        foreach (DinoMessage msg in m_msg_list)
        {
            msg.gameObject.SetActive((!n_collapse || msg.m_count_value > 0));
        }
    }

    /// <summary>
    /// set the maximum number of messages that are present in the scene. 
    /// When set, will delete oldest messages until new max number reached
    /// </summary>
    /// <param name="n_num">the new maxmimum number of messages that will be in the scene</param>
    public void set_max_messages(int n_num)
    {
        m_max_messages = n_num;

        while (m_msg_list.Count > m_max_messages)
        {
            Destroy(m_msg_list[m_msg_list.Count - 1]);
            m_msg_list.RemoveAt(m_msg_list.Count - 1);
        }
    }

    /// <summary>
    /// Called when there is an exception by unity. We redirect to simply print it to our canvas
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void log_callback(string condition, string stackTrace, LogType type)
    {
        spawn_msg(LogType.Exception, stackTrace);
        spawn_msg(type, condition);
    }

    /// <summary>
    /// calcualte the hash value for a given string. NOT-Cryptographic
    /// </summary>
    /// <param name="n_msg">the string to has</param>
    /// <returns>resulting hash</returns>
    public static System.UInt64 CalculateHash(string n_msg)
    {
        System.UInt64 hashedValue = 3074457345618258791ul;
        for (int i = 0; i < n_msg.Length; i++)
        {
            hashedValue += n_msg[i];
            hashedValue *= 3074457345618258799ul;
        }
        return hashedValue;
    }

    private bool filtered(LogType n_type)
    {
        switch (n_type)
        {
            case LogType.Log:
                return !m_show_log;
            case LogType.Warning:
                return !m_show_warning;
            case LogType.Assert:
                return !m_show_assert;
            case LogType.Error:
                return !m_show_error;
            case LogType.Exception:
                return !m_show_exception;
        }

        // this should never be reached, all cases should be handled in switch case
        return true;
    }

    #endregion
}
