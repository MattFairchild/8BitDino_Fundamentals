/* (c) 2020 vr-on GmbH
 *  
 * Script to easily read and write to files, with quick functions to retrieve and write to often used files in Stage
*/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileHandler
{
    // path to the data file
    private static string DATA_PATH = Application.dataPath + @"/STAGE/_PresentationInfo/data.dat";

    /// <summary>
    /// write a given nested list (2 lists exactly) into a comma separated value file
    /// </summary>
    /// <typeparam name="T">whatever type we want it to be. must apply to all values</typeparam>
    /// <param name="n_filepath">path to the file we write into</param>
    /// <param name="n_data">the data to be written into the file</param>
    public static void write_csv_file<T>(string n_filepath, List<List<T>> n_data)
    {
        using (StreamWriter writer = new StreamWriter(n_filepath, false))
        {
            foreach (var list in n_data)
            {
                // if we reach an empty row/list we don't bother and continue on with our task
                if (list.Count == 0)
                {
                    continue;
                }

                string line = list[0].ToString();
                for (int i = 1; i < list.Count; i++)
                {
                    line = string.Format("{0},{1}", line, list[i].ToString());
                }
                writer.WriteLine(line);
            }
        }

        Debug.Log("wrote shit");
    }


    /// <summary>
    /// write a given string to a given path. Will create if doesn't exist.
    /// </summary>
    /// <param name="n_file_path">path to the file we want to write to</param>
    /// <param name="n_info">a single string that will be written into the file</param>
    /// <param name="n_append">whether to append to the file or not. If not, will override file with this string</param>
    public static void write_file(string n_file_path, string n_info, bool n_append = false)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        using (StreamWriter writer = new StreamWriter(n_file_path, n_append))
        {
            writer.WriteLine(n_info);
        }
    }

    /// <summary>
    /// write a given dictionary into a file with the appropriate formatting we use to read
    /// </summary>
    /// <param name="n_file_path">path to the file we want to write to</param>
    /// <param name="n_dict">the dictionary that will be written into the file</param>
    public static void write_file<T>(string n_file_path, Dictionary<string, T> n_dict)
    {
        Dictionary<string, T> dictionary = new Dictionary<string, T>();

        using (StreamWriter writer = new StreamWriter(n_file_path, false))
        {
            foreach (var pair in n_dict)
            {
                writer.WriteLine(pair.Key + ":" + pair.Value.ToString());
            }
        }
    }

    /// <summary>
    /// write / update a single dictionary entry into the data file
    /// </summary>
    /// <param name="n_key">key to create / update</param>
    /// <param name="n_value">new value for the given key</param>
    public static void write_data_value<T>(string n_key, T n_value)
    {
        Dictionary<string, T> dict = read_file<T>(DATA_PATH);
        dict[n_key] = n_value;
        write_file(DATA_PATH, dict);
    }

    /// <summary>
    /// get the value of a single specific key in the data file
    /// </summary>
    /// <param name="n_key">key of interest in the data file</param>
    /// <returns>the corresponding value to the key of interest in the data file</returns>
    public static T read_data_value<T>(string n_key)
    {
        Dictionary<string, T> dict = read_file<T>(DATA_PATH);
        foreach (var pair in dict)
        {
            if (pair.Key == n_key)
            {
                return pair.Value;
            }
        }

        return default;
    }

    /// <summary>
    /// simply get a dictionary with all of the key value string pairs back that are saved in the file
    /// </summary>
    /// <param name="n_filepath"></param>
    /// <returns>dictionary with all key value string pairs of the given file, or null if file does not exist</returns>
    public static Dictionary<string, T> read_file<T>(string n_filepath)
    {
        Dictionary<string, T> result = new Dictionary<string, T>();

        // if the file does not exist, then we just reset the variable we have instantiated and use that
        if (!File.Exists(n_filepath))
        {
            return result;
        }

        // read through all of the lines and 
        using (StreamReader reader = new StreamReader(n_filepath))
        {
            while (!reader.EndOfStream)
            {
                string[] sd = reader.ReadLine().Split(':');
                result[sd[0]] = (T)Convert.ChangeType(sd[1], typeof(T));
            }
        }

        return result;
    }

    /// <summary>
    /// reads a comma separated value file and returns a nested list of given values
    /// </summary>
    /// <typeparam name="T">the type that the values will have. must be same for the entire file</typeparam>
    /// <param name="n_filepath">path to the file we want to write</param>
    /// <returns></returns>
    public static List<List<T>> read_csv_file<T>(string n_filepath)
    {
        List<List<T>> result = new List<List<T>>();

        // if the file does not exist, then we just reset the variable we have instantiated and use that
        if (!File.Exists(n_filepath))
        {
            return result;
        }

        // read through all of the lines and 
        using (StreamReader reader = new StreamReader(n_filepath))
        {
            while (!reader.EndOfStream)
            {
                string[] sd = reader.ReadLine().Split(',');
                List<T> line = new List<T>();
                foreach (string val in sd)
                {
                    line.Add((T)Convert.ChangeType(val, typeof(T)));
                }
                result.Add(line);
            }
        }

        return result;
    }
}
