using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FBC.Models;

public partial class Fbc1Context : IdentityDbContext<User>
{
    public Fbc1Context()
    {
    }

    public Fbc1Context(DbContextOptions options)
        : base(options)
    {
    }

    //public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookOrder> BookOrders { get; set; }

    public virtual DbSet<CartOrder> CartOrders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ExchangeRequest> ExchangeRequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }
    public virtual DbSet<WalletOrder> WalletOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);

       // var admin = new IdentityRole("admin");
       // admin.NormalizedName = "admin";
       // var client = new IdentityRole("client");
       // client.NormalizedName = "client";

       //modelBuilder.Entity<IdentityRole>().HasData(admin, client);

        //modelBuilder.Entity<Account>(entity =>
        //{
        //    entity.HasKey(e => e.Email).HasName("PK__Account__A9D1053531C8A413");

        //    entity.ToTable("Account");

        //    entity.Property(e => e.Email).HasMaxLength(255);
        //    entity.Property(e => e.Password).HasMaxLength(255);
        //});

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C207C29A701D");

            entity.ToTable("Book");

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Credit)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(30, 5)");
            entity.Property(e => e.Height)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 5)");
            entity.Property(e => e.Length)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 5)");
            entity.Property(e => e.Publisher).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Weight)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 5)");
            entity.Property(e => e.Width)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 5)");

            entity.HasMany(d => d.Categories).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookCateg__Categ__36B12243"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookCateg__BookI__35BCFE0A"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId").HasName("PK__BookCate__9C7051A779BBAB76");
                        j.ToTable("BookCategory");
                    });
        });

        modelBuilder.Entity<BookOrder>(entity =>
        {
            entity.HasKey(e => e.BookOrderId).HasName("PK__BookOrde__6D863926FD0C4C09");

            entity.ToTable("BookOrder");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Recipient).HasMaxLength(255);
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookOrder__UserI__398D8EEE");

            entity.HasMany(d => d.Books).WithMany(p => p.BookOrders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderDeta__BookI__3D5E1FD2"),
                    l => l.HasOne<BookOrder>().WithMany()
                        .HasForeignKey("BookOrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderDeta__BookO__3C69FB99"),
                    j =>
                    {
                        j.HasKey("BookOrderId", "BookId").HasName("PK__OrderDet__0E58350653C60172");
                        j.ToTable("OrderDetail");
                    });
        });

        modelBuilder.Entity<CartOrder>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__CartOrde__51BCD7B7F1AB39F1");

            entity.ToTable("CartOrder");

            entity.HasOne(d => d.User).WithMany(p => p.CartOrders)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartOrder__UserI__403A8C7D");

            entity.HasMany(d => d.Books).WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CartDetai__BookI__440B1D61"),
                    l => l.HasOne<CartOrder>().WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CartDetai__CartI__4316F928"),
                    j =>
                    {
                        j.HasKey("CartId", "BookId").HasName("PK__CartDeta__3262DB97E0FE7F12");
                        j.ToTable("CartDetail");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B456777E8");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<ExchangeRequest>(entity =>
        {
            entity.HasKey(e => e.ExchangeId).HasName("PK__Exchange__72E6008B0E8B6472");

            entity.ToTable("ExchangeRequest");

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Credit).HasColumnType("decimal(30, 5)");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.ExchangeRequests)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeR__UserI__46E78A0C");
        });



        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Wallet__84D4F90EC43E767F");

            entity.ToTable("Wallet");

            entity.Property(e => e.Bank).HasMaxLength(255);
            entity.Property(e => e.BankNumber).HasMaxLength(255);
            entity.Property(e => e.Credit)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(30, 5)");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__Wallet__UserId__2A4B4B5E");
        });

        modelBuilder.Entity<WalletOrder>(entity =>
        {
            entity.HasKey(e => e.WalletOrderId).HasName("PK__WalletOrder__8VSDVSDVA");

            entity.ToTable("WalletOrder");

            entity.Property(e => e.BankAcountName).HasMaxLength(255);
            entity.Property(e => e.PaymentCode).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Credit)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(30, 5)");

            entity.HasOne(d => d.User).WithMany(p => p.WalletOrder)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wallet__UserId__84D4F90EC43E767F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
