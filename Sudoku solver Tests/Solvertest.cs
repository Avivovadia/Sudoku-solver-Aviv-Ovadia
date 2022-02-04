using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Sudoku_solver_Aviv_Ovadia;
namespace Sudoku_solver_Tests
{
    [TestClass]
    public class Solvertest
    {
        
            [TestMethod]
            public void solve_1x1_ReturnsTrue()
            {
                string data = "0";
                Board board = new Board(data);

                bool result = Solver.solve(board);
                
                Assert.IsTrue(result); 

            }
            [TestMethod]
            public void solve_4x4_ReturnsTrue()
            {
                string data = "0010204003000004";
                Board board = new Board(data);

                bool result = Solver.solve(board);

                Assert.IsTrue(result);

            }
             [TestMethod]
            public void solve_invalid4x4_ReturnsFalse()
            {
                string data = "0010230000000400";
                Board board = new Board(data);

                bool result = Solver.solve(board);

                Assert.IsFalse(result);

            }
        [TestMethod]
        public void solve_9x9_ReturnsTrue()
        {
            string data = "007080200600702000090501060700009008400307002300800009010408050000905006008060900";
            Board board = new Board(data);

            bool result = Solver.solve(board);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void solve_16x16_ReturnsTrue()
        {
            string data = "102000;680054<00>00;08:0<09007000<00000002700?090090070000:0>85;0:0@1002;40600080300000900000000;942050>00=030000000008@3920040000100:?39600000000060900@0<02;4>00000000200000102000@0>8100=<06054?10>0000600@0060@00250000000<000<00@0:0710=00400:>?00;43000501";
            Board board = new Board(data);

            bool result = Solver.solve(board);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void solve_empty16x16_ReturnsTrue()
        {
            string data= string.Concat(Enumerable.Repeat("0", 256));

            Board board = new Board(data);

            bool result = Solver.solve(board);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void solve_25x25_ReturnsTrue()
        {
            string data = "00<60070B05H0:1004000000020C0=000:00000000B50000010000000F000030200><0@8I000@0002G00=<F000E?C30000>0G0H00000I840@CE0070003<0904020000000:0H<A@00050000009006I0008053000000D00BC?0:;000B<C0000000G00700400000000>0F00B@D06;=0000000F0I001A54700>083E00;00060D=?00000090020I0180050E0010000@:07004D00900>0H0A0I2500000=00000F00000C1800007E00<02A000B6@00?00=0:08:B<@900050000C00A0E0?00F0800?03060E0070B>50100000000C010@;000:FI?000000E000310E004000020=0HI00>00600000000?0<>06AH0000000=0005G@40=H720900?30F000000800ID20C000010000E300<0000@<050;E0G00?0000C900000I:00009DF74030>IB0;0000010?H060F80I>0:;090D1@070G00=>=E100500060F0G:000200B7;";
            Board board = new Board(data);

            bool result = Solver.solve(board);

            Assert.IsTrue(result);
        }

    }
}
