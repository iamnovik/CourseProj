using CourseProj.Models;
using CourseProj.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseProj.ViewModels;

public class ItemForm
{
    [BindRequired]
    [NoWhitespace]
    public string Name { get; set; }
    
    [NoWhitespaceList]
    public List<string> Fields { get; set; } = new List<string>();
    

}