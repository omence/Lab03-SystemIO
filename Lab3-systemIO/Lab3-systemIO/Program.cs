using System;
using System.IO;
using System.Linq;
namespace Lab3_systemIO
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../hangman.txt";
            Console.WriteLine("Hello World!");
            CreateFile(path);
            UserInterface(path);
        }

        static void UserInterface(string path)
        {
            //interface is running

            bool running = true;

            //while the interface is runn run this code
            while (running)
            {
                Console.WriteLine("please choose option 1, 2, or 3");
                Console.WriteLine("1: Add a word");
                Console.WriteLine("2: Delete a word");
                Console.WriteLine("3: See all words");
                Console.WriteLine("4: Play Game");
                try
                {
                    string userSelection = Console.ReadLine();

                    int userSelect = Convert.ToInt32(userSelection);


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
                catch
                {
                    Console.WriteLine("please choose a valid option");
                }
            }
        }

        public static string CreateFile(string path)
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

        static void AddToFile(string path, string userAdd)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(userAdd);
            }
        }

        public static void DeleteFromFile(string path, string userDelete)
        {
            try
            {
                if (userDelete.Length > 0)
                {
                    string[] wordsInFile = File.ReadAllLines(path);
                    foreach (string word in wordsInFile)
                    {
                        if (string.Equals(word, userDelete, StringComparison.CurrentCultureIgnoreCase))
                        {
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
                Console.WriteLine("Press Enter to Continue");
            }
        }

        static void ViewAllWords(string path)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

        static string GetRandomWord(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Random line = new Random();
            int index = line.Next(lines.Length);
            return lines[index];
        }

        static void Play(string path)
        {
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

                    if(!renderWord.Contains(" _ "))
                    {
                        Console.WriteLine("You Win");
                        userWins = true;
                    }
                }


            }
        }
    }
}
