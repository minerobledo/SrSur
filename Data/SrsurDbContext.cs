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

    public virtual DbSet<Postsvisitedbyguest> Postsvisitedbyguests { get; set; }

    public virtual DbSet<Refreshtoken> Refreshtokens { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categorydescription)
                .HasMaxLength(500)
                .HasColumnName("categorydescription");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Lasteditdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("lasteditdate");
            entity.Property(e => e.Parentid).HasColumnName("parentid");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("categories_parentid_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasColumnName("author");
            entity.Property(e => e.Authorurl)
                .HasMaxLength(500)
                .HasColumnName("authorurl");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Datecreate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("datecreate");
            entity.Property(e => e.Device)
                .HasMaxLength(100)
                .HasColumnName("device");
            entity.Property(e => e.Emailauthor)
                .HasMaxLength(255)
                .HasColumnName("emailauthor");
            entity.Property(e => e.Parentid).HasColumnName("parentid");
            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("comments_parentid_fkey");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_postid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("comments_userid_fkey");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guest_pkey");

            entity.ToTable("guest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Datecreate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("datecreate");
            entity.Property(e => e.Device)
                .HasMaxLength(100)
                .HasColumnName("device");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("ipaddress");
            entity.Property(e => e.Referrer)
                .HasMaxLength(500)
                .HasColumnName("referrer");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Useragent)
                .HasMaxLength(500)
                .HasColumnName("useragent");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Authorid).HasColumnName("authorid");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Commentstatus)
                .HasMaxLength(50)
                .HasColumnName("commentstatus");
            entity.Property(e => e.Createdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Excerpt)
                .HasMaxLength(500)
                .HasColumnName("excerpt");
            entity.Property(e => e.Lasteditdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("lasteditdate");
            entity.Property(e => e.Poststatus)
                .HasMaxLength(50)
                .HasColumnName("poststatus");
            entity.Property(e => e.Publishdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("publishdate");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Authorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_authorid_fkey");

            entity.HasMany(d => d.Categories).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "Postincategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("Categoryid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("postincategory_categoryid_fkey"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("Postid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("postincategory_postid_fkey"),
                    j =>
                    {
                        j.HasKey("Postid", "Categoryid").HasName("postincategory_pkey");
                        j.ToTable("postincategory");
                        j.IndexerProperty<long>("Postid").HasColumnName("postid");
                        j.IndexerProperty<long>("Categoryid").HasColumnName("categoryid");
                    });

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "Postintag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("Tagid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("postintag_tagid_fkey"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("Postid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("postintag_postid_fkey"),
                    j =>
                    {
                        j.HasKey("Postid", "Tagid").HasName("postintag_pkey");
                        j.ToTable("postintag");
                        j.IndexerProperty<long>("Postid").HasColumnName("postid");
                        j.IndexerProperty<long>("Tagid").HasColumnName("tagid");
                    });
        });

        modelBuilder.Entity<Postsvisitedbyguest>(entity =>
        {
            entity.HasKey(e => new { e.Postid, e.Guestid }).HasName("postsvisitedbyguest_pkey");

            entity.ToTable("postsvisitedbyguest");

            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Guestid).HasColumnName("guestid");
            entity.Property(e => e.Datevisited)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("datevisited");
            entity.Property(e => e.Visitetime).HasColumnName("visitetime");

            entity.HasOne(d => d.Guest).WithMany(p => p.Postsvisitedbyguests)
                .HasForeignKey(d => d.Guestid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("postsvisitedbyguest_guestid_fkey");

            entity.HasOne(d => d.Post).WithMany(p => p.Postsvisitedbyguests)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("postsvisitedbyguest_postid_fkey");
        });

        modelBuilder.Entity<Refreshtoken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refreshtokens_pkey");

            entity.ToTable("refreshtokens");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Expirydate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("expirydate");
            entity.Property(e => e.Replacedbytoken)
                .HasMaxLength(200)
                .HasColumnName("replacedbytoken");
            entity.Property(e => e.Revoked)
                .HasDefaultValue(false)
                .HasColumnName("revoked");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .HasColumnName("token");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Refreshtokens)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refreshtokens_userid_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tags_pkey");

            entity.ToTable("tags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Lasteditdate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("lasteditdate");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.Tagdescription)
                .HasMaxLength(500)
                .HasColumnName("tagdescription");
            entity.Property(e => e.Tagname)
                .HasMaxLength(100)
                .HasColumnName("tagname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dateregister)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("dateregister");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .HasColumnName("nickname");
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
