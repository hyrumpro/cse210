using System;
using System.Collections.Generic;
using System.IO;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
    public bool Availability { get; set; }

    public Book(string title, string author, string isbn, string genre, bool availability)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Genre = genre;
        Availability = availability;
    }

    public void UpdateAvailability(bool newStatus)
    {
        Availability = newStatus;
    }
}

class Library
{
    private const string FilePath = "books.csv"; 

    public List<Book> Books { get; set; }

    public Library()
    {
        Books = LoadBooks(); 
    }

    
    private List<Book> LoadBooks()
    {
        List<Book> books = new List<Book>();

        if (File.Exists(FilePath))
        {
            using (var reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    bool.TryParse(values[4], out bool availability);

                    books.Add(new Book(values[0], values[1], values[2], values[3], availability));
                }
            }
        }

        return books;
    }

    public void DisplayAvailableBooks()
    {
        Console.WriteLine("Available Books:");
        foreach (var book in Books)
        {
            if (book.Availability)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}");
            }
        }
    }





    private void SaveBooks()
    {
        using (var writer = new StreamWriter(FilePath))
        {
            foreach (var book in Books)
            {
                writer.WriteLine($"{book.Title},{book.Author},{book.ISBN},{book.Genre},{book.Availability}");
            }
        }
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        SaveBooks(); 
    }


    public void RemoveBook(string title)
    {
        var bookToRemove = Books.Find(b => b.Title.ToLower() == title.ToLower());

        if (bookToRemove != null)
        {
            Books.Remove(bookToRemove);
            Console.WriteLine($"Book '{bookToRemove.Title}' removed successfully!");

            SaveBooks(); 
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void CheckoutBook(string title)
    {
        var book = Books.Find(b => b.Title.ToLower() == title.ToLower() && b.Availability);

        if (book != null)
        {
            book.UpdateAvailability(false);
            Console.WriteLine($"Book '{book.Title}' checked out successfully!");

            SaveBooks(); 
        }
        else
        {
            Console.WriteLine("Book not found or unavailable.");
        }
    }

    public void ReturnBook(string title)
    {
        var book = Books.Find(b => b.Title.ToLower() == title.ToLower() && !b.Availability);

        if (book != null)
        {
            book.UpdateAvailability(true);
            Console.WriteLine($"Book '{book.Title}' returned successfully!");

            SaveBooks(); 
        }
        else
        {
            Console.WriteLine("Book not found or already returned.");
        }
    }
}




class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        Console.WriteLine("Welcome to the Library Management System!");

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Display Available Books");
            Console.WriteLine("2. Checkout a Book");
            Console.WriteLine("3. Return a Book");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        library.DisplayAvailableBooks();
                        break;
                    case 2:
                        Console.Write("Enter the title of the book you want to checkout: ");
                        string checkoutTitle = Console.ReadLine();
                        library.CheckoutBook(checkoutTitle);
                        break;
                    case 3:
                        Console.Write("Enter the title of the book you want to return: ");
                        string returnTitle = Console.ReadLine();
                        library.ReturnBook(returnTitle);
                        break;
                    case 4:
                        Console.WriteLine("Exiting... Thank you!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}

