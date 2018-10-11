using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MacPan
{
    public static class Stats
    {
        static FileWrite fileWrite = new FileWrite();

        static List<string> statNames = new List<string>();
        static List<object> stats = new List<object>();

        //public static int
        //    stat1,
        //    stat2;
        //public static float
        //    stat 3,
        //    stat 4;
        //public static string
        //    stat 5,
        //    stat 6;

        public static void SaveStats()
        {
            statNames.Add("Hello");
            stats.Add(3);
            fileWrite.Write(Program.Path, statNames, stats);
        }
    }
}
