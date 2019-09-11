using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Zork
{
    internal class Program
    {
        private static string currentRoom
        {
            get
            {
                return Rooms[Location.x, Location.y];
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while(command != Commands.QUIT)
            {
                Console.CursorLeft = Console.BufferWidth - 32;
                Console.WriteLine("You are " + Rooms[Location.x, Location.y]);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.");
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.CursorLeft = Console.BufferWidth - 32;
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
                    Location.x++;
                    break;
                case Commands.WEST when Location.y > 0:
                    Location.x--;
                    break;

                default:
                    validMovement = false;
                    break;
            }

            return validMovement;
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN);

        private static bool canMove(Commands com) => Directions.Contains(com);

        private static readonly string[,] Rooms = {
            {"at the Rocky Trail", "South of the House", "at the Canyon View"},
            {"at the Forest","West of the House","behind the House"},
            {"in the Dense Woods","North of the House","at the Clearing"}
        };

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static (int x, int y) Location = (1, 1);
    }
}
