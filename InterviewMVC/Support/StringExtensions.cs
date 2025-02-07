namespace InterviewMVC.Support;

public static class StringExtensions
{
    public static string SanitizeDrinkName(this string drinkName)
    {
        return drinkName.Trim().ToLower();
    }
    
}