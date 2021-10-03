/*  (c) 2020 matthew fairchild
 *  
 *  window that will have some settings used in different Editor functionality by 8BD
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EightBitDinosaur
{
    public class EightBitDinosaurWindow : EditorWindow
    {
        // the name of the file we write the JSON data into
        public static string FILE_NAME = @"EightBitDinosaur_VerificationData";
        
        // json data read from file
        private static Dino_EditorVerificationData m_additional_verification_data;

        private static bool m_dynamic_requirements_region = false;

        /*****************************************************************************************
                                        UNITY LIFECYCLE
         *****************************************************************************************/
        #region UNITY LIFECYCLE

        // open the window
        [MenuItem("EightBitDinosaur/8BD Window")]
        public static void show_window()
        {
            if (System.IO.File.Exists(Application.dataPath + FILE_NAME))
            {
                string json_str = System.IO.File.ReadAllText(Application.dataPath + FILE_NAME);
                m_additional_verification_data = new Dino_EditorVerificationData();
                EditorJsonUtility.FromJsonOverwrite(json_str, m_additional_verification_data);
            }
            else
            {
                m_additional_verification_data = new Dino_EditorVerificationData();
            }

            EditorWindow.GetWindow(typeof(EightBitDinosaurWindow));
        }

        private void OnGUI()
        {
            #region dynamic requirements region
            m_dynamic_requirements_region = EditorGUILayout.Foldout(m_dynamic_requirements_region, "Project Requirements");
            if (m_dynamic_requirements_region)
            {
                draw_list(m_additional_verification_data.m_required_layers, "Required Layers");
                draw_list(m_additional_verification_data.m_required_tags, "Required Tags");
                draw_list(m_additional_verification_data.m_required_scripts, "Required Scripts");

                if (GUILayout.Button("Close without saving"))
                {
                    Close();
                }
                if (GUILayout.Button("Save and Close"))
                {
                    string json_text = EditorJsonUtility.ToJson(m_additional_verification_data);
                    System.IO.File.WriteAllText(Application.dataPath + FILE_NAME, json_text);
                    Close();
                }
            }

            #endregion
        }

        #endregion

        /*****************************************************************************************
                                        WINDOW FUNCTIONS
        *****************************************************************************************/
        #region UNITY LIFECYCLE

        /// <summary>
        /// draw a list of strings that can be set/modified
        /// </summary>
        /// <param name="n_list">the list to draw the contents of</param>
        /// <param name="n_label">the label that is used as headline above it</param>
        private static void draw_list(List<string> n_list, string n_label)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                n_list.Add("");
            }
            EditorGUILayout.LabelField(n_label);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < n_list.Count; /*erase in place means we iterate only in loop when needed*/)
            {
                EditorGUILayout.BeginHorizontal();
                n_list[i] = EditorGUILayout.TextField(n_list[i]);
                if (GUILayout.Button("-"))
                {
                    n_list.RemoveAt(i);
                    continue;
                }
                i++;
                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// draw a list of script references that can be set/modified
        /// </summary>
        /// <param name="n_list">the list to draw the contents of</param>
        /// <param name="n_label">the label that is used as headline above it</param>
        private static void draw_list(List<MonoScript> n_list, string n_label)
        {
            // draw the label for what the list headline will be with a '+' to add to it
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                n_list.Add(null);
            }
            EditorGUILayout.LabelField(n_label);
            EditorGUILayout.EndHorizontal();

            // draw the contents of the list into a list that can be modified
            for (int i = 0; i < n_list.Count; /*erase in place means we iterate only in loop when needed*/)
            {
                EditorGUILayout.BeginHorizontal();
                n_list[i] = EditorGUILayout.ObjectField(n_list[i], typeof(MonoScript), false) as MonoScript;
                if (GUILayout.Button("-"))
                {
                    n_list.RemoveAt(i);
                    continue;
                }
                i++;
                EditorGUILayout.EndHorizontal();
            }
        }

        #endregion
    }

}
