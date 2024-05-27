using CourseProj.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseProj.ViewModels;

public class CollectionVM
{
    public Collection? Collection { get; set; }
    [BindRequired]
    public ItemForm ItemForm { get; set; }
}