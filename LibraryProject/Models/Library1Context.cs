using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Models;

public partial class Library1Context : DbContext
{
    public Library1Context()
    {
    }

    public Library1Context(DbContextOptions<Library1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public DbSet<Users> Users { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ICPU0076\\SQLEXPRESS;Initial Catalog=Library1;Persist Security Info=False;User ID=sa;Password=sql@123;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Bookid).HasName("PK__Book__3DE1DE3FA81116BA");

            entity.ToTable("Book");

            entity.Property(e => e.Author)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BookDescription)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BookName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
