using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Fileio : MonoBehaviour
{
    public static void write(string fileName, List<KeyValuePair<string, float>> list, bool append = true)
    {
        StreamWriter writer = new StreamWriter(fileName, append);

        foreach(KeyValuePair<string, float> elem in list)
        {
            writer.WriteLine($"{elem.Key}:{elem.Value}");
        }
        writer.Close();
    }

    public static List<KeyValuePair<string, float>> read(string fileName)
    {
        List<KeyValuePair<string, float>> list = new List<KeyValuePair<string, float>>();

        if (File.Exists(fileName))
        {
            StreamReader reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] split = line.Split(':');
                if (float.TryParse(split[1], out float score))
                {
                    list.Add(new KeyValuePair<string, float>(split[0], score));
                }
            }

            reader.Close();
        }

        return list;
    }
}
