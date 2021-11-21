/*  (c) 2019 matthew fairchild
 *  
 *  Singleton
 *  DontDestroyOnLoad
 *  class that has a number of events that can be bound to from anywhere
 */

using System;
using UnityEngine;
using UnityEngine.Events;

namespace EightBitDinosaur
{
    public class GameEvents : MonoBehaviour
    {
        #region VARIABLES

        #region singleton
        private static GameEvents m_instance;
        /// <summary>
        /// singleton instance of the combat manager
        /// </summary>
        public static GameEvents Instance
        {
            get { return m_instance; }
        }
        #endregion

        [Tooltip("events to be executed when the game is started, i.e. ship construction phase has ended")]
        public UnityEvent m_game_start;
        /// <summary>
        /// events to be executed when the game is started, i.e. ship construction phase has ended
        /// </summary>
        public static Action Game_Start
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the game stops, i.e. all waves have been completed or playership was destroyed")]
        public UnityEvent m_game_stop;
        /// <summary>
        /// events to be executed when the game stops, i.e. all waves have been completed or playership was destroyed
        /// </summary>
        public static Action Game_Stop
        {
            get;
            set;
        }

        #region INTERACTABLE GLOBAL EVENTS

        [Tooltip("events to be executed when the player grips an interactable")]
        public static UnityEvent<Interactable> m_interactable_grip_pressed;
        /// <summary>
        /// events to be executed when the player grips an interactable
        /// </summary>
        public static Action<Interactable> Interactable_Grip_Pressed
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player releases an interactable")]
        public static UnityEvent<Interactable> m_interactable_grip_released;
        /// <summary>
        /// events to be executed when the player releases an interactable
        /// </summary>
        public static Action<Interactable> Interactable_Grip_Released
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player hand overlaps with an interactable")]
        public static UnityEvent<Interactable> m_interactable_overlapped;
        /// <summary>
        /// events to be executed when the player hand overlaps with an interactable
        /// </summary>
        public static Action<Interactable> Interactable_Overlapped
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player hand stops overlapping with an interactable")]
        public static UnityEvent<Interactable> m_interactable_overlap_stopped;
        /// <summary>
        /// events to be executed when the player hand stops overlapping with an interactable
        /// </summary>
        public static Action<Interactable> Interactable_Overlap_Stopped
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player triggers an interactable")]
        public static UnityEvent<Interactable> m_interactable_trigger_pressed;
        /// <summary>
        /// events to be executed when the player triggers an interactable
        /// </summary>
        public static Action<Interactable> Interactable_Trigger_Pressed
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player triggers an interactable")]
        public static UnityEvent<Interactable> m_interactable_trigger_released;
        /// <summary>
        /// events to be executed when the player releases trigger on an interactable
        /// </summary>
        public static Action<Interactable> Interactable_Trigger_Released
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player pressed primary button on an interactable")]
        public static UnityEvent<Interactable> m_interactable_A_pressed;
        /// <summary>
        /// events to be executed when the player pressed primary button on an interactable
        /// </summary>
        public static Action<Interactable> Interactable_A_Pressed
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player releases primary button on an interactable")]
        public static UnityEvent<Interactable> m_interactable_A_released;
        /// <summary>
        /// events to be executed when the player pressed primary button on an interactable
        /// </summary>
        public static Action<Interactable> Interactable_A_Released
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player pressed secondary button on an interactable")]
        public static UnityEvent<Interactable> m_interactable_B_pressed;
        /// <summary>
        /// events to be executed when the player pressed secondary button on an interactable
        /// </summary>
        public static Action<Interactable> Interactable_B_Pressed
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player releases secondary button on an interactable")]
        public static UnityEvent<Interactable> m_interactable_B_released;
        /// <summary>
        /// events to be executed when the player pressed secondary button on an interactable
        /// </summary>
        public static Action<Interactable> Interactable_B_Released
        {
            get;
            set;
        }

        #endregion

        [Tooltip("events to be executed when an interactable dispatches some information")]
        public static UnityEvent<string> m_gameplay_info_dispatch;
        /// <summary>
        /// events to be executed when an interactable dispatches some information
        /// </summary>
        public static Action<string> Gameplay_Info_Dispatch
        {
            get;
            set;
        }

        [Tooltip("events to be executed when the player has (successfully) called the teleport")]
        public static UnityEvent<Vector3> m_teleport_ended;
        /// <summary>
        /// events to be executed when an interactable dispatches some information
        /// </summary>
        public static Action<Vector3> Teleport_Ended
        {
            get;
            set;
        }

        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            if (m_instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                m_instance = this;
            }
        }

