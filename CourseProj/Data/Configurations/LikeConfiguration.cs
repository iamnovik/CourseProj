using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> entity)
    {
        entity
            .HasKey(ia => new { ia.ItemId, ia.AppUserId });

        entity
            .HasOne(l =>l.Item)
            .WithMany(i => i.Likes)
            .HasForeignKey(ia => ia.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        entity
            .HasOne(ia => ia.AppUser)
            .WithMany(a => a.Likes)
            .HasForeignKey(ia => ia.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}