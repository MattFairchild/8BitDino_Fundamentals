/*  (c) 2020 matthew fairchild
 *  
 *  window that will have some settings used in different Editor functionality by 8BD
 */

using UnityEngine;
using UnityEditor;

public class EightBitDinosaurWindow : EditorWindow
{
    // open the window
    [MenuItem("EightBitDinosaur/8BD Window")]
    public static void show_window()
    {
        EditorWindow.GetWindow(typeof(EightBitDinosaurWindow));
    }

    private void OnGUI()
    {
        // close button at the bottom to simply close this window again
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Close"))
        {
            Close();
        }
    }
}
