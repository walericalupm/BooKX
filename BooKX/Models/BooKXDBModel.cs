namespace BooKX.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BooKXDBModel : DbContext
    {
        public BooKXDBModel()
            : base("name=BooKXDBModel")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Shops)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.Id_AspNetUsers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Author)
                .IsFixedLength();

            modelBuilder.Entity<Book>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Book>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Book>()
                .Property(e => e.Price)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Shops)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.Id_Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .Property(e => e.Code)
                .IsFixedLength();
        }
    }
}
