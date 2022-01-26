using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
    class Solver :TacticSolver //Solver class -> inherits the tactics from Tactic Solver and has function for the main solving algorithm of the sudoku.
    {
        //creates copy of the Cell matrix.
        public static Cell[,] copy(Cell[,] matrix)
        {
            int length = Convert.ToInt32(Math.Sqrt(matrix.Length));
            Cell[,] copy = new Cell[length,length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    copy[i, j] = new Cell(Convert.ToInt32(Math.Sqrt(length)), i, j, matrix[i, j].value());
                    copy[i, j].options = (int[])matrix[i, j].options.Clone();
                }
            }
            return copy;
        } 
        
        //uses all the tactics from tactic solver in order to solve the sudoku (hidden singles, naked singles and intersections).
        //loops all the tactics until thery have no effect.
        public static void tactics(Board board)
        {
            bool flag = true;
            while (flag)
            {
                solve_singles(board);
                flag = solve_intersection(board);
            }
        }


        //returns the cell which has the least number of option which is not solved.
        public static Cell leastoptions(Board board)
        {
            Cell cell = next_pos(board); //init cell with empty cell
            for(int i = 0; i < board.length; i++)
            {
                for(int j = 0; j < board.length; j++)
                {
                    if (cell.options.Length > board.matrix[i, j].options.Length && board.matrix[i, j].options.Length != 1)
                        cell = board.matrix[i, j];
                }
            }
            return cell;
        }
  
        /*-----------------------------------------------------------------------------------------------------------
         * the main algorithm which gets a board and solves it:
         *      
         *      the function is recursive and gets a board and return bool:
         *          1.do all the tactics possible
         *          2.if the board is not valid return false
         *          3.if the board is full return true
         *          4. get the cell with the least option possible and make a copy of the matrix board
         *          5. for every option:
         *              5.1. put the option in the cell and call the function
         *              5.2. if it returned true, return true
         *              5.3. else put the copy matrix in the matrix
         *          6. return false
         *          
         *     the function solves using tactics and when it can't progress it makes a guess and continues.
         -----------------------------------------------------------------------------------------------------------*/
        public static bool solve(Board board)
        {     
            tactics(board); //solve tactics
       
            if (board.check_valid() == false)
                return false;
            if (next_pos(board) == null)
                return true;           
            Cell cell = leastoptions(board);
            Cell[,] clone = copy(board.matrix); //clone = matrix
            if (cell.options.Length == 0)
                return false;
            foreach(int option in cell.options)
            {
                cell = leastoptions(board);
                cell.setValue(option);
                if (solve(board))
                    return true;
                board.matrix = copy(clone); //put the original value of matrix (clone) inside matrix. bacause the solving failed.
                board.boxmatrix_init();     //update boxmatrix too
            }          
            return false;
        }
    }
}
