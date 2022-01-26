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
        public static bool update_options_cell(Board board, int row, int col)
        {
            bool flag = false;
            Cell cell = board.matrix[row, col];
            int i, value = cell.value();
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


        //the function returns array of counters of options values in elements.
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


        //the function solves cells in element by checking if there is an option which appear only once in the element.(hidden single)
        public static bool solve_single_inArray(Cell[] element, Board board)
        {
            bool flag = false;
            int value;
            int count;
            int[] optionscount = generate_options_count(element, board);

            while ((value = Array.IndexOf(optionscount, 1) + 1) != 0)
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

        //the function solves all hidden singles in board.
        public static bool solve_hidden_singles(Board board)
        {
            int i;
            bool flag = false;
            for (i = 0; i < board.length; i++)
            {
                if (solve_single_inArray(board.GetRow(board.matrix, i), board))
                    flag = true;

            }
            for (i = 0; i < board.length; i++)
            {

                if (solve_single_inArray(board.GetColumn(board.matrix, i), board))
                    flag = true;


            }
            for (i = 0; i < board.length; i++)
            {

                if (solve_single_inArray(board.GetRow(board.boxmatrix, i), board))
                    flag = true;

            }
            return flag;
        }

        //the function solves the board by the tactics hidden single and naked single.
        public static void solve_singles(Board board)
        {
            while (update_options(board) || solve_hidden_singles(board))
            {

            }
        }


        //the function checks if all the cells that contains the option are in the same row/collumn, if they are, it returns this row/collum otherwise returns null.
        public static Cell[] options_in_same_element(int option, Cell[] box, Board board)
        {
            int[] rowcounter = new int[board.scale];
            int[] colcounter = new int[board.scale];
            int startrow = box[0].row, row, col;
            int startcol = box[0].col;
            foreach (Cell cell in box)
            {
                if (!cell.hasValue() && cell.options.Contains(option))
                {
                    rowcounter[cell.row % board.scale] = 1;
                    colcounter[cell.col % board.scale] = 1;
                }
            }
            if (rowcounter.Sum() == 1)
            {
                row = Array.IndexOf(rowcounter, 1);

                return board.GetRow(board.matrix, row + startrow);

            }
            if (colcounter.Sum() == 1)
            {
                col = Array.IndexOf(colcounter, 1);
                return board.GetColumn(board.matrix, col + startcol);
            }
            return null;
        }

        //the function solves the board with intersection tactic:
        // if the placing of an option in a box is in only one row/collumn,
        // remove the option from all the other cells in the row/collumn which are not in the box.
        public static bool solve_intersection(Board board)
        {
            Cell[] box;
            Cell[] element;
            int[] optionscount;
            int option;
            bool flag = false;
            for (int i = 0; i < board.length; i++)
            {
                box = board.GetRow(board.boxmatrix, i);
                optionscount = generate_options_count(box, board);
                for (int j = 0; j < board.length; j++)
                {
                    if (optionscount[j] > 1)
                    {
                        option = j + 1;
                        if ((element = options_in_same_element(option, box, board)) != null)
                        {
                            foreach (Cell cell in element)
                            {
                                if (cell.box != i)
                                {
                                    if (cell.remove(option))
                                    { flag = true;
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

        //returns the next empty cell in the board
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
        //the function gets the board and try to solve it using brute force -> check every possible combination.
        public static bool brute_force(Board board)
        {
            Cell nextempty = next_pos(board);
            if (nextempty == null)
                return true;
            int[] options = nextempty.options;
            foreach (int option in options)
            {
                nextempty.setValue(option);
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
        public static bool fast_brute_force(Board board, int i)
        {
            Cell nextempty = next_pos(board);
            if (nextempty == null)
                return true;
            int[] options = nextempty.options;

            foreach (int option in options)
            {
                if (board.check_valid_guess(nextempty, option))
                {
                    nextempty.setValue(option);
                    if (fast_brute_force(board, i + 1))
                    {
                        return true;
                    }
                    nextempty.options = options;
                }
            }
            return false;
        }
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
        public static void tactics(Board board)
        {
            bool flag = true;
            while (flag)
            {
                solve_singles(board);
                flag = solve_intersection(board);
            }
        }

        public static Cell leastoptions(Board board)
        {
            Cell cell = next_pos(board);
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
        //the main function which gets a board and solves it.
        public static bool solve(Board board)
        {
            if (next_pos(board) == null)
                return true;
            if (board.check_valid() == false)
                return false;
            tactics(board);
           
            if (board.check_valid() == false)
                return false;
            if (next_pos(board) == null)
                return true;
            
            Cell cell = leastoptions(board);
            Cell[,] clone = copy(board.matrix);
            if (cell.options.Length == 0)
                return false;
            foreach(int option in cell.options)
            {
                cell = leastoptions(board);
                cell.setValue(option);
                if (solve(board))
                    return true;
                board.matrix = copy(clone);
                board.boxmatrix_init();
            }
           
            return false;
            
        }
    }
}
