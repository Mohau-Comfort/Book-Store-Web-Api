using BookStore.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser> 
    {

        //setting up the constructor
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        //Method to create another table in the database
        public DbSet<Books> Books { get; set; }


    }
}
