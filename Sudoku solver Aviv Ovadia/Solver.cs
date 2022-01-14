using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
    class Solver //Solver class contains functions which solve the board
    {
        //the function gets the board and a cell and removes it's value from all the cells that shares an element with it.
        //returns whether there was a change in the options or not.
        public static bool update_options_cell(Board board,int row,int col)
        {
            bool flag = false;
            Cell cell = board.matrix[row, col];
            int i,value=cell.value();
            for (i = 0; i < board.length; i++)
            {
                if (board.matrix[i, col] != cell)
                {
                    if (board.matrix[i, col].remove(value))
                        flag = true;
                }
                if (board.matrix[row, i] != cell)
                {
                    if (board.matrix[row, i].remove(value))
                        flag = true;
                }
                if (board.boxmatrix[cell.box, i] != cell)
                {
                    if (board.boxmatrix[cell.box, i].remove(value))
                        flag = true;
                }

            }
            return flag;
        }
        //the function update the options of all the cells, returns whether there was a change in the options or not.
        public static bool update_options(Board board)
        {
            bool flag = false;
            for(int i = 0; i < board.length; i++)
            {
                for(int j = 0; j < board.length; j++)
                {
                    if (board.matrix[i, j].hasValue())
                    {
                        if (update_options_cell(board, i, j))
                            flag = true;
                    }
                }
            }
            return flag;
        }


        //the function returns array of counters of options values in elements.
        public static int[] generate_options_count(Cell[] element,Board board)
        {
            int[] optionscount = new int[board.length];
            foreach( Cell cell in element)
            {
                if (!cell.hasValue())
                {
                    foreach(int value in cell.options)
                    {
                        optionscount[value - 1]++;
                    }
                }
            }
            return optionscount;
        }


        //the function solves cells in element by checking if there is an option which appear only once in the element.(hidden single)
        public static bool solve_single_inArray(Cell[] element,Board board)
        {
            bool flag = false;
            int value;
            Cell cell;
            int[] optionscount = generate_options_count(element, board);
            
            while((value = Array.IndexOf(optionscount, 1) + 1) != 0)
            {
                flag = true;
                foreach (Cell c in element)
                {
                    if (c.options.Contains(value))
                    {
                        c.setValue(value);
                        update_options_cell(board, c.row, c.col);
                        break;
                    }
                }
                optionscount = generate_options_count(element, board);


            }
            return flag;
        }


        //the functioin solves the board by the tactics hidden single and naked single.
        public static bool solve_singles(Board board)
        {
            while (update_options(board))
            {

            }
            int i;
            bool flag = false;
            for (i = 0; i < board.length; i++)
            {
               if( solve_single_inArray(board.GetRow(board.matrix, i), board)||
                    solve_single_inArray(board.GetColumn(board.matrix,i),board)||
                    solve_single_inArray(board.GetRow(board.boxmatrix, i), board))
                {
                    flag = true;
                }
            }
            return flag;
        }

        //returns the next empty cell in the board
        public static Cell next_pos(Board board)
        {
            for(int i = 0; i < board.length; i++)
            {
                for(int j = 0; j < board.length; j++)
                {
                    if (!board.matrix[i, j].hasValue())
                        return board.matrix[i, j];
                }
            }
            return null;
        }
        //the function gets the board and try to solve it using brute force -> check every possible combination.
        public static bool brute_force(Board board)
        {
            Cell nextempty = next_pos(board);
            if (nextempty == null)
                return true;
            int[] options = nextempty.options;
           // nextempty.show();
            foreach(int option in options)
            {
                nextempty.setValue(option);
               // Console.WriteLine("put value "+option);
                if (board.check_valid())
                {
                    if (brute_force(board))
                    {
                        return true;
                    }
                }

                nextempty.options = options;
            }
            return false;
        }

        //the main function which gets a board and solves it.
        public void solve(Board board)
        {

        }
    }
}
