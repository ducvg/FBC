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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = fbc1;uid=sa;pwd=admin;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D10535EF859912");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C207CF67AF25");

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
                        .HasConstraintName("FK__BookCateg__Categ__49C3F6B7"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookCateg__BookI__48CFD27E"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId").HasName("PK__BookCate__9C7051A791B7F7F4");
                        j.ToTable("BookCategory");
                    });
        });

        modelBuilder.Entity<BookOrder>(entity =>
        {
            entity.HasKey(e => e.BookOrderId).HasName("PK__BookOrde__6D8639268EDBBFB8");

            entity.ToTable("BookOrder");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Recipient).HasMaxLength(255);
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookOrder__UserI__4CA06362");

            entity.HasMany(d => d.Books).WithMany(p => p.BookOrders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderDeta__BookI__5070F446"),
                    l => l.HasOne<BookOrder>().WithMany()
                        .HasForeignKey("BookOrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderDeta__BookO__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("BookOrderId", "BookId").HasName("PK__OrderDet__0E583506973B4802");
                        j.ToTable("OrderDetail");
                    });
        });

        modelBuilder.Entity<CartOrder>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__CartOrde__51BCD7B7ADC79FB6");

            entity.ToTable("CartOrder");

            entity.HasOne(d => d.User).WithMany(p => p.CartOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartOrder__UserI__534D60F1");

            entity.HasMany(d => d.Books).WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CartDetai__BookI__571DF1D5"),
                    l => l.HasOne<CartOrder>().WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CartDetai__CartI__5629CD9C"),
                    j =>
                    {
                        j.HasKey("CartId", "BookId").HasName("PK__CartDeta__3262DB97425522E0");
                        j.ToTable("CartDetail");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BE9957069");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<ExchangeRequest>(entity =>
        {
            entity.HasKey(e => e.ExchangeId).HasName("PK__Exchange__72E6008BDC1F6D85");

            entity.ToTable("ExchangeRequest");

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Credit).HasColumnType("decimal(30, 5)");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.ExchangeRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExchangeR__UserI__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C2F93F232");

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
            entity.HasKey(e => e.WalletId).HasName("PK__Wallet__84D4F90E35D638EA");

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
