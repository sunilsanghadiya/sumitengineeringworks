namespace sew.Utility;

public class Utility
{
    public static string GenderateRandomNo(int length)
    {
        Random _rdm = new();
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[_rdm.Next(s.Length)]).ToArray());
    }
}
