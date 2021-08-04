/*  (c) 2019 matthew fairchild
 */

using UnityEngine;
using System.Collections.Generic;

public class InputsForTesting : MonoBehaviour
{
	#region VARIABLES
	
	
	#endregion

	#region UNITY LIFECYCLE

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("write csv file");
            List<List<string>> values = new List<List<string>>();
            List<string> one = new List<string>() { "f", "a", "7" };
            List<string> two = new List<string>() { "s"};
            List<string> three = new List<string>() { "n", "1", "yx" };
            List<string> four = new List<string>() {  };
            values.Add(one);
            values.Add(two);
            values.Add(three);
            values.Add(four);

            FileHandler.write_csv_file("my_csv_file.txt", values);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("read csv file");
            List<List<string>> values = FileHandler.read_csv_file<string>("my_csv_file.txt");
            foreach (var line in values)
            {
                foreach (var word in line)
                {
                    Debug.Log(word);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("resetting stats");
        }
    }
	
	#endregion
	
	#region InputsForTesting FUNCTIONS
	#endregion
}
