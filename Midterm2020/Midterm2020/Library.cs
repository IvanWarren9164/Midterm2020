using System;
using System.Collections.Generic;
using System.IO;

namespace Midterm2020
{
    public abstract class Library
    {
        public static void ReadLibrary(List<Book>listOfBooks)
        {
            int num = 1;
            char space = ' ';
            PrintGUIFormat();
            foreach(Book book in listOfBooks)
            {
                Console.WriteLine($"{num.ToString().PadRight(3, space)}| " +
                $"{book.Title.PadRight(45, space)}{book.Author.PadRight(45, space)} " +
                $"{book.Status.ToString().PadRight(45, space)} " +
                $"{book.DueDate.ToString().PadRight(45, space)}");
                num++;
            }
        }
        public static void SearchForBook(List<Book> listOfBooks)
        {
            Console.WriteLine("Would you like to search by Title or Author?: \n" +
                "[1]Title \n" +
                "[2]Author \n");
            string searchCriteria;
            bool searchFlag = true;
            while (searchFlag)
            {
                if (uint.TryParse(Console.ReadLine().Trim(), out uint userSelection) && userSelection < 3)
                {
                    if (userSelection == 1)
                    {
                        Console.WriteLine("Enter the title of the book you are looking for");
                        searchCriteria = Console.ReadLine();
                        PrintSearchForBook(searchCriteria, listOfBooks, true);
                        searchFlag = false;
                    }
                    else if (userSelection == 2)
                    {
                        Console.WriteLine("Enter the author of the book you are looking for");
                        searchCriteria = Console.ReadLine();
                        PrintSearchForBook(searchCriteria, listOfBooks, false);
                        searchFlag = false;
                    }
                    else
                    {
                        Console.WriteLine("Error invalid selection please try again");
                    }
                }
                else
                {
                    Console.WriteLine("Error invalid selection please try again");
                }
            }
        }
        public static void PrintSearchForBook(string searchCriteria, List<Book> listOfBooks, bool bookOrAuthor)
        {
            int num = 1;
            char space = ' ';
            PrintGUIFormat();
            if(bookOrAuthor == true)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Title.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"{num.ToString().PadRight(3, space)}| " +
                        $"{book.Title.PadRight(45, space)}{book.Author.PadRight(45, space)} " +
                        $"{book.Status.ToString().PadRight(45, space)} " +
                        $"{book.DueDate.ToString().PadRight(45, space)}");
                        num++;
                    }
                }
            }
            else if(bookOrAuthor == false)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Author.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"{num.ToString().PadRight(3, space)}| " +
                        $"{book.Title.PadRight(45, space)}{book.Author.PadRight(45, space)} " +
                        $"{book.Status.ToString().PadRight(45, space)} " +
                        $"{book.DueDate.ToString().PadRight(45, space)}"); 
                        num++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error incorrect selection");
            }


        }
        public static List<Book> CheckOutReturn(List<Book> listOfBooks,bool checkoutorreturn)
        {
            if(checkoutorreturn == true)
            {
                Console.WriteLine("Please enter the title of the book you are returning:");
                var searchItem = Console.ReadLine();
                listOfBooks = CheckoutReturnVerification(listOfBooks, searchItem, true);
                return listOfBooks;
            }
            else if(checkoutorreturn == false)
            {
                Console.WriteLine("Please Enter the title book you\'d like to check out");
                var searchItem = Console.ReadLine();
                listOfBooks = CheckoutReturnVerification(listOfBooks, searchItem, false);
                return listOfBooks;
            }
            else
            {
                Console.WriteLine("Error unable to check out or return book");
                return listOfBooks;
            }

        }
        public static List<Book> CheckoutReturnVerification(List<Book> listOfBooks, string searchItem, bool checkoutorreturn)
        {
            if(checkoutorreturn == true)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Title.Equals(searchItem, StringComparison.OrdinalIgnoreCase) && book.Status == Status.CheckedOut)
                    {
                        Console.WriteLine($"Great! Thank you! Returning {book.Title} by {book.Author} \n");
                        book.Status = Status.OnShelf;
                        book.DueDate = DateTime.MinValue;
                    }
                    else if (book.Title.Equals(searchItem, StringComparison.OrdinalIgnoreCase) && book.Status == Status.OnShelf)
                    {
                        Console.WriteLine($"Sorry! {book.Title} by {book.Author} is already on the shelf \n");
                    }
                }
                return listOfBooks;
            }
            else if(checkoutorreturn == false)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Title.Equals(searchItem, StringComparison.OrdinalIgnoreCase) && book.Status == Status.OnShelf)
                    {
                        book.Status = Status.CheckedOut;
                        book.DueDate = DateTime.Now.AddDays(14);
                        Console.WriteLine($"Great! Checking out {book.Title} by {book.Author} \n" +
                            $"It will be due: " + book.DueDate);

                    }
                    else if (book.Title.Equals(searchItem, StringComparison.OrdinalIgnoreCase) && book.Status == Status.CheckedOut)
                    {
                        Console.WriteLine($"Sorry! {book.Title} by {book.Author} is already checked out \n");
                    }
                }
                return listOfBooks;
            }
            else
            {
                Console.WriteLine("Error unable to check out or return book");
                return listOfBooks;
            }
        }
        public static Book CreateBook()
        {
            Console.WriteLine("Please Enter a Title:");
            string userTitle = Console.ReadLine();
            Console.WriteLine("Please Enter an Author:");
            string userAuthor = Console.ReadLine();
            Console.WriteLine($"{userTitle} by {userAuthor}, Got It!");
            return new Book(userTitle, userAuthor);
        }
        public static List<Book> BuildLibraryFromText()
        {
            var myLibary = new List<Book>();
            string[] myData = File.ReadAllLines(Global.libaryPath);
            for (int i = 0; i < myData.Length; i = i + 4)
            {
                var bookTitle = myData[i];
                var bookAuthor = myData[i + 1];
                Status bookStatus = SetStatus(myData[i + 2]);
                DateTime bookDueDate = SetDueDate(myData[i + 3]);
                myLibary.Add(new Book(bookTitle, bookAuthor, bookStatus, bookDueDate));
            }

            return myLibary;
        }
        public static Status SetStatus(string status)
        {
            if (status.Equals("OnShelf", StringComparison.OrdinalIgnoreCase))
            {
                return Status.OnShelf;
            }
            else
            {
                return Status.CheckedOut;
            }
        }
        public static DateTime SetDueDate(string dueDate)
        {
            if (DateTime.TryParse(dueDate, out DateTime result))
            {
                return result;
            }
            else
            {
                return DateTime.Today.AddDays(2);
            }
        }
        public static void UpdateLibary(List<Book> listOfBooks)
        {
            var sw = new StreamWriter(Global.libaryPath, false);
            using (sw)
            {
                foreach (Book book in listOfBooks)
                {
                    sw.WriteLine(book.Title);
                    sw.WriteLine(book.Author);
                    sw.WriteLine(book.Status.ToString());
                    sw.WriteLine(book.DueDate.ToString());
                }
            }
            sw.Close();
            var realData = File.ReadAllText(Global.libaryPath).Trim();
            var dw = new StreamWriter(Global.libaryPath);
            using (dw)
            {
                dw.Write(realData);
            }
            dw.Close();

        }
        public static void PrintGUIFormat()
        {
            Console.WriteLine("\n\n" +
            "No.Title                                          Author Name                                   Status                                        Due Date \n" +
             "===================================================================================================================================================================== \n");
        }
    }
}