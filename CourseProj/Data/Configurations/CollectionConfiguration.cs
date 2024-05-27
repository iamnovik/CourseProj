using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> entity)
    {
        entity
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Collections)
            .HasForeignKey(c => c.AppUserId);
        
        entity
            .HasOne(c => c.Category)
            .WithMany(u => u.Collections)
            .HasForeignKey(c => c.CategoryId);

        entity
            .HasMany(c => c.Items)
            .WithOne(i => i.Collection)
            .HasForeignKey(i => i.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity
            .HasGeneratedTsVectorColumn(
                c => c.SearchVector,
                "english",
                c => new { c.Name})
            .HasIndex(c => c.SearchVector)
            .HasMethod("GIN");
    }
}