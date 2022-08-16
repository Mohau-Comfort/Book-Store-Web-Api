using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Repositry
{
    public class BookRepositry : IBookRepositry
    {
        private readonly BookStoreContext _context;

        public BookRepositry(BookStoreContext context)
        {
            _context = context;
        }

        //Method to get all books from table (Database)
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).ToListAsync();

            return records;
        }


        //Method to get a book from table (Database) using ID
        public async Task<BookModel> GetBookByIdAsync(int bookdId)
        {
            var records = await _context.Books.Where(x => x.Id == bookdId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).FirstOrDefaultAsync();

            return records;
        }


        //Method to add a book in a table (Database) 
        public async Task<int> AddBookAsync(BookModel bookModel)
        {

            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Add(book);
            //Line saves the data to the database
            await _context.SaveChangesAsync();

            return book.Id;

        }

        //Method to update a book in the database
        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            /*
            var book = await _context.Books.FindAsync(bookId);
            if(book != null)
            {
                book.Title = bookModel.Title;
                book.Description = bookModel.Description;

                await _context.SaveChangesAsync();
            }*/ 

            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description

            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }


        //Update an item in one database cell
        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {

            var book = await _context.Books.FindAsync(bookId);
            if (book!=null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }

        }

        //Delete method
        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books() { Id = bookId};

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
          
        }

    }

}
