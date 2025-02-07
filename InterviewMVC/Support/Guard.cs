namespace InterviewMVC.Support;

public static class Guard
{
    public static class Against
    {
        public static void NullOrWhiteSpace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("String can't be null or white space");
            }
        }
    }
}