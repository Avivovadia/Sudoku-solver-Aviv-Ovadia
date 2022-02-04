using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace Sudoku_solver_Aviv_Ovadia
{
    class Program
    {
        static void Main(string[] args)
        {
            char end = '1';
            while (end != '0')
            {
                Console.WriteLine("Sudoku Solver Aviv Ovadia\n");
                UI.HandleSudoku();
                Console.Write("Do you want to stop? Enter 0 to stop:");
                end = Console.ReadKey().KeyChar;
                Console.Clear();
            }
            string data = string.Concat(Enumerable.Repeat("0", 256));
            Console.ReadKey();
        }
        
    }
}
