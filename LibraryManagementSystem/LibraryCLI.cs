using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class LibraryCLI
    {
        private Library library = new Library();

        public void Run() 
        {
            Console.WriteLine("Welcome to the Library Management System!");

            int choice;

            do
            {
                PrintMenus();
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddBook();
                            break;
                        case 2:
                            ViewAllBooks();
                            break;
                        case 3:
                            CheckOutBook();
                            break;
                        case 4:
                            CheckInBook();
                            break;
                        case 5:
                            SearchByTitle();
                            break;
                        case 6:
                            SearchByAuthor();
                            break;
                        case 7:
                            Console.WriteLine("Goodbye!");

                            break;
                        default:
                            Console.WriteLine("Please choose a valid option (1-7).");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose a valid option (1-7).");
                }
            } while (choice != 7);

        }

        public void PrintMenus()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. View all books");
            Console.WriteLine("3. Check out a book");
            Console.WriteLine("4. Check in a book");
            Console.WriteLine("5. Search for books by title");
            Console.WriteLine("6. Search for books by author");
            Console.WriteLine("7. Exit\n");

            Console.Write("Enter your choice: ");
        }

        public void AddBook()
        {
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            
            Console.Write("Enter book author: ");
            string author = Console.ReadLine();

            Console.Write("Enter book genre: ");
            string genre = Console.ReadLine();

            library.AddBook(title, author, genre);
            Console.WriteLine("Book added successfully!");

        }

        public void ViewAllBooks()
        {
            List<Book> books = library.GetAllBooks();
            if (books.Any() )
            {
                Console.WriteLine("List of Books: ");
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("No books in the library.");
            }
        }

        public void CheckOutBook()
        {
            Console.Write("Enter the ID of the book to check out: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                library.CheckOutBook(bookId);
                Console.WriteLine("Book checked out successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid book ID.");
            }
        }

        public void CheckInBook()
        {
            Console.Write("Enter the ID of the book to check in: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                library.CheckInBook(bookId);
                Console.WriteLine("Book checked in successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid book ID.");
            }
        }

        public void SearchByTitle()
        {
            Console.Write("Enter the title to search: ");
            string title = Console.ReadLine();

            List<Book> books = library.SearchBooksByTitle(title);
            if (books.Any() )
            {
                Console.WriteLine("Search Results:");
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("No books found with the given title");
            }
        }

        public void SearchByAuthor()
        {
            Console.Write("Enter the title to author: ");
            string author = Console.ReadLine();

            List<Book> books = library.SearchBooksByAuthor(author);
            if (books.Any())
            {
                Console.WriteLine("Search Results:");
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("No books found with the given title");
            }
        }
    }
}
