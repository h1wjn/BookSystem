using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksSystem.Data
{
    public class BooksContext:DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCard> BookCard {get; set;}
        public DbSet<BorrowBook> BorrowBook {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<BookCard>().ToTable("BookCard");
            modelBuilder.Entity<BorrowBook>().ToTable("BorrowBook");
           
        }
    }
}
