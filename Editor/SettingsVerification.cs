
using UnityEditor;

/***********************************************************************************/
/*------------------------------LAYER VERIFICATION---------------------------------*/
/***********************************************************************************/

/// <summary>
/// this class deals with all verification, checking adding etc. of anything to do with layers
/// </summary>
public class LayerVerification
{

    /// <summary>
    /// checks for the existence of a ayer with a given name
    /// </summary>
    /// <param name="n_name">name of layer we want to check for</param>
    /// <returns>whether a layer with the given name already exists</returns>
    public static bool is_layer_defined(string n_name)
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProperty = serializedObject.FindProperty("layers");

        // run through all layers and check for the given name
        for (int i = 8; i < layersProperty.arraySize; i++)
        {
            SerializedProperty sp = layersProperty.GetArrayElementAtIndex(i);

            // the first empty layer is filled with "TeleportLayer"
            if (sp.stringValue == n_name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// adds a layer with the given name to the tag manager IFF there is no layer with that name yet
    /// -> by default it will be added to the first empty slot in the layers manager. 
    /// If given an index, we place the new layer into the given index (and override any existing layer if necessary)
    /// </summary>
    /// <param name="n_name">the name for the new layer we will add</param>
    /// <param name="n_layerindex">OPTIONAL! will place layer to this index, if one is passed in. Must be in Range [8,32] or will be clamped</param>
    public static void add_layer(string n_name, int n_layerindex = -1)
    {
        if (is_layer_defined(n_name))
        {
            return;
        }

        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProperty = serializedObject.FindProperty("layers");

        // depending on the index parameter, we either add in first empty slot, or the given (clamped) index
        if (n_layerindex == -1)
        {
            // we cannot know if/how many layers a user already defined themselves, so we need to find out which layer we can use and set our layer there
            // we do so by going though all possible layers the user can define and look for the first one with no value.
            for (int i = 8; i < 32; i++)
            {
                SerializedProperty sp = layersProperty.GetArrayElementAtIndex(i);

                // the first empty layer is filled with "TeleportLayer"
                if (sp.stringValue == "")
                {
                    sp.stringValue = n_name;
                    break;
                }
            }
        }
        else
        {
            // no clamp function, dont want to write one just for this, so make sure its [8, 32]
            int index = n_layerindex < 0 ? 0 : n_layerindex;
            index = n_layerindex > 32 ? 32 : n_layerindex;

            SerializedProperty sp = layersProperty.GetArrayElementAtIndex(index);
            sp.stringValue = n_name;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

/***********************************************************************************/
/*------------------------------TAG VERIFICATION---------------------------------*/
/***********************************************************************************/

/// <summary>
/// this class deals with all verification, checking adding etc. of anything to do with layers
/// </summary>
public class TagVerification
{

    /// <summary>
    /// checks for the existence of a ayer with a given name
    /// </summary>
    /// <param name="n_name">name of layer we want to check for</param>
    /// <returns>whether a layer with the given name already exists</returns>
    public static bool is_tag_defined(string n_name)
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProperty = serializedObject.FindProperty("tags");

        // run through all layers and check for the given name
        for (int i = 0; i < tagsProperty.arraySize; i++)
        {
            SerializedProperty sp = tagsProperty.GetArrayElementAtIndex(i);

            // the first empty layer is filled with "TeleportLayer"
            if (sp.stringValue == n_name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// adds a layer with the given name to the tag manager IFF there is no layer with that name yet
    /// -> by default it will be added to the first empty slot in the layers manager. 
    /// If given an index, we place the new layer into the given index (and override any existing layer if necessary)
    /// </summary>
    /// <param name="n_name">the name for the new layer we will add</param>
    /// <param name="n_layerindex">OPTIONAL! will place layer to this index, if one is passed in. Must be in Range [8,32] or will be clamped</param>
    public static void add_tag(string n_name)
    {
        if (is_tag_defined(n_name))
        {
            return;
        }

        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProperty = serializedObject.FindProperty("tags");

        tagsProperty.InsertArrayElementAtIndex(0);
        SerializedProperty n = tagsProperty.GetArrayElementAtIndex(0);
        n.stringValue = n_name;

        serializedObject.ApplyModifiedProperties();
    }
}
