using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories();
}