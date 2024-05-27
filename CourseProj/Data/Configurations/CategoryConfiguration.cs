using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.
            HasData(
                new Category{Id = 1, Name = "Albums"},
                new Category{Id = 2, Name = "Films"},
                new Category{Id = 3, Name = "Books"},
                new Category{Id = 4, Name = "Other"}
            );


    }
}