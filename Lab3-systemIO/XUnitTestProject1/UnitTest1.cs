using System;
using Xunit;
using Lab3SystemIO;

namespace XUnitTestProject1
{

    public class UnitTest1
    {
        [Fact]
        public void CanretrieveAllWordsFromFIle()
        {
            //arrange
            string path = "../../../hangman.txt";

            //act
            //Program.ViewAllWords(path);

            //assert
            Assert.Contains("DOG", Program.ViewAllWords(path));

        }
       
    }
}
