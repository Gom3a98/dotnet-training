using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace OnlineStore.Models;

public partial class BookstoresdbContext : DbContext
{
    public BookstoresdbContext()
    {
    }

    public BookstoresdbContext(DbContextOptions<BookstoresdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookauthor> Bookauthors { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Refreshtoken> Refreshtokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<User> Users { get; set; }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.Address)
                .HasMaxLength(40)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasColumnName("state");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("book");

            entity.HasIndex(e => e.PubId, "pub_id_fk");

            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.Advance)
                .HasPrecision(12)
                .HasColumnName("advance");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .HasColumnName("notes");
            entity.Property(e => e.Price)
                .HasPrecision(12)
                .HasColumnName("price");
            entity.Property(e => e.PubId)
                .HasColumnType("int(11)")
                .HasColumnName("pub_id");
            entity.Property(e => e.PublishedDate)
                .HasColumnType("datetime")
                .HasColumnName("published_date");
            entity.Property(e => e.Royalty)
                .HasColumnType("int(11)")
                .HasColumnName("royalty");
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(12)
                .HasColumnName("type");
            entity.Property(e => e.YtdSales)
                .HasColumnType("int(11)")
                .HasColumnName("ytd_sales");

            entity.HasOne(d => d.Pub).WithMany(p => p.Books)
                .HasForeignKey(d => d.PubId)
                .HasConstraintName("pub_id_fk");
        });
                    

        modelBuilder.Entity<Bookauthor>(entity =>
        {
            entity.HasKey(e => new { e.AuthorId, e.BookId }).HasName("PRIMARY");

            entity.ToTable("bookauthor");

            entity.HasIndex(e => e.BookId, "book_id_fk");

            entity.Property(e => e.AuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.AuthorOrder)
                .HasColumnType("tinyint(4)")
                .HasColumnName("author_order");
            entity.Property(e => e.RoyalityPercentage)
                .HasColumnType("int(11)")
                .HasColumnName("royality_percentage");

            entity.HasOne(d => d.Author).WithMany(p => p.Bookauthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("author_id_fk");

            entity.HasOne(d => d.Book).WithMany(p => p.Bookauthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("book_id_fk");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PRIMARY");

            entity.ToTable("job");

            entity.Property(e => e.JobId)
                .HasColumnType("int(11)")
                .HasColumnName("job_id");
            entity.Property(e => e.JobDesc)
                .HasMaxLength(50)
                .HasColumnName("job_desc");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("PRIMARY");

            entity.ToTable("publisher");

            entity.Property(e => e.PubId)
                .HasColumnType("int(11)")
                .HasColumnName("pub_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .HasColumnName("country");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(40)
                .HasColumnName("publisher_name");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasColumnName("state");
        });

        modelBuilder.Entity<Refreshtoken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity.ToTable("refreshtoken");

            entity.HasIndex(e => e.UserId, "user_id_fk");

            entity.Property(e => e.TokenId)
                .HasColumnType("int(11)")
                .HasColumnName("token_id");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .HasColumnName("token");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Refreshtokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_id_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleDesc)
                .HasMaxLength(50)
                .HasColumnName("role_desc");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PRIMARY");

            entity.ToTable("sale");

            entity.HasIndex(e => e.BookId, "book_id_fk2");

            entity.HasIndex(e => e.StoreId, "store_id_fk1");

            entity.Property(e => e.SaleId)
                .HasColumnType("int(11)")
                .HasColumnName("sale_id");
            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderNum)
                .HasMaxLength(20)
                .HasColumnName("order_num");
            entity.Property(e => e.PayTerms)
                .HasMaxLength(12)
                .HasColumnName("pay_terms");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.StoreId)
                .HasMaxLength(4)
                .HasColumnName("store_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("book_id_fk2");

            entity.HasOne(d => d.Store).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("store_id_fk1");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PRIMARY");

            entity.ToTable("store");

            entity.Property(e => e.StoreId)
                .HasMaxLength(4)
                .HasColumnName("store_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasColumnName("state");
            entity.Property(e => e.StoreAddress)
                .HasMaxLength(40)
                .HasColumnName("store_address");
            entity.Property(e => e.StoreName)
                .HasMaxLength(40)
                .HasColumnName("store_name");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.PubId, "pub_id_fk1");

            entity.HasIndex(e => e.RoleId, "role_id_fk");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("datetime")
                .HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(1)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PubId)
                .HasColumnType("int(11)")
                .HasColumnName("pub_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .HasColumnName("source");

            entity.HasOne(d => d.Pub).WithMany(p => p.Users)
                .HasForeignKey(d => d.PubId)
                .HasConstraintName("pub_id_fk1");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("role_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
