using CourseProj.Data;
using CourseProj.Filters;
using CourseProj.Models;
using CourseProj.Services.Interfaces;
using CourseProj.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;

namespace CourseProj.Controllers;
[TypeFilter(typeof(CheckBlockedUserFilter))]
public class CollectionController(ICollectionService collectionService, IItemService itemService, IItemAttributeService itemAttributeService) : Controller
{
    public async Task<IActionResult> Index(int id)
    {

        var collection = await collectionService.GetCollectionById(id);
        var viewModel = new CollectionVM
        {
            Collection = collection,
            ItemForm = new ItemForm() // Пустая форма для заполнения
        };
        ViewBag.Show = false;
        return View(viewModel);
    }
    
        
    [HttpPost]
    public async Task<IActionResult> CreateCollection(Collection collection, string id, IFormFile image)
    {

        var collectionAdded = collectionService.CreateCollection(collection, id, image);

        return Ok(collectionAdded.Id); 
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateItemVM(int id,string collectionJson, CollectionVM collectionForm)
    {
        
        Collection collection = JsonConvert.DeserializeObject<Collection>(collectionJson);
        
        
        if (!ModelState.IsValid)
        {
            
            collectionForm.Collection = collection;
            ViewBag.Show = true;

            return View("Index", collectionForm);
        }
        
        var item = await itemService.CreateItem(collectionForm.ItemForm.Name, collection.Id);

        await itemAttributeService.CreateItemAttribute(collectionForm.ItemForm.Fields, item.Id,
            collection.ItemsAttributes.Select(ia => ia.Id).ToList());
        return RedirectToAction("Index", new { id }); // Передача параметра id для восстановления состояния страницы
        

    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteCollections(List<int> collIds)
    {
        await collectionService.DeleteCollections(collIds);

        return Ok(); 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCollectionData(int id)
    {
        // Найти коллекцию по id
        var collection = await collectionService.GetCollectionDataById(id);

        return Ok(collection);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateCollectionName(string value, int id)
    {
        var collectionUpdated = await collectionService.UpdateCollectionName(value, id);
        return Ok(collectionUpdated);


    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateCollectionDescription(string value, int id)
    {
        var collectionUpdated = await collectionService.UpdateCollectionDescription(value, id);
            
        return Ok(collectionUpdated);


    }

    [HttpPost]
    public async Task<IActionResult> UpdateCollectionImage(IFormFile imageFile, int id)
    {
        var collectionUpdated = await collectionService.UpdateCollectionImage(imageFile, id);
        return Ok(collectionUpdated);
    }
    
}