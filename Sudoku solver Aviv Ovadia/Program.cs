using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
    class Program
    {
        static void Main(string[] args)
        {
            Cell cell = new Cell(3,2,5);
            cell.show();
            Console.WriteLine(cell.remove(5));
            cell.show();
            Console.WriteLine(cell.remove(5));
            cell.show();
            Console.WriteLine(cell.value());
        }
    }
}
