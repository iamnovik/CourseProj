using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProj.Models;

public class CollectionItemAttribute
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }

    public int AttributeId { get; set; }
    public Attribute Attribute{ get; set; }
    
    public virtual ICollection<ItemAttribute> ItemsAttributes { get; set; }
}