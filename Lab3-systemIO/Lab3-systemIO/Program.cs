using System;
using System.IO;
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
                            GetRandomWord(path);
                            break;
                            ////default
                            //default:
                            //    Environment.Exit(0);
                            //    break;

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

        static void DeleteFromFile(string path, string userDelete)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == userDelete)
                {
                    
                }
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

        static void GetRandomWord(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Random line = new Random();
            int index = line.Next(lines.Length);
            Console.WriteLine(lines[index]);
        }
    }
}
