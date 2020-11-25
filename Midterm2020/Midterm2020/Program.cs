using System;
using System.Collections.Generic;
using System.IO;

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
            testListOfBooks.Add(new Book("Visible Man", "Eli Ralphson", DateTime.Now));
            testListOfBooks.Add(new Book("Beloved", "Toni Morrison"));
            testListOfBooks.Add(new Book("Deloved", "Toni Morrison"));

            Library.DisplayAllBooks(testListOfBooks);

            string mySearchString = Console.ReadLine();

            SearchForBook(testListOfBooks, mySearchString);
        }

        private static void SearchForBook(List<Book> testListOfBooks, string mySearchString)
        {
            Console.WriteLine("Do you want to search by name or author?");

            foreach (Book currentBook in testListOfBooks)
            {
                if (mySearchString == "name")
                {
                    Console.WriteLine(currentBook.Title);
                }
                else if (mySearchString == "author")
                {
                    Console.WriteLine(currentBook.Title);

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
            public static void DisplayAllBooks(List<Book> listOfBooks)
            {
                foreach (Book book in listOfBooks)
                {
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    DynamicDueDate(book);
                    Console.WriteLine("\n\n");
                }
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
            public static void ReturnBook(List<Book> listOfBooks, Book bookToBeReturned)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Title.Equals(bookToBeReturned.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        book.Status = Status.OnShelf;
                        book.DueDate = DateTime.MinValue;
                    }
                }
            }
            public static void CheckOutBook(List<Book> listOfBooks, Book bookToBeCheckedOut)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Title.Equals(bookToBeCheckedOut.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        book.Status = Status.CheckedOut;
                        book.DueDate = DateTime.Now.AddDays(14);
                    }


                }
            }
            public static void CreateLibrary()
            {
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




        }


    }