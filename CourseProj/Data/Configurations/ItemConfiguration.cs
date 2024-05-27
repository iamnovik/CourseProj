using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity
            .HasGeneratedTsVectorColumn(
                x => x.SearchVector,
                "english",
                x => new {x.Name})
            .HasIndex(x => x.SearchVector)
            .HasMethod("GIN");
    }
}