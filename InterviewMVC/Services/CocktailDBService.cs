using System.Text.Json;
using InterviewMVC.Interfaces;
using InterviewMVC.Models;

namespace InterviewMVC.Services;

public class CocktailDbService(HttpClient httpClient) : ICocktailDbService
{
    public async Task<List<Drink>> GetDrinks(string drinkName)
    {
        var response = await httpClient.GetAsync($"search.php?s={drinkName}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var wrapper = JsonSerializer.Deserialize<DrinkWrapper>(content,new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
        return wrapper?.Drinks ?? [];
    }

    public async Task<List<Drink>> GetDrinkByIngredient(string drinkIngredient)
    {
        var response = await httpClient.GetAsync($"filter.php?i={drinkIngredient}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        if (content.Contains("no data found", StringComparison.OrdinalIgnoreCase))
        {
            return [];
        }
        var wrapper = JsonSerializer.Deserialize<DrinkWrapper>(content,new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
        
        return wrapper?.Drinks ?? [];
    }
    
    
    public async Task<Drink?> GetDrinkById(string drinkId)
    {
        var response = await httpClient.GetAsync($"lookup.php?i={drinkId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        if (content.Contains("no data found", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        var wrapper = JsonSerializer.Deserialize<DrinkWrapper>(content,new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
        return wrapper?.Drinks[0];
    }
}