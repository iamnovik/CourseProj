using CourseProj.Models;

namespace CourseProj.ViewModels;

public class HomeVM
{
    public List<Item> LatestItems { get; set; }
    public List<Collection> LargestCollections { get; set; }
    
    public List<Tag> Tags { get; set; }
}