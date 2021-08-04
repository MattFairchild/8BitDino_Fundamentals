/*  (c) 2019 matthew fairchild
 *  
 *  base class for any spawner logic that gives the caller new positions to spawn at
 */

using UnityEngine;

public abstract class SpawnLogic : MonoBehaviour
{
    #region VARIABLES

    // the spherecollider on the position helper
    protected SphereCollider m_helper_collider;

    /// <summary>
    /// defines a minimum height at which the object should spawn at
    /// </summary>
    protected float m_min_height = -1000.0f;

    #endregion

    #region UNITY LIFECYCLE

    protected void Awake()
    {
        m_helper_collider = this.gameObject.AddComponent<SphereCollider>();
        m_helper_collider.isTrigger = true;
        m_helper_collider.radius = 0.1f;

        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    #endregion

    #region SpawnLogic FUNCTIONS

    public abstract Vector3 get_spawn_position(out bool n_success, int n_max_tries = 10);

    #endregion

    #region Helpers

    /// <summary>
    /// returns whether the given transform is at a position to collide with something
    /// </summary>
    /// <param name="n_transform">the transform to check a collision for</param>
    /// <returns>true if colliding with something, false if no collision detected</returns>
    public bool will_collide(Transform n_transform)
    {
        return Physics.CheckSphere(n_transform.position, 0.1f); // layermask ~0 means everything
    }

    /// <summary>
    /// returns whether the given position collides with something
    /// </summary>
    /// <param name="n_position">the position to check a collision for</param>
    /// <returns>true if colliding with something, false if no collision detected</returns>
    public bool will_collide(Vector3 n_position)
    {
        return Physics.CheckSphere(n_position, 0.1f); // layermask ~0 means everything
    }

    #endregion
}
