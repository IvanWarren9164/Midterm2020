using System;

namespace Midterm2020
{
    public class Book
    {
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
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
}