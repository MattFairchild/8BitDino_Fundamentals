/*  (c) 2020 matthew fairchild
 *  
 *  a class that has general utility functionality for a multitude of use cases
 */

using System.Collections.Generic;
using UnityEngine;

namespace EightBitDinosaur
{
	public static class Utils
	{
        /// <summary>
        /// check whether a given layer is part of a given layermask
        /// </summary>
        public static bool is_in_layermask(int n_layer, LayerMask n_layermask)
        {
            return n_layermask == (n_layermask | (1 << n_layer));
        }

        /// <summary>
        /// TODO: could be a general Utils function that gives points to a trajectory
        /// </summary>
        /// <param name="n_origin"></param>
        /// <returns></returns>
        public static Vector3[] calculat_trajectory(Transform n_origin, float n_velocity, out RaycastHit n_hit)
        {
            n_hit = new RaycastHit();
            if (n_origin == null) return new Vector3[0];

            List<Vector3> points = new List<Vector3>();

            Vector3 velocity = n_origin.forward * n_velocity;
            int threshold = 200; // to cap the iterations

            // two vectors used to always store the last, and the next point calculated on the ray
            Vector3 position_0 = n_origin.transform.position;
            Vector3 position_1 = n_origin.transform.position;
            points.Add(position_0);
            while (threshold > 0)
            {
                // calc new position and velocity x_1 = x_0 + v*t + g*t^2
                position_1 = position_0 + velocity * Time.fixedDeltaTime + 0.5f * Physics.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
                velocity += Physics.gravity * Time.fixedDeltaTime;

                points.Add(position_1);

                Ray ray = new Ray(position_0, position_1 - position_0);
                if (Physics.Raycast(ray, out n_hit, (position_1 - position_0).magnitude))
                {
                    break;
                }

                // from new make old 
                position_0 = position_1;
                threshold--;
            }

            return points.ToArray();
        }

        /// <summary>
        /// get points that will form an arc between start and end, with he arc tilting into the direction of n_arc_direction
        /// </summary>
        /// <param name="n_start"></param>
        /// <param name="n_end"></param>
        /// <param name="n_arc_direction"></param>
        /// <returns></returns>
        public static Vector3[] calculate_arc_between_points(Vector3 n_start, Vector3 n_end, Vector3 n_arc_direction)
        {
            List<Vector3> arc_positions = new List<Vector3>();
            Vector3 midpoint = n_start + (n_end - n_start) * 0.5f + n_arc_direction;

            //Vector3 SampleCurve(Vector3 start, Vector3 end, Vector3 control, float t)
            for (int time = 0; time < 20; time++)
            {
                // Interpolate along line S0: control - start;
                Vector3 Q0 = Vector3.Lerp(n_start, midpoint, time / 20.0f);
                // Interpolate along line S1: S1 = end - control;
                Vector3 Q1 = Vector3.Lerp(midpoint, n_end, time / 20.0f);
                // Interpolate along line S2: Q1 - Q0
                Vector3 Q2 = Vector3.Lerp(Q0, Q1, time / 20.0f);

                arc_positions.Add(Q2);
            }

            return arc_positions.ToArray();
        }
    }

	public static class VectorExtensions
	{
		/// <summary>
		/// check whether the vector is within a given (uniform) range of zero
		/// </summary>
		/// <param name="n_vec">vector to check against</param>
		/// <param name="n_approx_base">the value we checka against, i.e. must be under this value in all directions</param>
		/// <returns></returns>
		public static bool is_approx_zero(this Vector3 n_vec, float n_approx_base)
		{
			if (n_vec.magnitude > n_approx_base) return false;
			return true;
		}

		/// <summary>
		/// check whether the vector is within a given (uniform) range of zero
		/// </summary>
		/// <param name="n_vec">vector to check against</param>
		/// <param name="n_approx_base">the value we checka against, i.e. must be under this value in all directions</param>
		/// <returns></returns>
		public static bool is_approx_zero(this Vector2 n_vec, float n_approx_base)
        {
            if (n_vec.magnitude > n_approx_base) return false;
            return true;
        }
    }
}
