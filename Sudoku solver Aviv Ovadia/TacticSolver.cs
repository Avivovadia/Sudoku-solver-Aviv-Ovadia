using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
    class TacticSolver  //Solver class, contains function which solve Board by using human tactics.
                        //contains implementations of human tactics.
    {

        //the function gets a board and a coordinates of cell, removes its value from all it's neighboring cells (cells which share an element). 
        //if the cell is empty it has no effect.
        //returns true if there was change in the options of some cell.
        public static bool update_options_cell(Board board, int row, int col)
        {
            bool flag = false;
            Cell cell = board.matrix[row, col];
            int i, value = cell.value();
            for (i = 0; i < board.length; i++)
            {
                if (board.matrix[i, col] != cell)   
                {
                    if (board.matrix[i, col].remove(value)) //remove from collumn
                        flag = true;
                }
                if (board.matrix[row, i] != cell)
                {
                    if (board.matrix[row, i].remove(value)) //remove from row
                        flag = true;
                }
                if (board.boxmatrix[cell.box, i] != cell)
                {
                    if (board.boxmatrix[cell.box, i].remove(value)) //remove from box
                        flag = true;
                }
            }
            return flag;
        }

        //the function update the options of all the cells, returns whether there was a change in the options or not.
        public static bool update_options(Board board)
        {
            bool flag = false;
            for (int i = 0; i < board.length; i++)
            {
                for (int j = 0; j < board.length; j++)
                {
                    if (board.matrix[i, j].hasValue())
                    {
                        if (update_options_cell(board, i, j))
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }


        //the function returns array of counters to the options values in elements.
        public static int[] generate_options_count(Cell[] element, Board board)
        {
            int[] optionscount = new int[board.length];
            foreach (Cell cell in element)
            {
                if (!cell.hasValue())
                {
                    foreach (int value in cell.options)
                    {
                        optionscount[value - 1]++;
                    }
                }
            }
            return optionscount;
        }


        //the function solves cells in element by hidden single tactic: if an option appears only once in an element, the value of the cell which contains this option must be the option
        //returns if there is a change in the options.
        public static bool solve_single_inArray(Cell[] element, Board board)
        {
            bool flag = false;
            int value;
            int count;
            int[] optionscount = generate_options_count(element, board);

            while ((value = Array.IndexOf(optionscount, 1) + 1) != 0)   //value = the option which appears only once in the element. 
            {
                count = 0;
                foreach (Cell c in element)
                {
                    if (c.options.Contains(value))
                        count++;
                }
                if (count == 1)            //if there is no solved cell in the element which has the value of the option. 
                {
                    flag = true;
                    foreach (Cell c in element)
                    {
                        if (c.options.Contains(value))  
                        {   
                                         //put the value in the cell and update the option of it's neighboring cells.
                            c.setValue(value);
                            update_options_cell(board, c.row, c.col);
                            break;
                        }
                    }
                    optionscount = generate_options_count(element, board);
                }
                else
                    break;

            }
            return flag;
        }

        //the function solves all hidden singles in board. return true if there was a change in the options.
        public static bool solve_hidden_singles(Board board)
        {
            int i;
            bool flag = false;
            for (i = 0; i < board.length; i++) 
            {
                if (solve_single_inArray(board.GetRow(board.matrix, i), board)) //check row
                    flag = true;
                if (solve_single_inArray(board.GetColumn(board.matrix, i), board)) //check col
                    flag = true;
                if (solve_single_inArray(board.GetRow(board.boxmatrix, i), board)) //check box
                    flag = true;
            }
            return flag;
        }

        //the function solves the board by the tactics hidden single and naked single.
        //loops through both tactics until they don't affect the board anymore.
        public static void solve_singles(Board board)
        {
            while (update_options(board) || solve_hidden_singles(board)) { }
        }


        //the function gets an option and a box, checks if all the cells that contains this option are in the same row/collumn,
        //if they are, it returns this row/collum otherwise returns null.
        public static Cell[] options_in_same_element(int option, Cell[] box, Board board)
        {
            int[] rowcounter = new int[board.scale];     //count if row has row has the option inside
            int[] colcounter = new int[board.scale];     //count if row has collumn has the option inside
            int startrow = box[0].row, row, col;
            int startcol = box[0].col;
            foreach (Cell cell in box)       //loops the box and sum the rowcounter and colcounter
            {
                if (!cell.hasValue() && cell.options.Contains(option))
                {
                    rowcounter[cell.row % board.scale] = 1;
                    colcounter[cell.col % board.scale] = 1;
                }
            }
            if (rowcounter.Sum() == 1)     //if the options appear only in one row
            {
                row = Array.IndexOf(rowcounter, 1);
                return board.GetRow(board.matrix, row + startrow);

            }
            if (colcounter.Sum() == 1)      //if the options apper only in one collumn
            {
                col = Array.IndexOf(colcounter, 1);
                return board.GetColumn(board.matrix, col + startcol);
            }
            return null;
        }

        //the function solves the board with intersection tactic:
        // if the placing of an option in a box is in only one row/collumn,
        // remove the option from all the other cells in the row/collumn which are not in the box.
        //returns if there was a change in the options or not.
        public static bool solve_intersection(Board board)
        {
            Cell[] box;
            Cell[] element;
            int[] optionscount;
            int option;
            bool flag = false;
            for (int i = 0; i < board.length; i++) //loops boxes
            {
                box = board.GetRow(board.boxmatrix, i);
                optionscount = generate_options_count(box, board);
                for (int j = 0; j < board.length; j++) //loops options
                {
                    if (optionscount[j] > 1)  //if the option appears more than once in the box
                    {
                        option = j + 1;
                        if ((element = options_in_same_element(option, box, board)) != null) //if all the cells which has the option are in the same row/col
                        {
                            foreach (Cell cell in element)
                            {
                                if (cell.box != i) //we don't want to remove the option from the cells in the original box.
                                {
                                    if (cell.remove(option))
                                    {
                                        flag = true;
                                    }
                                }
                            }
                        }
                    }
                    if (flag)
                        break;
                }
                if (flag)
                    break;
            }
            return flag;
        }

        //returns the next empty cell in the board. if there are no empty cells return null -> can be used to check if the board is full.
        public static Cell next_pos(Board board)
        {
            for (int i = 0; i < board.length; i++)
            {
                for (int j = 0; j < board.length; j++)
                {
                    if (!board.matrix[i, j].hasValue())
                        return board.matrix[i, j];
                }
            }
            return null;
        }
    }
}
