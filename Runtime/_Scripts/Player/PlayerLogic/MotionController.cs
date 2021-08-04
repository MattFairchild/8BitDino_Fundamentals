/*  (c) 2019 matthew fairchild
 *  
 *  This class represents a single motion controller. 
 *  It has a representation in the world and initiates events with interact ables, polls for input and sets itself to hand tracked positions
*/

using UnityEngine;
using UnityEngine.XR;

namespace EightBitDinosaur
{ 
    public class MotionController : DinoVRDevice
    {
        #region VARIABLES

        private PlayerHands m_hands;
        /// <summary>
        /// reference to the hands script that manages both hands
        /// </summary>
        public PlayerHands Hands
        {
            set { m_hands = value; }
        }

        private Focus m_focus;
        /// <summary>
        /// the focus component belonging to this motion controller
        /// </summary>
        public Focus ControllerFocus
        {
            get { return m_focus; }
        }

        private bool m_hands_active = true;
        public bool HandsActive
        {
            set{ toggle_hands(value); }
        }

        private CollisionManager m_collision_manager;

        private MeshRenderer[] m_renderers;
        private Collider m_collider;

        #endregion

        #region UNITY LIFECYCLE

        private void Awake()
        {
            // setup the things for collisions to work
            Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.mass = 0.0f;
            rb.isKinematic = true;

            m_focus = this.gameObject.AddComponent<Focus>();
            m_focus.Motioncontroller = this;

            // get the renderers in this object to toggle controller visibility
            m_renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

            m_collider = gameObject.GetComponentInChildren<Collider>();

            // create the object that will manage our interactable collisions
            m_collision_manager = new CollisionManager(this);
        }

        private void OnEnable()
        {
            DinoInput.TriggerAction += trigger_pressed;
            DinoInput.TriggerReleaseAction += trigger_released;
            DinoInput.GrabAction += grip_pressed;
            DinoInput.GrabReleaseAction += grip_released;
            DinoInput.PrimaryButtonPressed += primarybutton_pressed;
            DinoInput.PrimaryButtonReleased += primarybutton_released;
            DinoInput.SecondaryButtonPressed += secondarybutton_pressed;
            DinoInput.SecondaryButtonReleased += secondarybutton_released;
        }

        private void OnDisable()
        {
            DinoInput.TriggerAction -= trigger_pressed;
            DinoInput.TriggerReleaseAction -= trigger_released;
            DinoInput.GrabAction -= grip_pressed;
            DinoInput.GrabReleaseAction -= grip_released;
            DinoInput.PrimaryButtonPressed -= primarybutton_pressed;
            DinoInput.PrimaryButtonReleased -= primarybutton_released;
            DinoInput.SecondaryButtonPressed -= secondarybutton_pressed;
            DinoInput.SecondaryButtonReleased -= secondarybutton_released;
        }

        private void primarybutton_pressed()
        {
            m_collision_manager.ActiveInteractable?.on_overlap_A_pressed(this);
        }

        private void primarybutton_released()
        {
            m_collision_manager.ActiveInteractable?.on_overlap_A_released(this);
        }

        private void secondarybutton_pressed()
        {
            m_collision_manager.ActiveInteractable?.on_overlap_B_pressed(this);
        }

        private void secondarybutton_released()
        {
            m_collision_manager.ActiveInteractable?.on_overlap_B_released(this);
        }

        private void trigger_pressed()
        {
            if (m_collision_manager.ActiveInteractable != null)
            {
                m_collision_manager.ActiveInteractable.on_overlap_trigger_pressed(this);
            }
            else if (m_focus.CurrentFocus?.m_object_type == ObjectType.INTERACTABLE)
            {
                m_focus.CurrentFocus.m_focused_object.GetComponent<Interactable>().on_focus_trigger_pressed(this);
            }
        }

        private void trigger_released()
        {
            if (m_collision_manager.ActiveInteractable != null)
            {
                m_collision_manager.ActiveInteractable.on_overlap_trigger_released(this);
            }
            else if (m_focus.CurrentFocus?.m_object_type == ObjectType.INTERACTABLE)
            {
                m_focus.CurrentFocus.m_focused_object.GetComponent<Interactable>().on_focus_trigger_released(this);
            }
        }

        private void grip_pressed()
        {
            m_collision_manager.ActiveInteractable?.on_overlap_grip_pressed(this);
        }

        private void grip_released()
        {
            m_collision_manager.ActiveInteractable?.on_overlap_grip_released(this);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                on_interactable_collision(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                on_interactable_collision_end(other);
            }
        }

        #endregion

        #region MOTIONCONTROLLER FUNCTIONS

        private void on_interactable_collision(Collider n_interactable)
        {
            Interactable interactable = n_interactable.GetComponent<Interactable>();
            m_collision_manager.begin_collision(interactable);
        }

        private void on_interactable_collision_end(Collider n_interactable)
        {
            Interactable interactable = n_interactable.GetComponent<Interactable>();
            m_collision_manager.end_collision(interactable);
        }

        private void toggle_hands(bool n_visible)
        {
            m_hands_active = n_visible;

            foreach (MeshRenderer mesh in m_renderers)
            {
                mesh.enabled = m_hands_active;
            }

            m_collider.enabled = m_hands_active;
        }

        #endregion

        #region TEMPORARY FUNCTIONS

        #endregion
    }
}