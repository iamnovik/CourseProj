﻿using CourseProj.Models;
using CourseProj.Services.Implementations;
using CourseProj.Services.Interfaces;
using CourseProj.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProj.Controllers;

public class TicketController(JiraService jiraService, UserManager<AppUser> userManager, ILogger<TicketController> _logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        var issues = await jiraService.GetIssuesAsync();

        // Передача списка во View
        return View(issues);
    }
    public async Task<IActionResult> CreateIssue(JiraIssueVM jiraIssueVm, string collection, string link)
    {
        
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }
        
        string email = user.Email;
        string displayName = user.UserName;
        try
        {
            string issueKey = await jiraService.CreateIssueAsync(jiraIssueVm.Description, jiraIssueVm.Priority.ToString(), collection, link);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing your request.");
            return View("Error"); // Отобразить страницу ошибки
        }
        
        

        return Redirect(link);

    }

    public async Task<IActionResult> GetIssues()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        //var issues = await jiraSerivce.GetIssuesAsync("assignee = currentUser() ORDER BY created DESC");

        return Ok();
    }
}