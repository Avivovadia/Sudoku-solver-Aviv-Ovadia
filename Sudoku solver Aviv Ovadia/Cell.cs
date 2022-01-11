﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver_Aviv_Ovadia
{
    class Cell //Cell class repressents a single cell in the board, has fields of its row, collum, box
               //and its options to be put inside it.
    {
        public int row { get; set; }
        public int col { get; set; }
        public int box { get; set; }
        public int[] options { get; set; } //array of options
       
      
        public Cell(int row,int col,int box,int value=0)
        {
            this.row = row;
            this.col = col;
            this.box = box;
            if (value == 0){
                this.options = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }
            else
            {
                this.options = new int[] { value };
            }

        }

        //the function returns the value of the cell, if it doesn't have value yet -> returns 0.
        public int value() 
        {
            if (this.options.Length == 1)
                return this.options[0];
            return 0;
        }
        //the function removes the value from the cell's options if its inside the array,
        //also returns whether the value got removed or not.
        public bool remove(int value)
        {
           int pos = Array.IndexOf(options, value);
           bool flag = false;
           if (pos != -1)
            {
                options = options.Where(num => num != value).ToArray();
                flag = true;
            }
            return flag;
        }
        //the function prints the cell's properties -> auxiliary function for testing and debugging.
        public void show()
        {
            Console.WriteLine("row:" + row + " col:" + col+" box:"+box);
            Console.WriteLine("options:");
            Console.WriteLine("[{0}]", string.Join(", ", this.options));

        }
    }
}
