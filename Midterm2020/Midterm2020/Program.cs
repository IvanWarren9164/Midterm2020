using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Midterm2020
{
    class Program
    {
        static void Main(string[] args)
        {
    

            var testListOfBooks = new List<Book>();
            testListOfBooks.Add(new Book("Harry Potter and the chamber of secrets", "JK Rowling"));
            testListOfBooks.Add(new Book("Sisterhood of the traveling pants", "Joe Schmo"));
            testListOfBooks.Add(new Book("To Kill A Mockingbird", "Harper Lee"));
            testListOfBooks.Add(new Book("The Great Gatsby", "F Scott Fitzgerald"));
            testListOfBooks.Add(new Book("A Passage to India", "EM Foster"));
            testListOfBooks.Add(new Book("Invisible Man", "Ralph Ellison"));
            testListOfBooks.Add(new Book("Visible Man", "Eli Ralphson"));
            testListOfBooks.Add(new Book("Beloved", "Toni Morrison"));
            testListOfBooks.Add(new Book("Deloved", "Toni Morrison"));

            // Library.DisplayAllBooks(testListOfBooks);

           

            Library.CreateList(testListOfBooks);
            Book.ReadLibrary();

            do
            {
                Console.WriteLine("Select one of the option below : ");
                Console.WriteLine("1. Search for a book by author.");
                Console.WriteLine("2. Search for a book by title keyword.");
                Console.WriteLine("3. CheckOut Book ");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. Exit ");
                int select = Convert.ToInt32(Console.ReadLine());


                if (select == 1)
                {
                    Console.WriteLine("Please enter name of the auther : ");
                    string autherOfTheBook = Console.ReadLine();
                    Library.SearchAuther(autherOfTheBook);
                    Console.WriteLine("Please select the option below : ");
                    Console.WriteLine("1. Check Out Book : ");
                    Console.WriteLine("2. Return a book");
                    Console.WriteLine("3. Return to Main Menu : ");

                    int select1 = Convert.ToInt32(Console.ReadLine());
                    if (select1 == 1)
                    {
                        Library.CheckOutBook1(testListOfBooks, autherOfTheBook);
                        Library.CreateList(testListOfBooks);
                        Book.ReadLibrary();
                        break;
                    }
                    else if (select1 == 2)
                    {
                        Library.ReturnBook(testListOfBooks, autherOfTheBook);
                        Library.CreateList(testListOfBooks);
                        Book.ReadLibrary();
                        break;

                    }
                }
                else if (select == 2 )
                {
                    Console.WriteLine("Please enter title of the book : ");
                    string titleOfTheBook = Console.ReadLine();
                    Library.Searchtitle(titleOfTheBook);
                    Console.WriteLine("Please select the option below : ");
                    Console.WriteLine("1. Check Out Book : ");
                    Console.WriteLine("2. Return a book");
                    Console.WriteLine("3. Return to Main Menu : ");
                    int select1 = Convert.ToInt32(Console.ReadLine());

                    if (select1 == 1)
                    {
                        Library.CheckOutBook1(testListOfBooks, titleOfTheBook);
                        Library.CreateList(testListOfBooks);
                        Book.ReadLibrary();
                        break;
                    }
                    else if (select1 == 2)
                    {
                        Library.ReturnBook(testListOfBooks, titleOfTheBook);
                        Library.CreateList(testListOfBooks);
                        Book.ReadLibrary();
                        break;

                    }
                    
                }
                else if (select == 3)
                {
                    Console.WriteLine("Please enter name of the auther or title of the book");
                    string checkout = Console.ReadLine();
                    Library.CheckOutBook1(testListOfBooks, checkout);
                    Library.CreateList(testListOfBooks);
                    Book.ReadLibrary();
                    break;
                }
                else if (select == 4)
                {
                    Console.WriteLine("Please enter name of the auther or title of the book");
                    string checkout = Console.ReadLine();
                    Library.ReturnBook(testListOfBooks, checkout);
                    Library.CreateList(testListOfBooks);
                    Book.ReadLibrary();
                    break;
                }
                else
                {
                    Console.WriteLine("\nBye!");
                    break;
                }
                
               
                


            } while (true);
           

        }
    }

    public enum Status
    {
        OnShelf = 0,
        CheckedOut = 1
    }


    static class Global // Create a path variable.
    {
        public static string path = Path.Combine(Environment.CurrentDirectory, @"\Midterm2020\Midterm2020\MyText.txt");
       
    }

    public abstract class Library
    {
        /*
        public static void DisplayAllBooks(List<Book> listOfBooks)
        {
         
            foreach (Book book in listOfBooks)
            {
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                DynamicDueDate(book);
                Console.WriteLine("\n\n");
            } 
            


           
        }*/

      /*  public static void DynamicDueDate(Book book)
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
        }*/ 
       
        public static void SearchAuther(string name)
        {
            int num = 1;
            char space = ' ';
            string[] readText = File.ReadAllLines(Global.path);
            Console.WriteLine("\n\n");
            Console.WriteLine("No. Title 				          Author Name		                     	Status 		                                    Due Date");
            Console.WriteLine("=====================================================================================================================================================================");

            for (int i = 0; i < File.ReadAllLines(Global.path).Length; i++)
            {
                i++;
                if (readText[i].Equals(name, StringComparison.OrdinalIgnoreCase))
                { 
                     string line1 = "";
                          for (int j = i-1; j < i + 3; j++)
                          {
                                line1 = line1 + readText[j].PadRight(45, space) + " ";
                          }
                     Console.WriteLine($"{num}.  {line1} ");
                    num++;
                    
                }
                
                
                
            }
            Console.WriteLine("\n\n");
            
        }

        public static void Searchtitle(string name)
        {
            int num = 1;
            char space = ' ';
            string[] readText = File.ReadAllLines(Global.path);
            Console.WriteLine("\n\n");
            Console.WriteLine("No. Title 				          Author Name		                     	Status 		                                    Due Date");
            Console.WriteLine("=====================================================================================================================================================================");

            for (int i = 0; i < File.ReadAllLines(Global.path).Length; i++)
            {
                if (readText[i].Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    string line1 = "";
                    for (int j = i; j < i + 4; j++)
                    {
                        line1 = line1 + readText[j].PadRight(45, space) + " ";
                    }
                    Console.WriteLine($"{num}.  {line1} ");
                    num++;
                    
                }
                
            }
            Console.WriteLine("\n\n");
        }

        public static void ReturnBook(List<Book> listOfBooks, string bookToBeReturned)
        {
            foreach (Book book in listOfBooks)
            {
                if (book.Title.Equals(bookToBeReturned, StringComparison.OrdinalIgnoreCase))
                {
                    book.Status = Status.OnShelf;
                    book.DueDate = DateTime.MinValue;
                    Console.WriteLine("Return Done!");
                }
                if (book.Author.Equals(bookToBeReturned, StringComparison.OrdinalIgnoreCase))
                {
                    book.Status = Status.OnShelf;
                    book.DueDate = DateTime.MinValue;
                    Console.WriteLine("Return Done!");
                }
            }
            if (File.Exists(Global.path))
                File.Delete(Global.path);
        }

    
       /* public static void CheckOutBook(List<Book> listOfBooks, Book bookToBeCheckedOut)
        {
            foreach (Book book in listOfBooks)
            {
                if (book.Title.Equals(bookToBeCheckedOut.Title, StringComparison.OrdinalIgnoreCase))
                {
                    book.Status = Status.CheckedOut;
                    book.DueDate = DateTime.Now.AddDays(14);
                }

            }
        }*/
        public static void CheckOutBook1(List<Book> listOfBooks, string bookToBeCheckedOut)
        {
            
            foreach (Book book in listOfBooks)
            {
                if (book.Title.Equals(bookToBeCheckedOut, StringComparison.OrdinalIgnoreCase) )
                {
                        book.Status = Status.CheckedOut;
                        book.DueDate = DateTime.Now.AddDays(14);
                        Console.WriteLine("Checkout Done!");                     
                                   
                    
                }
                if (book.Author.Equals(bookToBeCheckedOut, StringComparison.OrdinalIgnoreCase))
                {
                    book.Status = Status.CheckedOut;
                    book.DueDate = DateTime.Now.AddDays(14);
                    Console.WriteLine("Checkout Done!");


                }
                /*
                else if (book.Title.Equals(bookToBeCheckedOut, StringComparison.OrdinalIgnoreCase) && book.Status.Equals("CheckedOut"))
                {
                    Console.WriteLine("Book is already checked out! Please select athoer book.");
                    break;
                }
                */
            }



            /*
             string[] readText = File.ReadAllLines(Global.path);


             for (int i = 0; i < File.ReadAllLines(Global.path).Length; i+=4)
             {
                 if (readText[i].Equals(bookToBeCheckedOut, StringComparison.OrdinalIgnoreCase))
                 {

                     for (int j = i + 2; j == i; j++)
                     {
                         readText[j] = "CheckedOut";
                     }
                     for (int j = i+3 ; j == i; j++)
                     {
                         readText[j] = DateTime.Now.AddDays(14).ToString();
                     }



                 }

             }*/
            if (File.Exists(Global.path))
                File.Delete(Global.path);


        }

        public static void CreateList(List<Book> testListOfBooks)
        {
            var listOfTitle = new List<string>();
            for (int i = 0; i < testListOfBooks.Count; i++)
            {
                listOfTitle.Add(testListOfBooks[i].Title);
                listOfTitle.Add(testListOfBooks[i].Author);
                listOfTitle.Add(testListOfBooks[i].Status.ToString());
                listOfTitle.Add(testListOfBooks[i].DueDate.ToString());

            }
            
            if (!File.Exists(Global.path))
                File.WriteAllLines(Global.path, listOfTitle);
          
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
        public string Title { get; set; }
        public string Author { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }


        public static void ReadLibrary()
        {
            int num = 1;
            char space = ' ';
            string[] readText = File.ReadAllLines(Global.path);
            Console.WriteLine("\n\n");
            Console.WriteLine("No. Title 				          Author Name		                     	Status 		                                    Due Date");
            Console.WriteLine("=====================================================================================================================================================================");

            for (int i = 0; i < File.ReadAllLines(Global.path).Length; i=i+4)
            {
                string line1 = "";
                for (int j = i; j < i+4 ; j++)
                {
                    line1 = line1 + readText[j].PadRight(45,space) + " ";
                }
                Console.WriteLine($"{num}.  {line1} ");
                num++;
            }
            Console.WriteLine("\n\n");


           
            // depends on how our streamwriter / reader works. We want to create new books based on this txt file
        }

        
    }
}