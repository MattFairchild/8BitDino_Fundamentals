/*  (c) 2021 matthew fairchild
 *  
 *  simple teleport locomotion script, that has an arc for teleportation and can teleport to objects with the defined layer(s)
 *  by default will work with both left and right hand
*/

using System.Collections;
using UnityEngine;

namespace EightBitDinosaur
{
    public class Teleport : MonoBehaviour
    {
        public LayerMask m_teleportable_layers;

        /// <summary>
        /// the object from which the first teleport will originate. Normally a hand, but could be anything
        /// </summary>
        public Transform LeftRayOrigin
        {
            get; 
            set;
        }

        /// <summary>
        /// the object from which the second teleport will originate. Normally a hand, but could be anything
        /// </summary>
        public Transform RightRayOrigin
        {
            get;
            set;
        }

        // hand that is being used for teleport
        EHand m_teleporting_hand;

        public float m_thumbstick_deadzone = 0.15f;
        public float m_velocity = 15.0f;
        private RaycastHit m_hit;
        private Vector3 m_target_position;
        private bool m_can_teleport;

        // objects for holding the actual linerenderer
        private GameObject m_lr_object;
        private LineRenderer m_lr;

        // only one hand can be actively teleporting at at time, which is checked via a single assignable coroutine
        private Coroutine m_coroutine;

        // colors for when teleport is possible and impossible
        private Material m_teleport_mat;

        // the values of the thumbsticks
        private Vector2 m_thumbstick_L_value;
        private Vector2 m_thumbstick_R_value;
        // direction in 3D in which the player is pointing while teleport (calc from m_thumbstick_XY_value depending on hand used)
        private Vector3 m_dir;

        // the circle at the target position of successful teleport
        private GameObject m_teleport_target;

        private void OnEnable()
        {
            DinoInput.ThumbstickL += show_arc_L;
            DinoInput.ThumbstickR += show_arc_R;

            GameEvents.Teleport_Ended += teleport;
        }

        private void OnDisable()
        {
            DinoInput.ThumbstickL -= show_arc_L;
            DinoInput.ThumbstickR -= show_arc_R;

            GameEvents.Teleport_Ended -= teleport;
        }

        private void Awake()
        {
            // init layermask with layers we can teleport onto
            m_teleportable_layers = 1 << LayerMask.NameToLayer("Teleport");

            // load material we have setup specifically for linerenderer
            m_teleport_mat = Resources.Load<Material>("LineRenderer/M_LineRender");

            m_lr_object = new GameObject("TP Linerenderer");
            m_lr_object.transform.position = Vector3.zero;
            m_lr = m_lr_object.AddComponent<LineRenderer>();

            m_lr.material     = m_teleport_mat;
            m_lr.startWidth   = 0.02f;
            m_lr.endWidth     = 0.02f;
            m_lr.startColor   = Color.green;
            m_lr.endColor     = Color.red;
            m_lr.positionCount = 0;

            // load teleport circle for target
            m_teleport_target = Instantiate(Resources.Load<GameObject>("Teleport/TeleportTarget"));
            m_teleport_target.SetActive(false);
        }

        private void show_arc_L(Vector2 n_direction)
        {
            m_thumbstick_L_value = n_direction;

            if (m_coroutine == null && !n_direction.is_approx_zero(m_thumbstick_deadzone))
            {
                m_coroutine = StartCoroutine(show_arc_routine(EHand.LEFT));
            }
        }

        private void show_arc_R(Vector2 n_direction)
        {
            m_thumbstick_R_value = n_direction;

            if (m_coroutine == null && !n_direction.is_approx_zero(m_thumbstick_deadzone))
            {
                m_coroutine = StartCoroutine(show_arc_routine(EHand.RIGHT));
            }
        }

        private IEnumerator show_arc_routine(EHand n_initiated_side)
        {
            m_lr.enabled = true;
            m_teleporting_hand = n_initiated_side;

            while (!(n_initiated_side == EHand.RIGHT ? m_thumbstick_R_value : m_thumbstick_L_value).is_approx_zero(m_thumbstick_deadzone))
            {
                Vector3[] points = Utils.calculat_trajectory((n_initiated_side == EHand.RIGHT ? RightRayOrigin : LeftRayOrigin), m_velocity, out m_hit);
                m_lr.positionCount = points.Length;
                m_lr.SetPositions(points);

                if (m_hit.transform != null && (Utils.is_in_layermask(m_hit.transform.gameObject.layer, m_teleportable_layers)))
                {
                    m_lr.endColor = Color.green;
                    m_teleport_target.SetActive(true);

                    m_dir = n_initiated_side == EHand.RIGHT ? m_thumbstick_R_value : m_thumbstick_L_value;
                    m_teleport_target.transform.SetPositionAndRotation(m_hit.point, Quaternion.Euler(0.0f, (m_teleporting_hand == EHand.RIGHT ? RightRayOrigin : LeftRayOrigin).transform.eulerAngles.y+Mathf.Atan2(m_dir.x, m_dir.y) * Mathf.Rad2Deg, 0.0f));
                }
                else
                {
                    m_lr.endColor = Color.red;
                    m_teleport_target.SetActive(false);
                }
                
                yield return new WaitForFixedUpdate();
            }

            // after releasing teleport, we always stop showing ray, regardless of what we have last hit
            m_teleport_target.SetActive(false);

            // call the teleport ended event IFF hitting a teleport surface, but always stop showing the teleport arc
            if (m_hit.transform != null && m_hit.transform.gameObject.layer == LayerMask.NameToLayer("Teleport"))
            {
                GameEvents.execute_teleport_ended(m_hit.point);
            }

            m_lr.enabled = false;

            m_coroutine = null;
        }

        private void teleport(Vector3 n_position)
        {
            Player player = GameStatics.Instance.PlayerScript;

            // y-rotation-value of hand and teleport rotation/direction combined
            float target_looking_dir = (m_teleporting_hand == EHand.RIGHT ? RightRayOrigin : LeftRayOrigin).transform.rotation.eulerAngles.y + (Mathf.Atan2(m_dir.x, m_dir.y) * Mathf.Rad2Deg);
            // current y-rotation in world space of player camera
            float current_looking_dir = player.VR_Camera.transform.rotation.eulerAngles.y;
            player.Tracked_Objects.transform.Rotate(Vector3.up, target_looking_dir - current_looking_dir, Space.World);

            Vector3 discrepancy = player.transform.position - GameStatics.Instance.PlayerCamera.transform.position;
            player.transform.position = n_position + Vector3.ProjectOnPlane(discrepancy, Vector3.up);
        }
    }
}
