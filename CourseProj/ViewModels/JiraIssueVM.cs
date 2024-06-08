using CourseProj.Models.Enums;

namespace CourseProj.ViewModels;

public class JiraIssueVM
{
    public string Description { get; set; }
    public JiraPriority Priority { get; set; }
}