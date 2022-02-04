using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sudoku_solver_Aviv_Ovadia;
namespace Sudoku_solver_Tests
{
    [TestClass]
    public class Boardtest
    {
        [TestMethod]
        public void check_input_size_ValidSize_ReturnsTrue()
        {
            string data = "410007560800009401500040000730000600946000813005000042000030004601200007094100086";//size 81
            Board board = new Board();
            bool result=true;
            try {
                board.check_input_size(data);
                
            }
            catch(InvalidInputException e)
            {
                result = false;
            }
            finally { Assert.IsTrue(result); }
            
        }
        [TestMethod]
        public void check_input_size_ValidSize_ReturnsFalse()
        {
            string data = "4100075608000094015000400007300006009460008130050000420000300046012000070941000860";//size 82
            Board board = new Board();
            bool result = true;
            try
            {
                board.check_input_size(data);

            }
            catch (InvalidInputException e)
            {
                result = false;
            }
            finally { Assert.IsFalse(result); }
        }
        [TestMethod]
        public void check_input_keys_ValidKeys_ReturnsTrue()
        {
            string data = "410007560800009401500040000730000600946000813005000042000030004601200007094100086";//size 81
            Board board = new Board();
            board.length = 9;
            board.scale = 3;
            bool result = true;
            try
            {
                board.check_input_keys(data);

            }
            catch (InvalidInputException e)
            {
                result = false;
            }
            finally { Assert.IsTrue(result); }
        }
        [TestMethod]
        public void check_input_keys_InvalidKeys_ReturnsFalse()
        {
            string data = "ab0007560800009401500040000730000600946000813005000042000030004601200007094100086";//size 81
            Board board = new Board();
            board.length = 9;
            board.scale = 3;
            bool result = true;
            try
            {
                board.check_input_keys(data);

            }
            catch (InvalidInputException e)
            {
                result = false;
            }
            finally { Assert.IsFalse(result); }
        }
        [TestMethod]
        public void check_valid_ValidGrid_ReturnTrue()
        {
            string data = "800700003304050080000008500085006300200000006007100940006800000010090608700005004";
            Board board = new Board();
            board.length = 9;
            board.scale = 3;
            board.matrix_init(data);

            bool result = board.check_valid();
           
           Assert.IsTrue(result); 
        }
        [TestMethod]
        public void check_valid_InvalidGrid_ReturnFalse()
        {
            string data = "808700003304050080080008500085006300200000006007100940006808800010090608700005004";
            Board board = new Board();
            board.length = 9;
            board.scale = 3;
            board.matrix_init(data);

            bool result = board.check_valid();


           Assert.IsFalse(result); 
        }

        [TestMethod]
        public void get_matrix_ValidGrid_ReturnTrue()
        {
            string data = "000659000009208100020103050738000614400000003192000578070401060004506700000732000";
            Board board = new Board();
            bool result = true;
            try
            {
                board.get_matrix(data);
            }
            catch(InvalidInputException e)
            {
                result = false;
            }       
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void get_matrix_InvalidGrid_ReturnFalse()
        {
            string data = "000159000009208100020103050738000614400000003192000578070401060004506700000732000";
            Board board = new Board();
            bool result = true;
            try
            {
                board.get_matrix(data);
            }
            catch (InvalidInputException e)
            {
                result = false;
            }
            Assert.IsFalse(result);
        }

    }
}
