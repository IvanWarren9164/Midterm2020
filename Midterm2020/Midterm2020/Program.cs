using System;
using System.Collections.Generic;
using System.IO;

namespace Midterm2020
{
    class Program
    {
        static void Main(string[] args)
        {
            bool userContinue = true;
            List<Book> appStartList = Library.BuildLibraryFromText();
            while (userContinue)
            {
                uint userSelection = PromptForAction();
                RunAction(userSelection, appStartList);
                userContinue = ShouldContinue();
            }
            Library.UpdateLibary(appStartList);
        }

        public static uint PromptForAction()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Would you like to:");
                Console.WriteLine("[1]: View All Books");
                Console.WriteLine("[2]: Search Books");
                Console.WriteLine("[3]: Checkout Book");
                Console.WriteLine("[4]: Return Book");
                Console.WriteLine("[5]: Create New Book");
                if (uint.TryParse(Console.ReadLine(), out uint userSelection) && userSelection < 6)
                {
                    return userSelection;
                }
            }

        }
        public static List<Book> RunAction(uint selection, List<Book> listOfBooks)
        {
            if (selection == 1)
            {
                Library.ReadLibrary();
                return listOfBooks;
            }
            else if (selection == 2)
            {
                Library.SearchForBook(listOfBooks);
                return listOfBooks;
            }
            else if (selection == 3)
            {
                Library.CheckOutReturn(listOfBooks, false);
                return listOfBooks;
            }
            else if (selection == 4)
            {
                Library.CheckOutReturn(listOfBooks, true);
                return listOfBooks;
            }
            else
            {
                listOfBooks.Add(Library.CreateBook());
                return listOfBooks;

            }
        }
        public static bool ShouldContinue()
        {
            while (true)
            {
                Console.WriteLine("Would you like to do something else?");
                Console.WriteLine("[1]: Yes");
                Console.WriteLine("[2]: No");
                if (uint.TryParse(Console.ReadLine(), out uint userContinue) && userContinue < 3)
                {
                    if (userContinue == 1)
                    {
                        return true;
                    }
                    else return false;
                }
                else
                {
                    Console.WriteLine("Sorry not a valid selection");
                }
            }
        }
    }
    public static class Global // Create a path variable.
    {
        public static string path = Path.Combine(Environment.CurrentDirectory, @"\Midterm2020\Midterm2020\libary.txt");
        public static string libaryPath = "../../../libary.txt";

    }
}