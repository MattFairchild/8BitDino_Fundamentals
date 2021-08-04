/*  (c) 2019 matthew fairchild
 *  
 *  SpawnLogic class that spawns within this objects collider.
 *  Can be either a box or sphere collider atm
 */

using UnityEngine;

public class ColliderSpawner : SpawnLogic
{
    #region VARIABLES

    // Collider that will be used to spawn within. Can be assigned, but by default will assign to first collider retrieved from this object
    [SerializeField, Tooltip("Collider that will be used to spawn within. Can be assigned, but by default will assign to first collider retrieved from this object")]
    private Collider m_collider;

    #endregion

    #region UNITY LIFECYCLE

    new void Awake()
    {
        base.Awake();

        // remain the same if already assigned, otherwise retrieve collider
        m_collider = (m_collider != null)? m_collider : this.gameObject.GetComponent<Collider>();

        // if still none found. give an error
        if (m_collider == null)
        {
            Debug.LogError("No Collider could be assigned to ColliderSpawner " + gameObject.name);
        }
    }

    #endregion

    #region ColliderSpawner FUNCTIONS

    /// <summary>
    /// get a spawning position. depending on the collider type, will delegate to appropriate function
    /// </summary>
    /// <param name="n_success">whether or not a position was successfully found</param>
    /// <param name="n_max_tries">the maximum amount of tries before giving up</param>
    /// <returns>position within collider bounds</returns>
    public override Vector3 get_spawn_position(out bool n_success, int n_max_tries = 30)
    {
        Vector3 result = Vector3.zero;

        if (m_collider.GetType() == typeof(BoxCollider))
        {
            return get_spawn_position_box(out n_success, n_max_tries);
        }
        else if (m_collider.GetType() == typeof(SphereCollider))
        {
            return get_spawn_position_sphere(out n_success, n_max_tries);
        }
        else
        {
            // atm we only support box or sphere colliders
            n_success = false;
            return Vector3.zero;
        }
    }

    /// <summary>
    /// function to get a position within a sphere collider
    /// </summary>
    /// <param name="n_success">whether or not a position was successfully found</param>
    /// <param name="n_max_tries">the maximum amount of tries before giving up</param>
    /// <returns>position within collider bounds</returns>
    private Vector3 get_spawn_position_sphere(out bool n_success, int n_max_tries = 30)
    {
        // Assumption: the extent of a bounding box around a sphere collider will match it's radius
        float radius = m_collider.bounds.extents.x;
        Vector3 center = m_collider.bounds.center;
        
        Vector3 result = Vector3.zero;
        int attempt_counter = 0;

        // try to get a random position without a collsion. but at max n_max_tries tries befire giving up
        do
        {
            // get random position within the sphere
            float rnd_y_angle = Random.Range(0.0f, 360.0f);
            float rnd_x_angle = Random.Range(0.0f, 360.0f);
            float distance = Random.Range(0.0f, radius);

            Quaternion rotation = Quaternion.AngleAxis(rnd_x_angle, Vector3.right) * Quaternion.AngleAxis(rnd_y_angle, Vector3.up);
            
            result = center + (rotation * Vector3.forward) * distance;
            result = new Vector3(result.x, Mathf.Max(result.y, m_min_height), result.z);

            attempt_counter++;
        } while (will_collide(result) && attempt_counter < n_max_tries);

        n_success = (attempt_counter < n_max_tries);

        GameObject newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newcube.transform.position = result;
        newcube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        return result;
    }

    /// <summary>
    /// function that retrieves a position from within a box collider
    /// We assume the box to align with world coordinate system. If not, then returned positions will not be accurate, since logic requires checking for AABB
    /// </summary>
    /// <param name="n_success">whether or not a position was successfully found</param>
    /// <param name="n_max_tries">the maximum amount of tries before giving up</param>
    /// <returns>position within collider bounds</returns>
    private Vector3 get_spawn_position_box(out bool n_success, int n_max_tries = 30)
    {
        Vector3 center = m_collider.bounds.center;
        Vector3 bounds = m_collider.bounds.extents;

        Vector3 result = Vector3.zero;
        int attempt_counter = 0;

        // try to get a random position without a collsion. but at max n_max_tries tries befire giving up
        do
        {
            // get random position within the bounds of the defined box
            float rnd_width = center.x + Random.Range(-bounds.x, bounds.x);
            float rnd_height = center.y + Random.Range(-bounds.y, bounds.y);
            float rnd_depth = center.z + Random.Range(-bounds.z, bounds.z);

            result = new Vector3(rnd_width, Mathf.Max(m_min_height, rnd_height), rnd_depth);
            
            attempt_counter++;
        } while (will_collide(result) && attempt_counter < n_max_tries);

        n_success = (attempt_counter < n_max_tries);

        GameObject newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newcube.transform.position = result;
        newcube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        return result;
    }

    private Vector3 get_spawn_position_capsule()
    {
        // TODO @matt IMPLEMENTATION MISSING
        return Vector3.zero;
    }

    private Vector3 get_spawn_position_mesh()
    {
        // TODO @matt IMPLEMENTATION MISSING
        return Vector3.zero;
    }

    #endregion
}
