using System.Text;

namespace TinyUrl;

public static class Program
{
    public static void Main(string[] args)
    {
        char[] validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        
        int id = 15436;
        List<int> digits = new List<int>();
        while (id > 0)
        {
            int carry = id % 62;
            digits.Add(carry);
            id /= 62;
        }

        while (digits.Count < 8)
        {
            digits.Add(0);
        }
        digits.Reverse();

        StringBuilder shortUrl = new StringBuilder();
        foreach (int digit in digits)
        {
            shortUrl.Append(validCharacters[digit]);
        }
        Console.WriteLine(shortUrl.ToString());
    }
}