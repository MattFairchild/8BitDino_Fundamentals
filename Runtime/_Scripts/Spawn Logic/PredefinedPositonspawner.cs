/*  (c) 2020 matthew fairchild
 *  
 *  spawner logic that returns positions from a predefined list of positions
 */

using System.Collections.Generic;
using UnityEngine;

public class PredefinedPositonspawner : SpawnLogic
{
    [Header("Positions")]
    [SerializeField, Tooltip("the given list of possible positions that can be returned")]
    private  List<Transform> m_positions = new List<Transform>();
    /// <summary>
    /// the given list of possible positions that can be returned
    /// </summary>
    public List<Transform> Positions
    {
        get{ return m_positions; }
    }

    [Header("Return Logic")]
    [SerializeField, Tooltip("flag that determines whether to return the positions in order (if false, return values will be random)")]
    private bool m_return_ordered = true;
    /// <summary>
    /// flag that determines whether to return the positions in order (if false, return values will be random)
    /// </summary>
    public bool Return_Ordered
    {
        get { return m_return_ordered; }
        set { m_return_ordered = value; }
    }

    public override Vector3 get_spawn_position(out bool n_success, int n_max_tries = 10)
    {
        return m_return_ordered ? get_ordered_position(out n_success) : get_random_position(out n_success);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            bool result = will_collide(new Vector3(0.0f, 1.004251f, 0.713f));
            if (result)
            {
                Debug.Log("It shall be so");
            }
        }
    }

    /// <summary>
    /// get the first position in the list that is free, if there is one (in order of the list)
    /// </summary>
    /// <param name="n_success">flag that indicates if a position was successfully set, or if none was available</param>
    /// <returns>the position of the available spot</returns>
    private Vector3 get_ordered_position(out bool n_success)
    {
        foreach (Transform tf in m_positions)
        {
            if (!will_collide(tf.position))
            {
                n_success = true;
                return tf.position;
            }
        }

        n_success = false;
        return Vector3.zero;
    }
    
    /// <summary>
    /// get a random position from the position list that is free (if there is one)
    /// </summary>
    /// <param name="n_success">flag that indicates if a position was successfully found, or if none was available</param>
    /// <param name="n_max_tries">the maximum number of tries to get a random position</param>
    /// <returns></returns>
    private Vector3 get_random_position(out bool n_success)
    {
        List<int> occupied = new List<int>();

        // try to get a random position without a collsion.
        do
        {
            // get random position from our list
            int rnd_pos = Random.Range(0, m_positions.Count - 1);
            if (!will_collide(m_positions[rnd_pos]))
            {
                n_success = true;
                return m_positions[rnd_pos].position;
            }

            occupied.Add(rnd_pos);
        } while (m_positions.Count < occupied.Count);

        n_success = false;
        return Vector3.zero;
    }
}