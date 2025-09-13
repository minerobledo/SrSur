using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SrSur.Models.Entities;

namespace SrSur.Data;

public partial class SrsurDbContext : DbContext
{
    public SrsurDbContext()
    {
    }

    public SrsurDbContext(DbContextOptions<SrsurDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostsVisitedByGuest> PostsVisitedByGuests { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC078D05CD97");

            entity.Property(e => e.CategoryDescription).HasMaxLength(500);
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Categorie__Paren__4E1E9780");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC07D1C37D0A");

            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AuthorUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Device)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailAuthor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Comments__Parent__4B422AD5");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__PostId__4A4E069C");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comments__UserId__4959E263");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Guest__3214EC07D7ECEA09");

            entity.ToTable("Guest");

            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Device)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.Referrer).HasMaxLength(500);
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent).HasMaxLength(500);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC07C80D2ADA");

            entity.Property(e => e.CommentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Excerpt).HasMaxLength(500);
            entity.Property(e => e.PostStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__AuthorId__467D75B8");

            entity.HasMany(d => d.Categories).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostInCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PostInCat__Categ__57A801BA"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PostInCat__PostI__56B3DD81"),
                    j =>
                    {
                        j.HasKey("PostId", "CategoryId").HasName("PK__PostInCa__0B82F3B823E4C6A3");
                        j.ToTable("PostInCategory");
                    });

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostInTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PostInTag__TagId__5B78929E"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PostInTag__PostI__5A846E65"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("PK__PostInTa__7C45AF82AE840117");
                        j.ToTable("PostInTag");
                    });
        });

        modelBuilder.Entity<PostsVisitedByGuest>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.GuestId }).HasName("PK__PostsVis__9AD643D921BEAA85");

            entity.ToTable("PostsVisitedByGuest");

            entity.HasOne(d => d.Guest).WithMany(p => p.PostsVisitedByGuests)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostsVisi__Guest__53D770D6");

            entity.HasOne(d => d.Post).WithMany(p => p.PostsVisitedByGuests)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostsVisi__PostI__52E34C9D");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC076F33866F");

            entity.Property(e => e.ReplacedByToken).HasMaxLength(200);
            entity.Property(e => e.Token).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__UserI__43A1090D");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tags__3214EC075CE2C3C7");

            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TagDescription).HasMaxLength(500);
            entity.Property(e => e.TagName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E2F7BE54");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534142EFEBD").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
