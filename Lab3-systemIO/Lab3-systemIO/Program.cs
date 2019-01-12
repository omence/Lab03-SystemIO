using System;
using System.IO;
using System.Linq;

namespace Lab3SystemIO
{
    public class Program
    {   /// <summary>
    /// calls the interface and creats file to interact with words in game.
    /// </summary>
    /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string path = "../../../hangman.txt";
            Console.WriteLine("Welcome to the word guessing game");
            CreateFile(path);
            UserInterface(path);
        }

        /// <summary>
        /// allows the user to interact with the game
        /// </summary>
        /// <param name="path">is location of file with words for game</param>
        public static void UserInterface(string path)
        {
            //interface is running

            bool running = true;

            //while the interface is runn run this code
            while (running)
            {
                Console.WriteLine("please choose option 1, 2, 3, 4 or 5");
                Console.WriteLine("1: Add a word");
                Console.WriteLine("2: Delete a word");
                Console.WriteLine("3: See all words");
                Console.WriteLine("4: Play Game");
                Console.WriteLine("5: Exit");
                try
                {

                    string userSelection = Console.ReadLine();

                    int userSelect = Convert.ToInt32(userSelection);

                    if (userSelect == 1 || userSelect == 2 || userSelect == 3 || userSelect == 4 || userSelect == 5)
                    {
                        switch (userSelect)
                        {
                            //if user chooses withdrawal
                            case 1:
                                Console.WriteLine("What word would you like to add?");
                                string userAdd = Console.ReadLine().ToUpper();
                                AddToFile(path, userAdd);
                                Console.WriteLine($"Your word was added");
                                Console.ReadLine();
                                break;

                            case 2:
                                Console.WriteLine("What word would you like to delete?");
                                string userDelete = Console.ReadLine().ToUpper();
                                DeleteFromFile(path, userDelete);
                                break;

                            case 3:
                                ViewAllWords(path);

                                break;

                            case 4:
                                Play(path);
                                break;

                            default:
                                Environment.Exit(0);
                                break;

                        }
                    }
                    else
                    {
                        Console.WriteLine("Please Choose a valid option, press enter to continue");
                        Console.ReadLine();
                        UserInterface(path);

                    }
                }
                catch
                {
                    Console.WriteLine("please choose a valid option");
                }
            }
        }

        /// <summary>
        /// creats a txt file to store words from gale
        /// </summary>
        /// <param name="path">Location of file</param>
        /// <returns>string</returns>
        public static string CreateFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return "file exists";
                }
                else
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        streamWriter.WriteLine("DOG");
                    }
                    return " ";
                }
            }
            catch (Exception e)
            {
                throw e;

            }

        }

        /// <summary>
        /// Add a word to the txt file, user can add words
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userAdd">word user wants to add</param>
        public static void AddToFile(string path, string userAdd)
        {
            try
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(userAdd);
                }
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        /// <summary>
        /// Deletes a word from the txt file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userDelete">word user wants to delete</param>
        public static void DeleteFromFile(string path, string userDelete)
        {
            try
            {
                if (userDelete.Length > 0)
                {
                    string[] wordsInFile = File.ReadAllLines(path);
                    foreach (string word in wordsInFile)
                    {   
                        //check if delete word rwuest equals a word in list, and ignores differences in case
                        if (string.Equals(word, userDelete, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //the new list of words without deleted word
                            string[] newFileList = new string[wordsInFile.Length - 1];
                            int counter = 0;
                            for (int i = 0; i < newFileList.Length; i++)
                            {
                                if (userDelete == wordsInFile[counter])
                                {
                                    i--;
                                    counter++;
                                }
                                else
                                {
                                    newFileList[i] = wordsInFile[counter];
                                    counter++;
                                }
                            }
                            //this send the new list of words to the txt file
                            using (StreamWriter streamWriter = new StreamWriter(path))
                            {
                                for (int i = 0; i < newFileList.Length; i++)
                                {
                                    streamWriter.WriteLine(newFileList[i]);
                                }
                            }
                            Console.WriteLine($"{userDelete} Deleted.");
                            return;
                        }

                    }
                    Console.WriteLine($"{userDelete} does not exist");

                }
                else
                {
                    throw new Exception("Please enter word to be deleted.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Try again");
            }
        }

        /// <summary>
        /// View all the words in the txt file
        /// </summary>
        /// <param name="path">location of file</param>
        public static string ViewAllWords(string path)
        {
            //inputs words from list into array and then renders array to console
            string[] lines = File.ReadAllLines(path);
            try
            {   
                
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine(lines[i]);
               
                }
            }
            catch (Exception e)
            {
                throw e;
                
            }
            return lines[0];
        }

        /// <summary>
        /// Gets a random word from the txt file 
        /// </summary>
        /// <param name="path">location of txt file</param>
        /// <returns></returns>
        public static string GetRandomWord(string path)
        {
            try
            {   //gets randome word from array of words from txt
                string[] lines = File.ReadAllLines(path);
                Random line = new Random();
                int index = line.Next(lines.Length);
                return lines[index];
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        /// <summary>
        /// all the functionality for the game
        /// </summary>
        /// <param name="path">location of file</param>
        public static void Play(string path)
        {
            try
            {
                //getting word from random method and setting vars for later
                string word = GetRandomWord(path);
                string userGuess = " ";
                string[] renderWord = new string[word.Length];

                for (int i = 0; i < word.Length; i++)
                {
                    renderWord[i] = " _ ";
                }

                foreach (string l in renderWord)
                {
                    Console.Write(l);
                }

                Console.WriteLine();

                bool userWins = false;
                while (!userWins)
                {
                    //logic for determining if user guess is a match
                    Console.WriteLine("Guess a Letter");
                    string letter = Console.ReadLine();

                    if (letter != null && (word.ToLower().Contains(letter.ToLower()) && !userGuess.Contains(letter)))
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i].ToString().ToLower() == letter)
                            {
                                renderWord[i] = letter;
                                userGuess += letter;
                            }
                            else
                            {
                                Console.Write(renderWord[i]);
                            }
                        }

                        Console.WriteLine($"You Guessed: {userGuess}");

                        if (!renderWord.Contains(" _ "))
                        {
                            Console.WriteLine("You Win");
                            userWins = true;
                        }
                    }


                }
            }
            catch (Exception e)
            {
                throw e;

            }
        }
    }
}
