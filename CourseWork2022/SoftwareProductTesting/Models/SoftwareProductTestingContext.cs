using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SoftwareProductTesting.Models
{
    public class SoftwareProductTestingContext : DbContext
    {
        public SoftwareProductTestingContext()
        {
        }

        public SoftwareProductTestingContext(DbContextOptions<SoftwareProductTestingContext> options)
            : base(options)
        {
        }

        public  DbSet<Addressee> Addressees { get; set; }
        public  DbSet<Comment> Comments { get; set; }
        public  DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("SoftwareProductTesting");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Addressee>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__Addresse__AA2FFB85384F86BA");

                entity.ToTable("Addressee");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.CommentContentId).HasColumnName("CommentContentID");

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.CommentContent)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.CommentContentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Message__Comment__5629CD9C");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Message__Receive__5441852A");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Message__SenderI__5535A963");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
