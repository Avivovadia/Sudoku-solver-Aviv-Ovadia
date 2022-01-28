using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sudoku_solver_Aviv_Ovadia
{
    class Program

        //the function gets filename and data string, creating in Boards directory a solution file for board with name filename, the data is the solution.
    {   public static void createSolvedFile(string filename,string data)
        {
            string rootpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")); //root path
            string boardspath = rootpath + "\\Sudoku solver Aviv Ovadia\\Boards\\"; //directory path
            string postfix = ".txt";
            string path = boardspath + filename + "_Solution" + postfix;
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
            {
                sw.WriteLine(data);
            }
            Console.WriteLine("created solution file: "+filename+"_Solution"+postfix);

        }
        //the function gets a file name and returns its text, if the file don't exist in Boards directory, returns null
        public static string getBoardFromFile(string filename)
        {
            string rootpath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")); //root path
            string boardspath = rootpath + "\\Sudoku solver Aviv Ovadia\\Boards\\"; //directory path
            string postfix = ".txt";
            string path = boardspath + filename+postfix; //file path 
            string data = "";
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
                return data;
            }
            return null;
        }
        //solves valid sudoku, if input is string, prints the solved string, if input is file, create new solution file.
        public static void Solvesudoku(int choice, string data, string filename)
        {
            try
            {   Board board = new Board(data);
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
                        createSolvedFile(filename, solveddata);
                    }
                    if (choice == 0)
                    {
                        Console.WriteLine("the solved string is:");
                        Console.WriteLine(solveddata);
                    }
                }
                else
                    Console.WriteLine("couldn't solve...");
            }
            catch(InvalidInputException e)
            {
                Console.WriteLine(e.Message);
            }     
        }
        //taking input from the user and solves one sudoku.
        public static void HandleSudoku()
        {
            int choice=99;
            bool valid = true;
            string data,filename="";
            Console.Write("\nChoose way to input a sudoku:\n0 for entering string, 1 for choosing a file from Boards directory:");         
            while (choice != 1 && choice != 0)
            {
                bool Isnum = true;
                try
                {
                    choice = Convert.ToInt32(Console.ReadKey().KeyChar)-48;
                }
                catch(System.FormatException e)
                {
                    Console.Write("\nInvalid key, try again: ");
                    Isnum = false;
                }
                if (choice != 1 && choice != 0&&Isnum)
                    Console.Write("\nInvalid key, try again: ");
            }
            if (choice == 1)
            {
                Console.Write("\n\nenter file name (without .txt): ");
                filename = Console.ReadLine();
                data = getBoardFromFile(filename);
                if (data == null)
                {
                    Console.WriteLine("file does not exist.");
                    valid = false;
                }
            }
            else
            {
                Console.WriteLine("\n\nenter data string:");
                data = Console.ReadLine();
            }
            if (valid)
            {
                Solvesudoku(choice,data,filename);
            }
        }
        static void Main(string[] args)
        {

            char endingchar = '1';
            Console.WriteLine("Sudoku Solver!\n");
            while (endingchar != '0')
            {
                HandleSudoku();
                Console.Write("\nAgain? (press '0' to stop): ");
                endingchar = Console.ReadKey().KeyChar;
            }
            
        }
    }
}