        #region EVENT FUNCTIONS

        /// <summary>
        /// static function
        /// trigger all of the events bound to game start
        /// </summary>
        public static void start_game()
        {
            if (Game_Start != null)
            {
                Game_Start();
            }
            m_instance.m_game_start?.Invoke();

            GameStatics.GameRunning = true;
        }

        /// <summary>
        /// static function
        /// indicates that a running game, i.e. a number of waves, has ended and the player is returning to contrcuction/menu mode
        /// trigger all of the events bound to game stopping
        /// </summary>
        public static void stop_game()
        {
            if (Game_Stop != null)
            {
                Game_Stop();
            }
            m_instance.m_game_stop?.Invoke();

            GameStatics.GameRunning = false;
        }

        public static void execute_interactable_grip_pressed(Interactable n_interactable)
        {
            if (m_interactable_grip_pressed != null)
            {
                m_interactable_grip_pressed.Invoke(n_interactable);
            }

            if (Interactable_Grip_Pressed != null)
            {
                Interactable_Grip_Pressed(n_interactable);
            }
        }

        public static void execute_interactable_grip_released(Interactable n_interactable)
        {
            if (m_interactable_grip_released != null)
            {
                m_interactable_grip_released.Invoke(n_interactable);
            }

            if (Interactable_Grip_Released != null)
            {
                Interactable_Grip_Released(n_interactable);
            }
        }
        public static void execute_interactable_overlapped(Interactable n_interactable)
        {
            if (m_interactable_overlapped != null)
            {
                m_interactable_overlapped.Invoke(n_interactable);
            }

            if (Interactable_Overlapped != null)
            {
                Interactable_Overlapped(n_interactable);
            }
        }

        public static void execute_interactable_overlap_stopped(Interactable n_interactable)
        {
            if (m_interactable_overlap_stopped != null)
            {
                m_interactable_overlap_stopped.Invoke(n_interactable);
            }

            if (Interactable_Overlap_Stopped != null)
            {
                Interactable_Overlap_Stopped(n_interactable);
            }
        }

        public static void execute_interactable_trigger_pressed(Interactable n_interactable)
        {
            if (m_interactable_trigger_pressed != null)
            {
                m_interactable_trigger_pressed.Invoke(n_interactable);
            }

            if (Interactable_Trigger_Pressed != null)
            {
                Interactable_Trigger_Pressed(n_interactable);
            }
        }

        public static void execute_interactable_trigger_released(Interactable n_interactable)
        {
            if (m_interactable_trigger_released != null)
            {
                m_interactable_trigger_released.Invoke(n_interactable);
            }

            if (Interactable_Trigger_Released != null)
            {
                Interactable_Trigger_Released(n_interactable);
            }
        }

        public static void execute_interactable_A_pressed(Interactable n_interactable)
        {
            if (m_interactable_A_pressed != null)
            {
                m_interactable_A_pressed.Invoke(n_interactable);
            }

            if (Interactable_A_Pressed != null)
            {
                Interactable_A_Pressed(n_interactable);
            }
        }

        public static void execute_interactable_A_released(Interactable n_interactable)
        {
            if (m_interactable_A_released != null)
            {
                m_interactable_A_released.Invoke(n_interactable);
            }

            if (Interactable_A_Released != null)
            {
                Interactable_A_Released(n_interactable);
            }
        }

        public static void execute_interactable_B_pressed(Interactable n_interactable)
        {
            if (m_interactable_B_pressed != null)
            {
                m_interactable_B_pressed.Invoke(n_interactable);
            }

            if (Interactable_B_Pressed != null)
            {
                Interactable_B_Pressed(n_interactable);
            }
        }

        public static void execute_interactable_B_released(Interactable n_interactable)
        {
            if (m_interactable_B_released != null)
            {
                m_interactable_B_released.Invoke(n_interactable);
            }

            if (Interactable_B_Released != null)
            {
                Interactable_B_Released(n_interactable);
            }
        }

        public static void execute_gameplay_info_dispatch(string n_val)
        {
            m_gameplay_info_dispatch?.Invoke(n_val);
            Gameplay_Info_Dispatch?.Invoke(n_val);
        }

        public static void execute_teleport_ended(Vector3 n_teleport_position)
        {
            m_teleport_ended?.Invoke(n_teleport_position);
            Teleport_Ended?.Invoke(n_teleport_position);
        }

        #endregion
    }
}
