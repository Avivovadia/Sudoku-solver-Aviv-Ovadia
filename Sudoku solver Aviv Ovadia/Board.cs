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


        public Board()
        {

        }
        //sets the color of the text to white or green
        private void setcolor(bool flag)
        {
            if (flag)
                { Console.ForegroundColor = ConsoleColor.Green; }
            else
                Console.ForegroundColor = ConsoleColor.White;
        }

        //displays the board nicely
        public void display()
        {
            int i, j;
            for (i = 0; i < 3 * length; i++)
            {
                
                for (j = 0; j < 6 * length; j++)
                {
                    setcolor(false);
                    if (i % (scale * 3) == 0&& (j % (scale * 6) == 0||j%6!=0))
                        setcolor(true);
                    if (j % (scale * 6) == 0)
                        setcolor(true);
                    if (i == 0) {
                        setcolor(true);
                        if (j == 6 * length - 1)
                        {
                            setcolor(true);
                            Console.WriteLine("_");
                        }
                        else
                            Console.Write("_");
                    }
                    else if (j == 6 * length - 1)
                    {

                        if (i % 3 == 0)
                        {
                            Console.Write("_");
                            setcolor(true);
                            Console.WriteLine("|");
                        }
                        else
                        {
                            setcolor(true);
                            Console.WriteLine(" |");
                        }
                    }
                    else if(i % 3 == 0){
                        if(j%6==0)
                            Console.Write("|");
                        else
                            Console.Write("_");
                    }
                    else if (i % 3== 1)
                    {
                        if (j % 6 == 0)
                            Console.Write("|");
                       else
                            Console.Write(" ");
                    }
                    else if (i % 3 == 2)
                    {
                        if (j % 6 == 0)
                            Console.Write("|");
                        else if (j % 6 == 3)
                            Console.Write(matrix[i / 3, j / 6].value());
                        else
                            Console.Write(" ");
                    }

                }
            }
              for(j=0;j<length*6;j++)
             {  setcolor(false);
                if (j % 6 == 0)
                {
                    if (j % (6 * scale) == 0)
                        setcolor(true);
                    Console.Write("|");
                }
                else
                {
                    setcolor(true);
                    Console.Write("_");
                }
             }
            Console.WriteLine("|\n\n");
            setcolor(false);
        }


        //the function checks if the length of the input fits for a sudoku board -> the length is a power of 4 to some number.
        //if it fits, update the length and the scale appropriately, if not, throws exception (not valid size exception)
        public void check_input_size(string str)
        {
            int length = str.Length;
            if (Math.Sqrt(Math.Sqrt(length)) != Math.Ceiling(Math.Sqrt(Math.Sqrt(length))))
                throw new InvalidInputException(length);
            else
            {
                this.length = (int)Math.Sqrt(length);
                this.scale =(int)Math.Sqrt(this.length);
            }
        }


        //the function checks each key of the input, if there is an invalid key throw exception (not valid key exception).
        public void check_input_keys(string str)
        {
            int value;
            foreach(char chr in str){
                value = chr - '0';
                if (value < 0 || value > this.length)
                {
                    throw new InvalidInputException(chr);
                }
            }
        }


        //the function initialize the matrix, and initialize each cell inside it corresponds to the input board.
        public void matrix_init(string str)
        {
            int value;
            this.matrix = new Cell[length, length];
            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    value = str[i * length + j] - '0';
                    matrix[i, j] = new Cell(scale, i, j, value);
                }
            }
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
