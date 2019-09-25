using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zork
{
    class Program
    {

        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArgs.GameFilename] : defaultGameFilename);

            Game.Start(gameFilename);
            Console.WriteLine("Thank you for playing!");
        }

        private enum CommandLineArgs
        {
            GameFilename = 0
        }
    }
}
