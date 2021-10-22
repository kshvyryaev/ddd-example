using DddExample.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Infrastructure.Data.Configurations.Books
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted);
            
            builder.Ignore(x => x.PreDomainEvents);
            
            builder.Ignore(x => x.PostDomainEvents);
            
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Id)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .IsRequired();
            
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(4000)
                .IsRequired(false);
            
            builder.Property(x => x.TypeId)
                .IsRequired();
            
            builder.HasOne("BookTypes")
                .WithMany()
                .HasForeignKey("TypeId")
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(x => x.Chapters)
                .WithOne()
                .HasForeignKey("BookId");
        }
    }
}