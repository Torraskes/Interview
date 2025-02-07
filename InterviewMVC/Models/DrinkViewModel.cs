namespace InterviewMVC.Models;

public class DrinkViewModel
{
    public List<Drink> SearchResults { get; set; } = new List<Drink>();
    public List<Drink> AddedDrinks { get; set; } = new List<Drink>();
}