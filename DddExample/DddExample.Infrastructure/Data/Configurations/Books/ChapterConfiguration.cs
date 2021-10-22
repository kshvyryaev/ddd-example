using DddExample.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Infrastructure.Data.Configurations.Books
{
    internal class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("Chapters");

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
        }
    }
}