﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    class FileWrite
    {
        public void Write(string path, List<string> statNames, List<object> stats)
        {
            string[] data = new string[statNames.Count];

            for (int i = 0; i < statNames.Count; ++i)
            {
                data[i] += statNames[i] + ": " + stats[i].ToString();
            }

            File.WriteAllLines(path, data);

            // File.WriteAllText(@path + "/File Saving Folder/MyOwnData.txt", "My own text in a text file");
            // Console.WriteLine(File.ReadAllText(path + "/File Saving Folder/MyOwnData.txt"));
        }
    }
}
