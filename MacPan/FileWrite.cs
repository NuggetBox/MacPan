using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    class FileWrite
    {
        public void Write(string path, List<Stat> stats)
        {
            string[] data = new string[stats.Count];

            for (int i = 0; i < stats.Count; ++i)
            {
                data[i] += stats[i].Name + ": " + stats[i].Value + " " + stats[i].Unit;
            }

            File.WriteAllLines(path + "Stats.txt", data);
        }
    }
}
