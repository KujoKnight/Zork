using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zork
{
    internal class Program
    {
        private static Room currentRoom
        {
            get
            {
                return Rooms[Location.x, Location.y];
            }
        }

        static void Main(string[] args)
        {
            const string defaultRoomsFilename = "Rooms.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArgs.RoomsFilename] : defaultRoomsFilename);

            Console.WriteLine("Welcome to Zork!");
            InitRoom(roomsFilename);

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while(command != Commands.QUIT)
            {
                Console.WriteLine(currentRoom);
                if(previousRoom != currentRoom)
                {
                    Console.WriteLine(currentRoom.Description);
                    previousRoom = currentRoom;
                }

                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(currentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is blocked.");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }

        private static bool Move(Commands com)
        {
            Assert.IsTrue(canMove(com), "Invalid Direction");

            bool validMovement = true;
            switch (com)
            {
                case Commands.NORTH when Location.x > 0:
                    Location.x--;
                    break;
                case Commands.SOUTH when Location.x < Rooms.GetLength(0) - 1:
                    Location.x++;
                    break;
                case Commands.EAST when Location.y < Rooms.GetLength(1) - 1:
                    Location.y++;
                    break;
                case Commands.WEST when Location.y > 0:
                    Location.y--;
                    break;

                default:
                    validMovement = false;
                    break;
            }

            return validMovement;
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN);

        private static bool canMove(Commands com) => Directions.Contains(com);

        private static Room[,] Rooms;

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static (int x, int y) Location = (1, 1);

        private static void InitRoom(string roomsFilename) => 
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));

        private enum Fields
        {
            Name = 0,
            Description
        }

        private enum CommandLineArgs
        {
            RoomsFilename = 0
        }
    }
}
