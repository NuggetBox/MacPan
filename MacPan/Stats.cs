using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace MacPan
{
    public static class Stats
    {
        public static Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
        static Data exStats;

        public const string statsPath = "/Stats/Stats.sav";

        public static void AddStats()
        {
            //stats.Add(new Stat(, ));
            stats["Trophies"] = new Stat(0, "Trophies collected", "");
            stats["Busted"] = new Stat(0, "Player has been busted");
            stats["Distance"] = new Stat(0, "Distance covered", "tiles");
            stats["Up"] = new Stat(0, "Moved up");
            stats["Down"] = new Stat(0, "Moved down");
            stats["Left"] = new Stat(0, "Moved left");
            stats["Right"] = new Stat(0, "Moved right");
            stats["InteractNothing"] = new Stat(0, "Interacted with nothing");
            stats["Games"] = new Stat(0, "Games started", "");
            stats["Stats"] = new Stat(0, "Stats were viewed");
            stats["Quit"] = new Stat(0, "The game was quit");
            stats["Crashed"] = new Stat(0, "The game has crashed");
            stats["Evaded"] = new Stat(0, "Guards evaded", "");
            stats["Chased"] = new Stat(0, "The Player was chased");
            stats["Buttons"] = new Stat(0, "Buttons pressed", "");
            stats["Walls"] = new Stat(0, "Walls bumped", "");
            stats["AverageStepsTrophy"] = new Stat(0, "Average amount of steps taken for a trophy", "");
            stats["Secured"] = new Stat(0, "Trophies were secured");

            stats["Time"] = new Stat(0, "Total time played", "milliseconds");
            stats["Frames"] = new Stat(0, "Frames rendered", "");
            stats["Boxes"] = new Stat(0, "Boxes drawn", "");

            stats["Distance"] = new Stat((int)stats["Up"].Value + (int)stats["Down"].Value + (int)stats["Right"].Value + (int)stats["Left"].Value, stats["Distance"].Name, stats["Distance"].Unit);
        }

        public static void SaveStats()
        {
            if (stats.Count != FileWrite.Read(Program.Path + statsPath).stats.Count)
            {
                AddStats();
            }

            stats["Time"].Add((int)Program.gameTime.ElapsedMilliseconds);
            Program.gameTime.Restart();

            if (File.Exists(Program.Path + statsPath))
            {
                exStats = FileWrite.Read(Program.Path + statsPath);
                Data test = exStats;
            }
            else
            {
                exStats.stats = stats;
                foreach (KeyValuePair<string, Stat> stat in exStats.stats)
                {
                    exStats.stats[stat.Key].SetValue(0);
                }
            }

            foreach (KeyValuePair<string, Stat> stat in stats)
            {
                stats[stat.Key].Add((int)exStats.stats[stat.Key].Value);
            }

            Data data = new Data(stats);
            FileWrite.Write(Program.Path + statsPath, data);

            AddStats();
        }

        public static void ResetStats()
        {
            AddStats();
            File.Delete(Program.Path + statsPath);
        }
    }

    [Serializable]
    public struct Data
    {
        public Dictionary<string, Stat> stats;

        public Data (Dictionary<string, Stat> stats)
        {
            this.stats = stats;
        }
    }

    [Serializable]
    public class Stat
    {
        public object Value { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public Stat(object value, string name, string unit = "times")
        {
            Value = value;
            Name = name;
            Unit = unit;
        }

        public void Add(int value)
        {
            Value = (int)Value + value;
        }

        public void SetValue(int value)
        {
            Value = value;
        }
    }
}
