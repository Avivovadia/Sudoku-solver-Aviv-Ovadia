using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Sudoku_solver_Aviv_Ovadia
{
    class FileHandling
    {
        public string path { get; set; }
        public string filename { get; set; }
        public StreamReader sr { get; set; }

        public FileHandling(string filepath)
        {
            this.filename = Path.GetFileName(filepath); //File name
            this.path = (new FileInfo(filepath)).DirectoryName; //directoryName
           // this.sr = new StreamReader(filepath);
        }
        //the function returns the data of the file.
        public string getData()
        {
            string data = "";
            if (File.Exists(path+"\\"+filename))
            {
                data = File.ReadAllText(path+"\\"+filename);
                return data;
            }
            return null;
        }
        //the function creates a new file which contains the solution for the solved board in Boards directory
        public void createSolutionBoard(string data)
        {
            string rootpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")); //root path
            string boardspath = rootpath + "\\Sudoku solver Aviv Ovadia\\Boards\\"; //directory path
            string postfix = ".txt";
            string newpath = boardspath + filename.Remove(filename.Length - 4, 4) + "_Solution" + postfix;
            using (StreamWriter sw1 = File.CreateText(newpath))
            {
                sw1.WriteLine(data);
            }
        }
    }
}
