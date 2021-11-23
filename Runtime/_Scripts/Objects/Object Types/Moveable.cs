/*  (c) 2021 matthew fairchild
    object that can be moved, rotated, scaled, with the possibility of constraining some properties
 */

using System.Collections;
using UnityEngine;

namespace EightBitDinosaur
{
    public enum Moveable_Type { SIMPLE, ADVANCED }

    public class Moveable : Interactable
    {
        public Moveable_Type m_type;

        [Space(10)]

        public bool m_lock_translate;
        public bool m_lock_rotation;

        [Space(10)]

        public float m_rotation_speed = 1.0f;

        private MotionController m_first_hand;
        private Vector3 m_first_hand_position;
        private MotionController m_second_grab;

        protected Coroutine m_move_coroutine;

        /// <summary>
        /// whether the moveable is currently being grabbed / actively being used
        /// </summary>
        public bool Grabbed
        {
            get { return m_first_hand != null; }
        }

        public override void on_overlap_grip_pressed(MotionController n_hand)
        {
            base.on_overlap_trigger_pressed(n_hand);

            if (m_type == Moveable_Type.SIMPLE)
            {
                if (m_first_hand != null) return;

                m_first_hand = n_hand;
                m_first_hand_position = n_hand.transform.position;

                m_move_coroutine = StartCoroutine(simple_routine());
            }
            else
            {

            }

            DinoInput.GrabReleaseAction += on_grip_released;
        }

        public void on_grip_released()
        {
            if (m_type == Moveable_Type.SIMPLE)
            {
                if (m_first_hand != null)
                {
                    StopCoroutine(m_move_coroutine);
                    m_first_hand = null;
                } 
            }
            else
            {

            }

            DinoInput.GrabReleaseAction -= on_grip_released;
        }

        private IEnumerator simple_routine()
        {
            Vector3 initial_hand_pos = m_first_hand_position;
            while (true)
            {
                // position delta
                float y_diff = m_first_hand.transform.position.y - m_first_hand_position.y;

                // rotation
                float angle = Vector3.SignedAngle(m_first_hand_position - this.transform.position,
                    m_first_hand.transform.position - this.transform.position,
                    this.transform.up);

                m_first_hand_position = m_first_hand.transform.position;

                if(!m_lock_translate) this.transform.Translate(0.0f, y_diff, 0.0f);
                if(!m_lock_rotation) this.transform.Rotate(this.transform.up, angle * m_rotation_speed);

                yield return null;
            }
        }
    }
}

