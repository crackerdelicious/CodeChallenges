using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book
    {
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Genre { get; private set; }
        public bool IsCheckedOut { get; private set; }

        public Book(int bookId, string title, string author, string genre)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            Genre = genre;
            IsCheckedOut = false;
        }

        public void CheckOut()
        {
            IsCheckedOut = true;
        }

        public void CheckIn()
        {
            IsCheckedOut = false;
        }

        public override string ToString()
        {
            string status = IsCheckedOut ? "[Checked Out]" : "[Available]";
            return $"{BookId}. {status} {Title} by {Author} ({Genre})";
        }
    }
}
