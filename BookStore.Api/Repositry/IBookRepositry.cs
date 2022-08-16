using BookStore.Api.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore.Api.Repositry
{
    public interface IBookRepositry
    {

        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookModel bookModel);
        Task UpdateBookAsync(int bookId, BookModel bookModel);
        Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel);
        Task DeleteBookAsync(int bookId);

    }
}
