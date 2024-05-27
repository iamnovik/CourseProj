using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;

namespace CourseProj.Repositories.Implementations;

public class CategoryRepository(AppDbContext appDbContext) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategories()
    {
        var categories = appDbContext.Categories;
        return categories;
    }
}