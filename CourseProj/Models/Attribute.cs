using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProj.Models;

public class Attribute
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public String Name { get; set; }

    public String Type { get; set; }
    
    public virtual ICollection<CollectionItemAttribute> ItemsAttributes { get; set; }
}