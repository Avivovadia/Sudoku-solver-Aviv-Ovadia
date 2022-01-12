﻿using System;
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
        //the functoin update the options of all the cells, returns whether there was a change in the options or not.
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
        //the function gets the board and try to solve it using brute force -> check every possible combination.
        public void brute_force(Board board)
        {

        }

        //the main function which gets a board and solves it.
        public void solve(Board board)
        {

        }
    }
}
