using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku_solver_Aviv_Ovadia
{
    class Board  //Board class repressents a full sudoku board. It has a matrix of Cells,length and scale.
    {   public Cell[,] matrix { get; set; }
        public Cell[,] boxmatrix { get; set; } //every row in this matrix is a box, it makes it easier to check boxes.
        //both matrix and boxmatrix point to the same cells.
        public int length { get; set; }
        public int scale { get; set; }


        public Board(string str)
        {
            get_matrix(str);
        }
        //sets the color of the text to white or green, used in displaying the board.
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
                        else if (j % 6 == 3 || j % 6 == 4)

                        {
                            if (j % 6 == 3)
                            {
                                if (matrix[i / 3, j / 6].hasValue())
                                {
                                    if (matrix[i / 3, j / 6].value() >= 10)
                                        Console.Write(matrix[i / 3, j / 6].value());
                                    else
                                        Console.Write(matrix[i / 3, j / 6].value() + " ");
                                }
                                else
                                    Console.Write("  ");
                            }
                        }
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
        
        //two functions meant to help getting data from matrixes.
        public Cell[] GetColumn(Cell[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }
        public Cell[] GetRow(Cell[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
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
                    throw new InvalidInputException(chr,this.length);
                }
            }
        }

        //the function checks if the numbers in the matrix are placed legally
        //(not 2 of the same number in the same row,col,box). else, throw exception(not valid placing exception).
        public bool check_valid()
        {
            Cell cell;
            Cell[] element;
            int i, j, k;
            bool flag = true;
            for(i = 0; i < length; i++)
            {
                for ( j = 0; j < length; j++)
                {
                    cell = matrix[i, j];
                    if (cell.hasValue() )
                    {
                        element = GetRow(matrix, cell.row);
                        for( k = 0; k < length; k++)
                        {
                            if (element[k].value() == cell.value() && cell != element[k])
                            {
                                //throw new InvalidInputException();
                               
                                flag = false;
                                break;
                            }
                        }
                        if (!flag)
                            break;
                        element = GetColumn(matrix, cell.col);
                        for (k = 0; k < length; k++)
                        {
                            if (element[k].value() == cell.value() && cell != element[k])
                            {
                                // throw new InvalidInputException();
                               
                                flag = false;
                                break;
                            }
                        }
                        if (!flag)
                            break;
                        element = GetRow(boxmatrix, cell.box);
                        for (k = 0; k < length; k++)
                        {
                            if (element[k].value() == cell.value() && cell != element[k])
                            {
                                //throw new InvalidInputException();
                                
                                flag = false;
                                break;
                            }

                        }
                        if (!flag)
                            break;
                    }
                }
                if (!flag)
                    break;
            }
            return flag;

        }
       

        //the function returns if the board would be valid if the value was put in the cell.(checks if there is another neighbor cell which it's value is value)
        public bool check_valid_guess(Cell cell, int value)
        {
            Cell[] box = this.GetRow(boxmatrix, cell.box);
            Cell[] row = this.GetRow(matrix, cell.row);
            Cell[] col = this.GetColumn(matrix, cell.col);
            bool flag = true;
            for(int i = 0; i < this.length; i++)
            {
                if (box[i] != cell && box[i].value() == value)
                    flag = false;
                if (row[i] != cell && row[i].value() == value)
                    flag = false;
                if (col[i] != cell && col[i].value() == value)
                    flag = false;
            }
            return flag;
        }

        public void boxmatrix_init()
        {
            Cell cell;
            int index;
            boxmatrix = new Cell[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    cell = matrix[i, j];

                    index = 0;
                    while (boxmatrix[cell.box, index] != null) { index++; }
                    boxmatrix[cell.box, index] = cell;
                }
            }
        }
        //the function initialize the matrix, and initialize each cell inside it corresponds to the input board.
        public void matrix_init(string str)
        {
            int value;
            Cell[] element;
            Cell cell;
            
            int index;
            this.matrix = new Cell[length, length];
            this.boxmatrix = new Cell[length, length];

            for (int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    value = str[i * length + j] - '0';
                    matrix[i, j] = new Cell(scale, i, j, value);
                }
            }
            boxmatrix_init();
        }


        //the function gets an input, if the input is valid, create a fitting matrix and stores inside it the input board. 
        public void get_matrix(string str)
        {
            check_input_size(str);
            check_input_keys(str);
            matrix_init(str);
            if(!check_valid())
                throw new InvalidInputException();

        }
    }
}
