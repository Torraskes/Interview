using InterviewMVC.Interfaces;
using InterviewMVC.Models;
using InterviewMVC.Support;
using Microsoft.AspNetCore.Mvc;

namespace InterviewMVC.Controllers;

public class DrinkController (ICocktailDbService cocktailDbService) : Controller
{
    private static readonly DrinkViewModel DrinkList = new DrinkViewModel();
    private bool _searchByName = false;
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(DrinkList);
    }
    
    [HttpGet]
    public async Task<IActionResult> Detail(string idDrink)
    {
        
        var drinkToShow = DrinkList.SearchResults.FirstOrDefault(d => d.IdDrink == idDrink) ?? new Drink();
        if (!_searchByName && !string.IsNullOrWhiteSpace(drinkToShow.IdDrink))
        {
            drinkToShow = await cocktailDbService.GetDrinkById(drinkToShow.IdDrink);
        }
        drinkToShow?.GetTags();
        return View(drinkToShow);

    }
    
    [HttpGet]
    public async Task<ActionResult> DetailPopUp(string idDrink)
    {
        var drinkToShow = DrinkList.SearchResults.FirstOrDefault(d => d.IdDrink == idDrink) ?? new Drink();
        if (!_searchByName && !string.IsNullOrWhiteSpace(drinkToShow.IdDrink))
        {
            drinkToShow = await cocktailDbService.GetDrinkById(drinkToShow.IdDrink);
        }
        drinkToShow?.GetTags();
        return PartialView("Detail", drinkToShow);
    }
    
    [HttpPost]
    public async Task<IActionResult> Search(string drinkName)
    {
        if (string.IsNullOrWhiteSpace(drinkName))
        {
            DrinkList.SearchResults = new List<Drink>();
        }
        else
        {
            DrinkList.SearchResults = await cocktailDbService.GetDrinks(drinkName.SanitizeDrinkName());            
        }

        _searchByName = true;
        return View("Index", DrinkList);
    }
    
    [HttpPost]
    public IActionResult AddDrink(string idDrink)
    {
        var drinkToAdd = DrinkList.SearchResults.FirstOrDefault(d => d.IdDrink == idDrink);
        if (drinkToAdd is not null)
        {
            DrinkList.AddedDrinks.Add(drinkToAdd);    
        }
        return View("Index", DrinkList);
    }
    
    [HttpPost]
    public IActionResult RemoveDrink(string idDrink)
    {
        var drinkToRemove = DrinkList.AddedDrinks.FirstOrDefault(d => d.IdDrink == idDrink);
        if (drinkToRemove is not null)
        {
            DrinkList.AddedDrinks.Remove(drinkToRemove);    
        }
        return View("Index", DrinkList);
    }
    
    [HttpPost]
    public async Task<IActionResult> SearchByIngredient(string drinkIngredient)
    {
        if (string.IsNullOrWhiteSpace(drinkIngredient))
        {
            DrinkList.SearchResults = new List<Drink>();
        }
        else
        {
            DrinkList.SearchResults = await cocktailDbService.GetDrinkByIngredient(drinkIngredient.SanitizeDrinkName());            
        }

        _searchByName = false;
        return View("Index", DrinkList);
    }
    
    
    
}