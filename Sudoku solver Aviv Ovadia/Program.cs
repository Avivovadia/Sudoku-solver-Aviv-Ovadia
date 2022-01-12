using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            string str = "123456789012345678901123050801260147567890123456789012345678902345678901234567890";
            string str2 = "1234123412341234";
            string str3 = "1";
            Board board = new Board();
            board.get_matrix(str);
            board.display();
            Cell[] element = board.GetRow(board.matrix, 1);
            
            Console.ReadKey();
        }
    }
}
