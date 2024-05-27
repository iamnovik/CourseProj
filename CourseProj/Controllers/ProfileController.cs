using CourseProj.Data;
using CourseProj.Filters;
using CourseProj.Models;
using CourseProj.Models.Enums;
using CourseProj.Services.Interfaces;
using CourseProj.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Attribute = CourseProj.Models.Attribute;

namespace CourseProj.Controllers;
[TypeFilter(typeof(CheckBlockedUserFilter))]
public class ProfileController(IUserService userService,ICollectionService collectionService, IAttributeService attributeService, ICollectionItemAttributeService collectionItemAttributeService, ICategoryService categoryService) : Controller
{
    public async Task<IActionResult> Index(string id)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        var collections = await collectionService.GetCollectionsByUserId(id);
        var categories = await categoryService.GetCategories();
        foreach (var item in collections)
        {
            item.AppUser = user;
        }
        var viewModel = new ProfileVM
        {
            Categories = categories.ToList(),
            Collections = collections.ToList(),
            CollectionForm = new CollectionForm() // Пустая форма для заполнения
        };
        ViewBag.User = user;
        ViewBag.Show = false;
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCollectionVM(string id,ProfileVM profileVm)
    {
        
        if (!ModelState.IsValid)
        {
            // Валидация не прошла, вернуть форму обратно с ошибками
            var user = await userService.GetUserById(id);
            var collections = await collectionService.GetCollectionsByUserId(id);
            var categories = await categoryService.GetCategories();
            profileVm.Collections = collections.ToList();
            profileVm.Categories = categories.ToList();
            ViewBag.User = user;
            ViewBag.Show = true;

            return View("Index", profileVm);
        }
        var attributes = new List<Attribute>();
        attributes.Add(new Attribute { Name = profileVm.CollectionForm.fieldName1, Type = profileVm.CollectionForm.fieldType1});
        attributes.Add(new Attribute { Name = profileVm.CollectionForm.fieldName2, Type = profileVm.CollectionForm.fieldType2});
        attributes.Add(new Attribute { Name = profileVm.CollectionForm.fieldName3, Type = profileVm.CollectionForm.fieldType3});

        attributes = await attributeService.CreateAttributes(attributes);

        var collection = new Collection();
        collection.AppUserId = id;
        collection.CategoryId = profileVm.CollectionForm.CategoryId;
        collection.Name = profileVm.CollectionForm.Name;
        collection.Description = profileVm.CollectionForm.Description;

        collection = await collectionService.CreateCollection(collection, id, profileVm.CollectionForm.Image);

        var collectionItemAttributes = new List<CollectionItemAttribute>();
        for (int i = 0; i < 3; i++)
        {
            collectionItemAttributes.Add(new CollectionItemAttribute{AttributeId = attributes[i].Id, CollectionId = collection.Id});
        }

        await collectionItemAttributeService.CreateRows(collectionItemAttributes);
        
        return RedirectToAction("Index", new { id }); // Передача параметра id для восстановления состояния страницы
        

    }

    
    
    

    
    
}