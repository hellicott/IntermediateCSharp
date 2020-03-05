using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public Book(string title, string author, double price)
        {
            this.Title = title;
            this.Author = author;
            this.Price = price;
        }
    }

    class Author
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }

        public Author(string name, int birthYear)
        {
            this.Name = name;
            this.BirthYear = birthYear;
        }
    }
}
