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
            //cell cell = new cell(3, 2, 5);
            //cell.show();
            //console.writeline(cell.remove(5));
            //cell.show();
            //console.writeline(cell.remove(5));
            //cell.show();
            //console.writeline(cell.value());
            string str = "123456789012345678901123450780126047567890123456789012345678902345678901234567890";
            Board board = new Board();
            //board.check_input_size(str);
            //board.check_input_keys(str);
            //Console.WriteLine("length is: "+board.length+ " scale is:"+board.scale);
            board.get_matrix(str);
            board.display();

            Console.ReadKey();
        }
    }
}
