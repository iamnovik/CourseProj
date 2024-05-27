using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProj.Models;

public class ItemAttribute
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public String Value { get; set; }
    public int CollectionItemAttributeId { get; set; }
    public CollectionItemAttribute ColllectionItemAttribute{ get; set; }
}