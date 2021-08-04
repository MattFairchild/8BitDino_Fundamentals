/*  (c) 2019 matthew fairchild
    Class that simply makes sure an object is destroyed after x seconds
 */

using UnityEngine;

public class Lifetime : MonoBehaviour
{
    private float m_lifetime;
    /// <summary>
    /// sets the lifetime of the object and activates the countdown
    /// </summary>
    public float Time
    {
        set { m_lifetime = value; m_is_active = true; }
    }

    // by default we start with the countdown inactive, only after setting time, it begins
    private bool m_is_active = false;

    // Update is called once per frame
    void Update()
    {
        if (!m_is_active)
        {
            return;
        }

        m_lifetime -= UnityEngine.Time.deltaTime;

        if (m_lifetime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
