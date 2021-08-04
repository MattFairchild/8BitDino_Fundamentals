/*  (c) 2020 matthew fairchild
 *  
 *  simple script to make the object persist over scenes.
 *  Used for the singleton parent object
*/

using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    [Tooltip("If true, then make sure only one object with this script and same name can exist at once")]
    public bool m_behave_as_singleton = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (m_behave_as_singleton)
        {
            var dontdestroys = GameObject.FindObjectsOfType<DontDestroyOnLoadScript>();
            foreach (var dd in dontdestroys)
            {
                if (dd.gameObject == this.gameObject) continue;
                if (dd.gameObject.name == this.gameObject.name) Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
