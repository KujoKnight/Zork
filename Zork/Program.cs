using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            Console.WriteLine("Welcome to Zork!");
            InitRoomDesc();

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

        private static void InitRoomDesc()
        {
            var roomMap = new Dictionary<string, Room>();
            foreach(Room room in Rooms)
            {
                roomMap[room.Name] = room;
            }

            roomMap["Rocky Trail"].Description = "You are on a rock-strewn trail.";                                                                                //A Rocky Trail
            roomMap["South of House"].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";         //South of the House
            roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";                                                      //A Large Canyon

            roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";                                                     //A Forest
            roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";                                        //West of the House
            roomMap["Behind House"].Description = "You are behind the white house. In one corner of the house, there is a small window, which is slightly ajar.";   //Behind the House

            roomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";            //Some Dense Woods
            roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";         //North of the House
            roomMap["Clearing"].Description = "You are in a clearing, with a forest surrounding you on the west and south.";                                    //A Clearing
        }
    }
}
