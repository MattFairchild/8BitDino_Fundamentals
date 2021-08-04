/*  (c) 2019 matthew fairchild
 *  
 *  SpawnLogic class that spawns within a given box, defined by center, width height and depth
 */

using UnityEngine;

public class BoxSpawner : SpawnLogic
{
    #region VARIABLES

    [Tooltip("the center of the box in which to spawn. By default will always be the position of this gameobject")]
    [SerializeField]
    private Vector3 m_center;
    /// <summary>
    /// the center of the box in which to spawn
    /// </summary>
    public Vector3 Center
    {
        get { return m_center; }
    }

    [Tooltip("the width of the box in which to spawn")]
    [SerializeField]
    private float m_width;
    /// <summary>
    /// the width of the box in which to spawn
    /// </summary>
    public float Width
    {
        get { return m_width; }
    }
    
    [Tooltip("the height of the box in which to spawn")]
    [SerializeField]
    private float m_height;
    /// <summary>
    /// the height of the box in which to spawn
    /// </summary>
    public float Height
    {
        get { return m_height; }
    }

    [Tooltip("the depth of the box in which to spawn")]
    [SerializeField]
    private float m_depth;
    /// <summary>
    /// the depth of the box in which to spawn
    /// </summary>
    public float Depth
    {
        get { return m_depth; }
    }

    #endregion

    #region UNITY LIFECYCLE

    /// <summary>
    /// when Gizmos are turned on in editor scene view, then we can see the defined box
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(m_center, new Vector3(m_width, m_height, m_depth));
    }

    #endregion

    #region BoxSpawner FUNCTIONS

    public override Vector3 get_spawn_position(out bool n_success, int n_max_tries = 30)
    {
        Vector3 result = Vector3.zero;
        int attempt_counter = 0;

        // try to get a random position without a collsion. but at max n_max_tries tries befire giving up
        do
        {
            // get random position within the bounds of the defined box
            float rnd_width = m_center.x + Random.Range(-m_width / 2.0f, m_width / 2.0f);
            float rnd_height = m_center.y + Random.Range(-m_height / 2.0f, m_height / 2.0f);
            float rnd_depth = m_center.z + Random.Range(-m_depth / 2.0f, m_depth / 2.0f);

            result = new Vector3(rnd_width, Mathf.Max(m_min_height, rnd_height), rnd_depth);

            this.transform.position = result;
            
            attempt_counter++;
        } while (will_collide(result) && attempt_counter < n_max_tries);

        n_success = (attempt_counter < n_max_tries);

        return result;
    }

    #endregion
}
