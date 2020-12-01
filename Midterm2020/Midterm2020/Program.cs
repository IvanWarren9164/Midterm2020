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
                Library.CheckOutBook(listOfBooks);
                return listOfBooks;
            }
            else if (selection == 4)
            {
                Library.ReturnBook(listOfBooks);
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

    public enum Status
    {
        OnShelf = 0,
        CheckedOut = 1
    }

    public abstract class Library
    {

        public static void SearchForBook(List<Book> listOfBooks)
        {

            int num = 1;
            char space = ' ';
            int count = 1;
            string[] readText = File.ReadAllLines(Global.libaryPath);


            Console.WriteLine("Please enter a title or author to search by:");
            var searchCriteria = Console.ReadLine().Trim();
            Console.WriteLine("\n\n");
            Console.WriteLine("No.| Title 				          Author Name		                     	Status 		                                    Due Date");
            Console.WriteLine("=====================================================================================================================================================================");

            foreach (Book book in listOfBooks)
            {
                if (book.Title.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase) || book.Author.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase))
                {

                    Console.WriteLine($"{num.ToString().PadRight(3, space)}| {book.Title.PadRight(45, space)}{book.Author.PadRight(45, space)}{book.Status.ToString().PadRight(45, space)}{book.DueDate.ToString().PadRight(45, space)}");
                    count++;
                    break;
                }
                if (!book.Title.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase) || !book.Author.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase))
                {

                    count++;
                }
                if (count.Equals(12))
                {
                    Console.WriteLine("Wrong Input! Please input right Keywords");
                }
            }
            Console.WriteLine("\n\n");
        }


        public static void DynamicDueDate(Book book)
        {
            if (book.Status == Status.CheckedOut)
            {
                Console.WriteLine("Status: Checked Out");
                Console.WriteLine($"Due Date: {book.DueDate}");
            }
            else
            {
                Console.WriteLine("Status: Available");
                Console.WriteLine("No Due Date");
            }
        }

        public static List<Book> ReturnBook(List<Book> listOfBooks)
        {
            Console.WriteLine("Please enter the title of the book you are returning:");
            var returnedTitle = Console.ReadLine();
            foreach (Book book in listOfBooks)
            {
                if (book.Title.Equals(returnedTitle, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Great! Thank you! Returning {book.Title} by {book.Author}");
                    book.Status = Status.OnShelf;
                    book.DueDate = DateTime.MinValue;
                }
            }
            return listOfBooks;
        }
        public static List<Book> CheckOutBook(List<Book> listOfBooks)
        {
            Console.WriteLine("Please Enter the title book you\'d like to check out");
            var searchItem = Console.ReadLine();
            foreach (Book book in listOfBooks)
            {
                if (book.Title.Equals(searchItem, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Great! Checking out {book.Title} by {book.Author}");
                    book.Status = Status.CheckedOut;
                    book.DueDate = DateTime.Now.AddDays(14);
                }
            }
            return listOfBooks;
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


        public static void ReadLibrary()
        {
            int num = 1;
            char space = ' ';
            string[] readText = File.ReadAllLines(Global.libaryPath);
            Console.WriteLine("\n\n");
            Console.WriteLine("No. Title 				          Author Name		                     	Status 		                                    Due Date");
            Console.WriteLine("=====================================================================================================================================================================");

            for (int i = 0; i < File.ReadAllLines(Global.libaryPath).Length; i = i + 4)
            {
                string line1 = "";
                for (int j = i; j < i + 4; j++)
                {
                    line1 = line1 + readText[j].PadRight(45, space) + " ";
                }
                Console.WriteLine($"{num.ToString().PadRight(3, space)}|{line1} ");
                num++;
            }
            Console.WriteLine("\n\n");



            // depends on how our streamwriter / reader works. We want to create new books based on this txt file
        }

    }
    public class Book
    {
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            Status = Status.OnShelf;
            DueDate = DateTime.MinValue;
        }
        public Book(string title, string author, DateTime dueDate)
        {
            Title = title;
            Author = author;
            Status = Status.CheckedOut;
            DueDate = dueDate;
        }
        public Book(string title, string author, Status status, DateTime dueDate)
        {
            Title = title;
            Author = author;
            Status = status;
            DueDate = dueDate;
        }
        public string Title { get; set; }
        public string Author { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }



    }
    public static class Global // Create a path variable.
    {
        public static string path = Path.Combine(Environment.CurrentDirectory, @"\Midterm2020\Midterm2020\Midterm2020\Midterm2020\library.txt");
        public static string libaryPath = "../../../libary.txt";

    }
}