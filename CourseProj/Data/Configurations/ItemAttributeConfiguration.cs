using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class ItemAttributeConfiguration : IEntityTypeConfiguration<ItemAttribute>
{
    public void Configure(EntityTypeBuilder<ItemAttribute> entity)
    {
        entity
            .HasOne(ia => ia.Item)
            .WithMany(i => i.ItemsAttributes)
            .HasForeignKey(ia => ia.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        entity
            .HasOne(ia => ia.ColllectionItemAttribute)
            .WithMany(a => a.ItemsAttributes)
            .HasForeignKey(ia => ia.CollectionItemAttributeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}