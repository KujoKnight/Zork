using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Zork
{
    public enum Directions
    {
        North,
        South,
        East,
        West
    }

    public class Player
    {
        public World World { get; }
        public int Moves = 0;

        [JsonIgnore]
        public Room Location { get; private set; }

        [JsonIgnore]
        public string LocationName
        {
            get
            {
                return Location?.Name;
            }
            set
            {
                Location = World?.RoomsByName.GetValueOrDefault(value);
            }
        }

        public Player(World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;
        }

        public bool Move(Directions direction)
        {
            bool validMovement = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (validMovement)
            {
                Location = destination;
            }

            return validMovement;
        }
    }
}