
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Repositories.Interfaces;

public interface IAttributeRepository
{
    Task<List<Attribute>> CreateAttributes(List<Attribute> attributes);

    Task<List<Attribute>> UpdateAttributes(List<Attribute> attributes);
}