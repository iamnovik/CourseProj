using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;
using CourseProj.Models;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Services.Implementations;

public class AttributeService(IAttributeRepository attributeRepository) : IAttributeService
{
    public async Task<List<Attribute>> CreateAttributes(List<Attribute> attributes)
    {
        var attributesAdded = await attributeRepository.CreateAttributes(attributes);
        return attributesAdded;
    }

    public async Task<List<Attribute>> UpdateAttributes(List<Attribute> attributes)
    {
        var attributesUpdated = await attributeRepository.UpdateAttributes(attributes);
        return attributesUpdated;
    }
}