using System;

namespace Zork
{
    class Program
    {
        private static string outputString;
        private static readonly string[,] Rooms = {
            {"at the Rocky Trail", "South of the House", "at the Canyon View"},
            {"at the Forest","West of the House","behind the House"},
            {"in the Dense Woods","North of the House","at the Clearing"}
        };
        private static (int x, int y) Location;
        private static bool canMove;

        static void Main(string[] args)
        {
            Location.x = 1;
            Location.y = 1;
            canMove = true;
            Console.Write("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while(command != Commands.QUIT)
            {
                if(Location.x >= 0 && Location.x <= Rooms.Length / 3 && Location.y >= 0 && Location.y <= Rooms.Length / 3 && canMove == true)
                {
                    Console.CursorLeft = Console.BufferWidth - 32;
                    Console.WriteLine("You are " + Rooms[Location.x,Location.y]);
                }
                else if(canMove == false)
                {
                    Console.CursorLeft = Console.BufferWidth - 32;
                    Console.WriteLine("You are " + Rooms[Location.x, Location.y]);
                    Console.CursorLeft = Console.BufferWidth - 32;
                    Console.WriteLine("The way is blocked");
                }
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == true)
                        {
                            outputString = $"You moved {command}.";
                        }

                        switch (command)
                        {
                            case Commands.NORTH:
                                //Go North
                                if (Location.x < Rooms.Length / 3 - 1)
                                {
                                    canMove = true;
                                    Location.x += 1;
                                }
                                else
                                {
                                    canMove = false;
                                }
                                break;
                            case Commands.SOUTH:
                                //Go South
                                if (Location.x > 0)
                                {
                                    canMove = true;
                                    Location.x -= 1;
                                }
                                else
                                {
                                    canMove = false;
                                }
                                break;
                            case Commands.EAST:
                                //Go East
                                if (Location.y < Rooms.Length / 3 - 1)
                                {
                                    canMove = true;
                                    Location.y += 1;
                                }
                                else
                                {
                                    canMove = false;
                                }
                                break;
                            case Commands.WEST:
                                //Go West
                                if (Location.y > 0)
                                {
                                    canMove = true;
                                    Location.y -= 1;
                                }
                                else
                                {
                                    canMove = false;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        outputString = "Unknown command";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN);

        private static bool Move(Commands com)
        {
            try
            {
                if (com == Commands.NORTH || com == Commands.SOUTH || com == Commands.EAST || com == Commands.WEST)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
