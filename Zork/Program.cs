using System;

namespace Zork
{
    class Program
    {
        private static string outputString;
        private static string[] Rooms = new string[] {"in the Forest", "West of the House", "behind the House", "at the clearing", "at the canyon"};
        private static int x;
        private static bool canMove;

        static void Main(string[] args)
        {
            canMove = true;
            x = 1;
            Console.Write("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while(command != Commands.QUIT)
            {
                if(x >= 0 && x <= Rooms.Length && canMove == true)
                {
                    Console.CursorLeft = Console.BufferWidth - 32;
                    Console.WriteLine("You are " + Rooms[x]);
                }
                else if(canMove == false)
                {
                    Console.CursorLeft = Console.BufferWidth - 32;
                    Console.WriteLine("You are " + Rooms[x]);
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
                                break;
                            case Commands.SOUTH:
                                //Go South
                                break;
                            case Commands.EAST:
                                //Go East
                                if (x < Rooms.Length - 1)
                                {
                                    canMove = true;
                                    x += 1;
                                }
                                else
                                {
                                    canMove = false;
                                }
                                break;
                            case Commands.WEST:
                                //Go West
                                if (x > 0)
                                {
                                    canMove = true;
                                    x -= 1;
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
