using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YaarxivBackend.Models
{
    public partial class ArticleContext : DbContext
    {
        public ArticleContext()
        {
        }

        public ArticleContext(DbContextOptions<ArticleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleRevision> ArticleRevision { get; set; }
        public virtual DbSet<PdfUpload> PdfUpload { get; set; }
        public virtual DbSet<ResetPasswordToken> ResetPasswordToken { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=yaarxiv_dev;Uid=root;Pwd=dbfordev");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("FK_9c7bd5faae7271b4f09dc64a165");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminSetPublicity)
                    .HasColumnName("adminSetPublicity")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'true'");

                entity.Property(e => e.CreateTime).HasColumnName("createTime");

                entity.Property(e => e.LastUpdateTime).HasColumnName("lastUpdateTime");

                entity.Property(e => e.LatestRevisionNumber).HasColumnName("latestRevisionNumber");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasColumnName("ownerId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerSetPublicity)
                    .HasColumnName("ownerSetPublicity")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'true'");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_9c7bd5faae7271b4f09dc64a165");
            });

            modelBuilder.Entity<ArticleRevision>(entity =>
            {
                entity.ToTable("article_revision");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("FK_39812c68ea9557fc09d647743ae");

                entity.HasIndex(e => e.PdfId)
                    .HasName("FK_7e607abc0262a18c67b2fba37b2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abstract)
                    .IsRequired()
                    .HasColumnName("abstract")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.Authors)
                    .IsRequired()
                    .HasColumnName("authors");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasColumnName("keywords");

                entity.Property(e => e.PdfId)
                    .HasColumnName("pdfId")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionNumber).HasColumnName("revisionNumber");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleRevision)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_39812c68ea9557fc09d647743ae");

                entity.HasOne(d => d.Pdf)
                    .WithMany(p => p.ArticleRevision)
                    .HasForeignKey(d => d.PdfId)
                    .HasConstraintName("FK_7e607abc0262a18c67b2fba37b2");
            });

            modelBuilder.Entity<PdfUpload>(entity =>
            {
                entity.ToTable("pdf_upload");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_d726da16923089065da0e46afb9");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("link")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PdfUpload)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_d726da16923089065da0e46afb9");
            });

            modelBuilder.Entity<ResetPasswordToken>(entity =>
            {
                entity.ToTable("reset_password_token");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("userEmail")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email)
                    .HasName("IDX_e12875dfb3b1d92d7d7c5377e2")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasColumnType("enum('user','admin')")
                    .HasDefaultValueSql("'user'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
