using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class ItemTagConfiguration : IEntityTypeConfiguration<ItemTag>
{
    public void Configure(EntityTypeBuilder<ItemTag> entity)
    {
        entity
            .HasKey(ia => new { ia.ItemId, ia.TagId });

        entity
            .HasOne(ia => ia.Item)
            .WithMany(i => i.ItemsTags)
            .HasForeignKey(ia => ia.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        entity
            .HasOne(ia => ia.Tag)
            .WithMany(a => a.ItemsTags)
            .HasForeignKey(ia => ia.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}