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

        static List<Stat> stats = new List<Stat>();

        //static List<string> statNames = new List<string>();
        //static List<object> stats = new List<object>();

        public static int
            totalTrophies,
            busted,//
            distanceCovered,
            movedUp,
            movedDown,
            movedLeft,
            movedRight,
            interactedWithNothing,
            gamesStarted,
            statsViewed,
            quit,
            crashed,
            evaded,//
            chased,//
            wallsBumped,
            buttonsPressed,
            averageStepsTrophy;//

        public static long
            timePlayed,
            frames,
            boxesDrawn;

        static void AddStats()
        {
            //stats.Add(new Stat(, ));
            stats.Add(new Stat(totalTrophies, "Trophies collected", ""));
            stats.Add(new Stat(busted, "Player busted"));
            stats.Add(new Stat(distanceCovered, "Distance Covered", "boxes"));
            stats.Add(new Stat(movedUp, "Moved up"));
            stats.Add(new Stat(movedDown, "Moved down"));
            stats.Add(new Stat(movedLeft, "Moved left"));
            stats.Add(new Stat(movedRight, "Moved right"));
            stats.Add(new Stat(interactedWithNothing, "Interacted with nothing"));
            stats.Add(new Stat(gamesStarted, "Game started"));
            stats.Add(new Stat(statsViewed, "Stats were viewed"));
            stats.Add(new Stat(quit, "The game was quit"));
            stats.Add(new Stat(crashed, "The game crashed"));
            stats.Add(new Stat(evaded, "Guards evaded", ""));
            stats.Add(new Stat(chased, "The Player was chased"));
            stats.Add(new Stat(buttonsPressed, "Buttons pressed", ""));
            stats.Add(new Stat(wallsBumped, "Walls ran into", ""));
            stats.Add(new Stat(averageStepsTrophy, "Average amount of steps taken for each trophy"));

            stats.Add(new Stat(timePlayed, "Total time played", "milliseconds"));
            stats.Add(new Stat(frames, "Frames rendered", ""));
            stats.Add(new Stat(boxesDrawn, "Boxes Drawn", ""));
        }

        public static void SaveStats()
        {
            if (stats.Count == 0)
            {
                AddStats();
            }

            if (!Directory.Exists(Program.Path + @"/Stats/"))
            {
                Directory.CreateDirectory(Program.Path + @"/Stats/");
            }
            distanceCovered = movedDown + movedLeft + movedRight + movedUp;

            fileWrite.Write(Program.Path, stats);
        }
    }

    class Stat
    {
        public object Value { get; private set; }
        public string Name { get; private set; }
        public string Unit { get; private set; }

        public Stat(object value, string name, string unit = "times")
        {
            Value = value;
            Name = name;
            Unit = unit;
        }
    }
}
