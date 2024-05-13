using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FBC.Models;

public partial class Fbc1Context : DbContext
{
    public Fbc1Context()
    {
    }

    public Fbc1Context(DbContextOptions<Fbc1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookOrder> BookOrders { get; set; }

    public virtual DbSet<CartOrder> CartOrders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ExchangeRequest> ExchangeRequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

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
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D105355482103F");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C207B0226EA4");

            entity.ToTable("Book");

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Credit)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(30, 5)");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__CategoryId__4316F928");
        });

        modelBuilder.Entity<BookOrder>(entity =>
        {
            entity.HasKey(e => e.BookOrderId).HasName("PK__BookOrde__6D863926F5C8A898");

            entity.ToTable("BookOrder");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Recipient).HasMaxLength(255);
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookOrder__UserI__45F365D3");

            entity.HasMany(d => d.Books).WithMany(p => p.BookOrders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderDeta__BookI__49C3F6B7"),
                    l => l.HasOne<BookOrder>().WithMany()
                        .HasForeignKey("BookOrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderDeta__BookO__48CFD27E"),
                    j =>
                    {
                        j.HasKey("BookOrderId", "BookId").HasName("PK__OrderDet__0E583506CF1F0642");
                        j.ToTable("OrderDetail");
                    });
        });

        modelBuilder.Entity<CartOrder>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__CartOrde__51BCD7B76EB201F2");

            entity.ToTable("CartOrder");

            entity.HasOne(d => d.User).WithMany(p => p.CartOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartOrder__UserI__4CA06362");

            entity.HasMany(d => d.Books).WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CartDetai__BookI__5070F446"),
                    l => l.HasOne<CartOrder>().WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CartDetai__CartI__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("CartId", "BookId").HasName("PK__CartDeta__3262DB97EAE3AD8A");
                        j.ToTable("CartDetail");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B2662F704");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<ExchangeRequest>(entity =>
        {
            entity.HasKey(e => e.ExchangeId).HasName("PK__Exchange__72E6008BC4E2EA3E");

            entity.ToTable("ExchangeRequest");

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Credit).HasColumnType("decimal(30, 5)");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.ExchangeRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeR__UserI__534D60F1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C6E0CFEB7");

            entity.ToTable("User");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__User__Email__398D8EEE");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Wallet__84D4F90EF2BB8B67");

            entity.ToTable("Wallet");

            entity.Property(e => e.Bank).HasMaxLength(255);
            entity.Property(e => e.BankNumber).HasMaxLength(255);
            entity.Property(e => e.Credit)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(30, 5)");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Wallet__UserId__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
