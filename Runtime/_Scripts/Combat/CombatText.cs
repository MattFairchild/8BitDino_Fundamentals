/*  (c) 2019 matthew fairchild
 *  
 *  simple combat text that will slowly move upwards and become smaller until it destroys itself
 */

using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour
{
    #region VARIABLES

    [Tooltip("the speed at which the text floats upwards. [m/s]")]
    [SerializeField]
    private float m_ascend_speed;
    /// <summary>
    /// the speed at which the text floats upwards. [m/s]
    /// </summary>
    public float AscendSpeed
    {
        set { m_ascend_speed = value; }
    }

    [Tooltip("the lifetime of the text, after which the GO is destroyed. also denotes the time until the scale becomes 0. [sec]")]
    [SerializeField]
    private float m_duration = 3.0f;
    /// <summary>
    /// the lifetime of the text, after which the GO is destroyed. also denotes the time until the scale becomes 0. [sec]
    /// </summary>
    public float Duration
    {
        set { m_duration = value; }
    }

    [Tooltip("reference to the text of the combat message")]
    [SerializeField]
    private TextMeshPro m_text;
    /// <summary>
    /// reference to the text of the combat message
    /// </summary>
    public TextMeshPro Combattext
    {
        get { return m_text; }
    }


    // helper variables that save temporal states
    private Vector3 m_start_scale;
    private Vector3 m_end_scale;
    private float m_duration_copy;
	
	#endregion

	#region UNITY LIFECYCLE
	
    // Start is called before the first frame update
    void Awake()
    {
        m_start_scale = this.transform.localScale;
        m_end_scale = Vector3.zero;
        m_duration_copy = m_duration;
    }

    // Update everything. will be called once per frame via combatmanager
    public void update_combattext()
    {
        if (m_duration <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        this.transform.localScale = Vector3.Slerp(m_start_scale, m_end_scale, (1 - m_duration/m_duration_copy));
        this.transform.Translate(Vector3.up * m_ascend_speed * Time.deltaTime, Space.World);

        // rotate the combat text to look toward the player
        this.transform.LookAt(GameStatics.Instance.PlayerCamera.transform);

        m_duration -= Time.deltaTime;
    }
	
	#endregion
	
	#region CombatText FUNCTIONS
	#endregion
}
