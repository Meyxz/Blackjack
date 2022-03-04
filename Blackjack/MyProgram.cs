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
            table = new Table();
            table.PlayTable();
        }
    }
}
