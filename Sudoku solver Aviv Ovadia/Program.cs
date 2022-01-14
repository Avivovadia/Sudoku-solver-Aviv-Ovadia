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
        {    //10 - : 11 - ; 12 - < 13 - = 14 - > 15 - ? 16 - @
            int i = 1;
            string valid4x4 = "040080@0;010060>30090?04:70=00<006002;0080003000?000000=00>00204" +
                              "0008030070005000000000>0000:0@0=009250000800;000<010000@00=00030" +
                              "000<?0000600=047@1=0:00300700900600000;04009002:03000@000>800;00" +
                              "5;3:000800<400010>00;9000=?04050=040600000020<090<0@0=00010060?0";

            string validgrid = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
            string validgrid2 = "800600905000000000000020310007318060240000073000000000002790100500080036003000000";
            string validgrid3 = "830100605000000080000700900050017000003000200000340010004008000090000000302006047";
            string valid2x2 = "0100420000200300";
            string zeros = "0000000000000000";

           
            Stopwatch sw = new Stopwatch();
            Board board = new Board();
            board.get_matrix(validgrid3); ;
            
            board.display();
            sw.Start();

            //  board.display();
            //while (Solver.update_options(board))
            //{
            //    Console.WriteLine("number " + i);
            //    i++;
            //    board.display();
            //}
            while (Solver.solve_singles(board))
            {
                Console.WriteLine("number " + i);
                i++;
                board.display();
            }
            sw.Stop();
            board.display();
            Console.WriteLine("time elapsed:{0} ", sw.ElapsedMilliseconds + " miliseconds");
            Console.WriteLine("Starting brute force...");
            sw.Start();
            Solver.brute_force(board);
            board.display();
            sw.Stop();
            Console.WriteLine("solved!!!");
            Console.WriteLine("time elapsed:{0} ", sw.ElapsedMilliseconds + " miliseconds");

            Console.ReadKey();
        }
    }
}
