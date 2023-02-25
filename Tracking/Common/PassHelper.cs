namespace Common;

public static class PassHelper
{
    public static bool IsValidPassword(string password)
    {
        return password.All(x => x != ' ');
    }
}