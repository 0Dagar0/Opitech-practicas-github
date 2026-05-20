using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<BookCopy> BookCopies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)


        {

        }
    }
}
