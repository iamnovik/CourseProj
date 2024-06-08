using CourseProj.Models.Enums;

namespace CourseProj.Models;

public class JiraIssue
{
    public string Link { get; set; }
    
    public JiraStatus Status { get; set; }
}