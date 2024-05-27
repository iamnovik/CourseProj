using CourseProj.Models;
using CourseProj.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseProj.ViewModels;

public class ProfileVM
{

    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Collection> Collections { get; set; } = new List<Collection>();
    
    [BindRequired]
    public CollectionForm CollectionForm { get; set; }
}