using InterviewMVC.Models;

namespace InterviewMVC.Interfaces;

public interface ICocktailDbService
{

    Task<List<Drink>> GetDrinks(string drinkName);
    Task<List<Drink>> GetDrinkByIngredient(string drinkIngredient);
    Task<Drink?> GetDrinkById(string drinkId);

}