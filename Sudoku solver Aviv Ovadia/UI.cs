using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace Sudoku_solver_Aviv_Ovadia
{
    class UI
    {
       //the function handles one sudoku-> gets input from user and solves sudoku.
       public static void HandleSudoku()
        {
            int choice = 0;
            bool IsNum,valid=true;
            string data = "", filename = "";
            FileHandling fh=null;
            Console.WriteLine("\nenter sudoku:\npress 1 for entering string\npress 2 for choosing file name from Boards directory\npress 3 for chossing full path");
            while (choice>3||choice<1)
            {
                IsNum = true;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch(System.FormatException e)
                {
                    Console.WriteLine("Invalid Input, choose from 1 to 3");
                    IsNum = false;
                }
                if((choice > 3 || choice < 1)&&IsNum)
                    Console.WriteLine("choose from 1 to 3");
            }
            if(choice==1)
            {
                Console.WriteLine("\n\nenter data string:");
                int bufSize = 1024;
                Stream inStream = Console.OpenStandardInput(bufSize);
                Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));
                data = Console.ReadLine();
            }
            if (choice == 2)
            {
                string rootpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")); //root path
                string boardspath = rootpath + "\\Sudoku solver Aviv Ovadia\\Boards\\"; //directory path
                Console.Write("\n\nenter file name: ");
                filename = Console.ReadLine();
                fh = new FileHandling(boardspath + filename);
                data = fh.getData();
                if (data == null) //handle file don't exist
                {
                    Console.WriteLine("file does not exist in Boards directory");
                    valid = false;
                }
            }
            if (choice == 3)
            {
              
                Console.Write("\n\nenter path: ");
                filename = Console.ReadLine();
                if (filename != "")
                {
                    fh = new FileHandling(filename);
                    data = fh.getData();
                }
                else
                    data = null;
                if (data == null)
                {
                    Console.WriteLine("No sudoku grid found in input path");
                    valid=false;
                }

            }
            if (valid)
            {
                solveSudoku(choice, data,fh);
            }
        }

        //the function solves sudoku, if the input is by string it prints the solved string and if the input is by file it creates solution file
       public static void solveSudoku(int choice, string data, FileHandling fh)
       {
            try
            {
                Board board = new Board(data);
                Stopwatch sw = new Stopwatch();
                board.display();
                sw.Start();
                bool solved = Solver.solve(board);
                sw.Stop();
                if (solved)
                {
                    string solveddata = board.convert_to_string();
                    board.display();
                    Console.WriteLine("solved!!!");
                    Console.WriteLine("time elapsed:{0} ", sw.ElapsedMilliseconds + " miliseconds");
                    if (choice == 1)
                    {
                        Console.WriteLine("the solved string is:");
                        Console.WriteLine(solveddata);
                    }
                    else
                    {
                        fh.createSolutionBoard(solveddata);
                        Console.WriteLine("created "+ fh.filename.Remove(fh.filename.Length - 4, 4) + "_Solution.txt in Boards directory");
                    }
                }
                else
                    Console.WriteLine("couldn't solve");
            }
            catch(InvalidInputException e)
            {
                Console.WriteLine(e.Message);
            }               
       }
    }
}
