using System.Reflection.Emit;
using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class CollectionItemAttributeConfiguration : IEntityTypeConfiguration<CollectionItemAttribute>
{
    public void Configure(EntityTypeBuilder<CollectionItemAttribute> entity)
    {
        entity
            .HasOne(ia => ia.Collection)
            .WithMany(i => i.ItemsAttributes)
            .HasForeignKey(ia => ia.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        entity
            .HasOne(ia => ia.Attribute)
            .WithMany(a => a.ItemsAttributes)
            .HasForeignKey(ia => ia.AttributeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}