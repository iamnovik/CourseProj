using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;


namespace CourseProj.Models;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }


    public int CollectionId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public virtual Collection? Collection { get; set; }
    
    
    public virtual ICollection<ItemTag>? ItemsTags { get; set; }
    
    public virtual ICollection<Like>? Likes { get; set; }
    
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<ItemAttribute>? ItemsAttributes { get; set; }
    
    public NpgsqlTsVector SearchVector { get; set; }
}