using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Sudoku_solver_Aviv_Ovadia
{
    class Program

    {  
        
        
        static void Main(string[] args)
        {    //10 - : 11 - ; 12 - < 13 - = 14 - > 15 - ? 16 - @
            int i = 1;
            string valid4x4 = "040080@0;010060>30090?04:70=00<006002;0080003000?000000=00>00204" +
                              "0008030070005000000000>0000:0@0=009250000800;000<010000@00=00030" +
                              "000<?0000600=047@1=0:00300700900600000;04009002:03000@000>800;00" +
                              "5;3:000800<400010>00;9000=?04050=040600000020<090<0@0=00010060?0";
            string oriboard = "0090104576;<=0?00700280040005:0<0<00000015:=28390000000023984167000856009:<0>0@=1300000002407<:600<0702030=189450=0004<0875632108103000:5<0@96=>05600003=48:<72000000090>032:@5100>=000<69170483061530000879@=<400070004012365>800320>07<=65009:00009000:@>41372";

            string valid4x42 = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            string valid4x43 = "00001:07004000@03070002000;0400<00410<000>782?0:0=:00;>0@00053605:;0001>0006000=0000<30:0@80?>00400050?020<=000000008000090>0004200070;0000<000000006=04070@000>00@60?5030>;0000;0003000=500064?0;2?00050<900@=0<05@=8:00060;900800406000;000:020>00070080=20000";
            string validgrid1 = "410007560800009401500040000730000600946000813005000042000030004601200007094100086";//easy
            string validgrid2 = "002501700600807009080204010007602500050000090004109200070305060500908007008706900";//moderate
            string validgrid3 = "000659000009208100020103050738000614400000003192000578070401060004506700000732000";//hard
            string validgrid4 = "060403020400109006001506400627000318000000000583000974006708200200304009010602040";//very hard
            string validgrid5 = "800700003304050080000008500085006300200000006007100940006800000010090608700005004";//extreme
            string validgrid6 = "020005900109308400603900000300004001000209000700100009000001604001406205004500080";//Ultra extreme
         
            string validgrid7 = "080010020600305001007000400020109050700000006090603040005000300900201008030060070";
            string validgrid8 = "007080200600702000090501060700009008400307002300800009010408050000905006008060900";
            string validgrid9 = "123000000456000000000000000000000000000000000000000000000000000000000000000000000";
            string custom= string.Concat(Enumerable.Repeat("0", 256));
            string teo = "410000000000000730000009005000800900006700120204006000000004000031050060000013000";
            
            
            Stopwatch sw = new Stopwatch();
            //sw.Start();
            Board board = new Board();
            board.get_matrix(valid4x43);
            board.display();
          
            Console.WriteLine("Solving singles...");
            sw.Start();
           
            
            Solver.solve_singles(board);
            Solver.solve_intersection(board);
            Solver.solve_singles(board);
            Solver.solve_intersection(board);
            Solver.solve_singles(board);
            Solver.solve_intersection(board);
            Solver.solve_singles(board);
            sw.Stop();
            board.display();
            
            //Console.WriteLine("time elapsed:{0} ", sw.ElapsedMilliseconds + " miliseconds");
            //Console.WriteLine("Starting brute force...");
            //sw.Start();
            //Solver.brute_force(board);
            //sw.Stop();
            //board.display();




            Console.WriteLine("solved!!!");
            Console.WriteLine("time elapsed:{0} ", sw.ElapsedMilliseconds + " miliseconds");

            Console.ReadKey();
        }
    }
}
