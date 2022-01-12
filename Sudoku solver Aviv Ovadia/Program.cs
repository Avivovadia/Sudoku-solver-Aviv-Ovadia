using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Sudoku_solver_Aviv_Ovadia
{
    class Program

    {   //sets the color of the text to white or green
        public void setcolor(bool flag)
        {
            if (flag)
            { Console.ForegroundColor = ConsoleColor.Green; }
            else
                Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {
            int i = 1;
            string str = "123456789012345678901123050801260147567890123456789012345678902345678901234567890";
            string validgrid = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
            string str2 = "1234123412341234";
            string str3 = "1";
            Stopwatch sw = new Stopwatch();
            Board board = new Board();
            board.get_matrix(validgrid);
            board.display();
            sw.Start();
            while (Solver.update_options(board))
            {
                Console.WriteLine("number "+i);
                i++;
                board.display();
            }
            board.display();
            sw.Stop();
            Console.WriteLine("SOLVED!!!");
            Console.WriteLine("Time elapsed:{0} ",sw.ElapsedMilliseconds +" miliseconds");
            
            Console.ReadKey();
        }
    }
}
