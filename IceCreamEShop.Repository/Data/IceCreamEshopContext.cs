using System;
using System.Collections.Generic;
using IceCreamEShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IceCreamEShop.Repository.Data;

public partial class IceCreamEShopContext : DbContext
{
    public IceCreamEShopContext()
    {
    }

    public IceCreamEShopContext(DbContextOptions<IceCreamEShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.ShoppingCartId, e.ProductId }).HasName("CartItemId");

            entity.ToTable("CartItem");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CI_Product");

            entity.HasOne(d => d.ShoppingCart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ShoppingCartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CI_ShoppingCart");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.OrderInfoId).HasName("PK__OrderInf__36170B25728DC99B");

            entity.ToTable("OrderInfo");

            entity.Property(e => e.PaymentProvider).HasMaxLength(20);

            entity.HasOne(d => d.UserAccount).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OI_UserAccount");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderInfoId, e.ProductId }).HasName("OrderItemId");

            entity.ToTable("OrderItem");

            entity.HasOne(d => d.OrderInfo).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OI_OrderInfo");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OI_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD7E0ABE75");

            entity.ToTable("Product");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ProductDescription).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.ProductPicUrl).HasMaxLength(255);
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.ShoppingCartId).HasName("PK__Shopping__7A789AE412D13276");

            entity.ToTable("ShoppingCart");

            entity.HasIndex(e => e.UserAccountId, "UQ__Shopping__DA6C709BF6418F1E").IsUnique();

            entity.HasOne(d => d.UserAccount).WithOne(p => p.ShoppingCart)
                .HasForeignKey<ShoppingCart>(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SC_UserAccount");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__UserAcco__DA6C709AB47A8ABA");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Email, "uq_useraccount_email").IsUnique();

            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EnPassword).HasMaxLength(255);
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Updatedat).HasColumnName("updatedat");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasMany(d => d.Products).WithMany(p => p.UserAccounts)
                .UsingEntity<Dictionary<string, object>>(
                    "WishList",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("WL_Product"),
                    l => l.HasOne<UserAccount>().WithMany()
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("WL_UserAccount"),
                    j =>
                    {
                        j.HasKey("UserAccountId", "ProductId").HasName("WishListId");
                        j.ToTable("WishList");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
