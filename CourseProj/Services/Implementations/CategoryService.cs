using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await categoryRepository.GetCategories();
    }
}