using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategories();
}