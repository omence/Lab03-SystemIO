using System;
using Xunit;
using System.IO;
using Lab3SystemIO;

namespace XUnitTestProject1
{

    public class UnitTest1
    {
        [Fact]
        public void CanViewAllWordsWorks()
        {
            //arrange
            string path = "../../../hangman.txt";

            //act
            string index = Program.ViewAllWords(path);

            //assert
            Assert.Equal("DOG", index);

        }

        [Fact]
        public void AddWordWorks()
        {
            //arrange
            string path = "../../../hangman.txt";
            string userAdd = "CAT";
            //act
            string index = Program.AddToFile(path, userAdd);

            //assert
            Assert.Contains("CAT", userAdd);

        }

        [Fact]
        public void DeleteWordWorks()
        {
            ////arrange
            string path = "../../../hangman.txt";
            string useradd = "BAG";
            string index = Program.AddToFile(path, useradd);

            string[] wordsInFile = File.ReadAllLines(path);
            string userDelete = "BAG";
            
            //act
            Program.DeleteFromFile(path, userDelete);

            string[] newA = File.ReadAllLines(path);
            //assert
            Assert.DoesNotContain(userDelete, newA);

        }

        

    }
}
