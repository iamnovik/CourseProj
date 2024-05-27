using System.Reflection;
using CourseProj.Models;
using CourseProj.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<CollectionItemAttribute> CollectionItemAtributes { get; set; }
    public DbSet<ItemAttribute> ItemAttributes { get; set; }
    public DbSet<ItemTag> ItemTags { get; set; }
    
    public DbSet<Like> Likes { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

   
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
 
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new {x.UserId,x.RoleId});
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => x.UserId);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
    }
}