using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
   public class InvalidInputException:Exception
    {
        public InvalidInputException() : base(modifymessage())
        {

        }
        public InvalidInputException(int length) : base(modifymessage(length))
        {

        }
        public InvalidInputException(char chr,int length) : base(modifymessage(chr,length))
        {

        }
        public static string modifymessage()
        {
            return "Invalid Input Exception: found the same number twice in an element (row, column, box)";
        }
        public static string modifymessage(int length)
        {
            return "Invalid Input Exception: " + length.ToString() + " is not a valid length.";
        }
        public static string modifymessage(char chr,int length)
        {
            return "Invalid Input Exception: " + chr + " is not a valid key for length "+length.ToString();
        }

    }
}
