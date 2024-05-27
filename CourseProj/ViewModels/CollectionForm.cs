using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using CourseProj.Models.Enums;
using CourseProj.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseProj.ViewModels;

public class CollectionForm
{
    [BindRequired]
    [NoWhitespace]
    public String Name { get; set; }
    [BindRequired]
    [NoWhitespace]
    public String Description { get; set; }
    [BindRequired]
    [NoWhitespace]
    public String fieldName1 { get; set; }

    
    public String fieldType1 { get; set; }
    
    [BindRequired]
    [NoWhitespace]
    public String fieldName2 { get; set; }
    
    
    public String fieldType2 { get; set; }
    [BindRequired]
    [NoWhitespace]
    public String fieldName3 { get; set; }
    
    public String fieldType3 { get; set; }

    public int CategoryId { get; set; }
    
    public IFormFile? Image { get; set; }
}