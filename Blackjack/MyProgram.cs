using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class MyProgram
    {
        Table table;

        public void Run()
        {
            bool gameRunning = true;
            table = new Table();
            Console.CursorVisible = false;

            while (gameRunning)
            {
                table.PlayTable();
            }
        }
    }
}
