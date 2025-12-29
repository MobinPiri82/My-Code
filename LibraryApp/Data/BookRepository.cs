using LibraryApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Data
{
    public class BookRepository
    {
        // ➕ ADD (بدون Id)
        public void Add(Book book)
        {
            using var db = new LibraryContext();
            db.Books.Add(book);
            db.SaveChanges();
        }

        // ✏️ UPDATE (با Id)
        public void Update(Book book)
        {
            using var db = new LibraryContext();
            var oldBook = db.Books.Find(book.Id);
            if (oldBook != null)
            {
                oldBook.Title = book.Title;
                oldBook.Author = book.Author;
                oldBook.PublishYear = book.PublishYear;
                db.SaveChanges();
            }
        }

        // 🗑️ DELETE (با Id)
        public void Delete(int id)
        {
            using var db = new LibraryContext();
            var book = db.Books.Find(id);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }

        // 📋 GET ALL
        public List<Book> GetAll()
        {
            using var db = new LibraryContext();
            return db.Books.ToList();
        }

        // 🔍 SEARCH
        public List<Book> SearchByTitle(string title)
        {
            using var db = new LibraryContext();
            return db.Books
                     .Where(b => b.Title.Contains(title))
                     .ToList();
        }
    }
}
