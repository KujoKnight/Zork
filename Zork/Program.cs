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
            const string defaultRoomsFilename = "Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArgs.RoomsFilename] : defaultRoomsFilename);

            Console.WriteLine("Welcome to Zork!");
            InitRoomDesc(roomsFilename);

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
                case Commands.SOUTH when Location.x > Rooms.GetLength(0) - 1:
                    Location.x++;
                    break;
                case Commands.EAST when Location.y > Rooms.GetLength(1) - 1:
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

        private static readonly Room[,] Rooms = {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static (int x, int y) Location = (1, 1);

        private static void InitRoomDesc(string roomsFilename)
        {
            const string fieldDelimiter = "##";
            const int expectedFieldCount = 2;

            var roomQuery = from line in File.ReadLines(roomsFilename)
                            let fields = line.Split(fieldDelimiter)
                            where fields.Length == expectedFieldCount
                            select (Name: fields[(int)Fields.Name],
                                    Description: fields[(int)Fields.Description]);

            foreach(var (Name, Description) in roomQuery)
            {
                RoomMap[Name].Description = Description;
            }
        }

        private static readonly Dictionary<string, Room> RoomMap;

        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                RoomMap[room.Name] = room;
            }
        }

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
