using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
    class Board  //Board class repressents a full sudoku board. It has a matrix of Cells,length and scale.
    {   public Cell[,] matrix { get; set; }
        public int length { get; set; }
        public int scale { get; set; }

        //the function checks if the length of the input fits for a sudoku board -> the length is a power of 4 to some number.
        //if it fits, update the length and the scale appropriately, if not, throws exception (not valid size exception)
        public void check_input_size(string str)
        {

        }
        //the function checks each key of the input, if there is an invalid key throw exception (not valid key exception).
        public void check_input_keys(string str)
        {

        }
        //the function initialize the matrix, and initialize each cell inside it corresponds to the input board.
        public void matrix_init(string str)
        {

        }
        //the function gets an input, if the input is valid, create a fitting matrix and stores inside it the input board. 
        public void get_matrix(string str)
        {
            check_input_size(str);
            check_input_keys(str);
            matrix_init(str);
        }
    }
}
