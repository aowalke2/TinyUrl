using TinyUrl.Repositories;
using TinyUrl.Services;

namespace TinyUrl;

public static class Program
{
    static readonly IUrlRepository _urlRepository = new UrlRepository();
    static readonly IUrlService _urlService = new UrlService(_urlRepository);
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to tinyUrl!");
        Console.WriteLine("To create a tinyUrl type -> create\nTo get a longUrl from a tinyUrl type -> get\nTo delete a tinyUrl type -> delete");

        while (true)
        {
            Console.WriteLine("\nWhat would you like to do?");

            string input = Console.ReadLine();
            if (input.ToLower().Equals("create"))
            {
                Console.WriteLine("What is your long url?");
                string longUrl = Console.ReadLine();
                
                Console.WriteLine("Do you want to use a custom tinyUrl (Y or N)?");
                string decision = Console.ReadLine();
                if (decision.ToLower().Equals("y"))
                {
                    Console.WriteLine("What is your tiny url?");
                    string tinyUrl = Console.ReadLine();
                    Console.WriteLine(Create(tinyUrl, longUrl));
                }
                else
                {
                    Console.WriteLine(Create(longUrl));
                }

            } 
            else if (input.ToLower().Equals("get")) 
            {
                Console.WriteLine("What tinyUrl are you retrieving?");
                string tinyUrl = Console.ReadLine();
                Console.WriteLine(Get(tinyUrl));
            }
            else if (input.ToLower().Equals("delete"))
            {
                Console.WriteLine("What tinyUrl are you deleting?");
                string tinyUrl = Console.ReadLine();
                Console.WriteLine(Delete(tinyUrl));
            }
            else
            {
                Console.WriteLine("That was a bad input try again");
            }
        }
    }

    private static string Create(string longUrl)
    {
        try
        {
            return _urlService.CreateUrl(longUrl);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    private static string Create(string tinyUrl, string longUrl)
    {
        try
        {
            return _urlService.CreateUrl(tinyUrl, longUrl);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    private static string Get(string tinyUrl)
    {
        try
        {
            var url = _urlService.GetUrl(tinyUrl);
            return $"The long Url is {url.LongUrl} and this url has been accessed {url.TimesAccessed}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    private static string Delete(string tinyUrl)
    {
        try
        {
            _urlService.DeleteUrl(tinyUrl);
            return "Tiny url deleted";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}