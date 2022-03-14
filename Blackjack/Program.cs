using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MyProgram myProgram;
            myProgram = new MyProgram();
            myProgram.Run();
        }
    }
}
