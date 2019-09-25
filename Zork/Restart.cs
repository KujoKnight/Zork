﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    [CommandClass]
    public static class RestartCommand
    {
        [Command("RESTART", new string[] { "RESTART", "R" })]
        public static void Restart(Game game, CommandContext commandContext)
        {
            if(game.ConfirmAction("Are you sure you want to restart?"))
            {
                game.Restart();
            }
        }
    }
}
