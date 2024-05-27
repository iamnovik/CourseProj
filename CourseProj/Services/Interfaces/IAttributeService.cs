using CourseProj.Models;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Services.Interfaces;

public interface IAttributeService
{
    Task<List<Attribute>> CreateAttributes(List<Attribute> attributes);

    Task<List<Attribute>> UpdateAttributes(List<Attribute> attributes);
}