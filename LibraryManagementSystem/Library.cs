using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Library
    {
        private List<Book> _books = new List<Book>();
        private int maxBookId = 0;

        public void AddBook(string title, string author, string genre)
        {
            int bookId = ++maxBookId;
            Book newBook = new Book(bookId, title, author, genre);
            _books.Add(newBook);
        }

        public void CheckOutBook(int bookId)
        {
            Book book = FindBookById(bookId);
            if (book != null && !book.IsCheckedOut)
            {
                book.CheckOut();
            }
        }

        public void CheckInBook(int bookId)
        {
            Book book = FindBookById(bookId);
            if (book != null && book.IsCheckedOut)
            {
                book.CheckIn();
            }
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            return _books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> SearchBooksByAuthor(string author)
        {
            return _books.Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public Book FindBookById(int bookId)
        {
            return _books.FirstOrDefault(book => book.BookId == bookId);
        }
    }
}
