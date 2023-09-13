using System.Text;
using System.Text.RegularExpressions;
using TinyUrl.Models;
using TinyUrl.Repositories;
using TinyUrl.Utilities;

namespace TinyUrl.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    
    private readonly char[] _validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
    private const short TinyUrlLength = 8;

    public UrlService(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }
    
    public string CreateUrl(string longUrl)
    {
        if (string.IsNullOrWhiteSpace(longUrl))
        {
            throw new ArgumentException("longUrl is null, empty or whitespace");
        }

        if (Regex.IsMatch(longUrl, @"\s"))
        {
            throw new ArgumentException("longUrl should not contain space");
        }

        var id = IdGenerator.GetNextId();
        var digits = new List<long>();
        while (id > 0)
        {
            var carry = id % _validCharacters.Length;
            digits.Add(carry);
            id /= _validCharacters.Length;
        }
        
        while (digits.Count < TinyUrlLength)
        {
            digits.Add(0);
        }
        digits.Reverse();

        StringBuilder tinyUrl = new StringBuilder();
        foreach (int digit in digits)
        {
            tinyUrl.Append(_validCharacters[digit]);
        }
        
        _urlRepository.CreateUrl(id, tinyUrl.ToString(), longUrl);
        return "https://tinyurl.com/" + tinyUrl;
    }
    
    public string CreateUrl(string tinyUrl, string longUrl)
    {
        if (string.IsNullOrWhiteSpace(tinyUrl))
        {
            throw new ArgumentException("tinyUrl is null, empty or whitespace");
        }
        
        if (string.IsNullOrWhiteSpace(longUrl))
        {
            throw new ArgumentException("longUrl is null, empty or whitespace");
        }

        if (Regex.IsMatch(tinyUrl, @"\s"))
        {
            throw new ArgumentException("tinyUrl should not contain space");
        }
        
        if (Regex.IsMatch(longUrl, @"\s"))
        {
            throw new ArgumentException("longUrl should not contain space");
        }

        tinyUrl = tinyUrl.Split('/').Last();
        _urlRepository.CreateUrl(IdGenerator.GetNextId(), tinyUrl, longUrl);
        return "https://tinyurl.com/" + tinyUrl;
    }

    public Url GetUrl(string tinyUrl)
    {
        if (string.IsNullOrWhiteSpace(tinyUrl))
        {
            throw new ArgumentException("tinyUrl is null, empty or whitespace");
        }

        if (Regex.IsMatch(tinyUrl, @"\s"))
        {
            throw new ArgumentException("tinyUrl should not contain space");
        }
        
        return _urlRepository.GetUrl(tinyUrl.Split('/').Last());
    }

    public void DeleteUrl(string tinyUrl)
    {
        if (string.IsNullOrWhiteSpace(tinyUrl))
        {
            throw new ArgumentException("tinyUrl is null, empty or whitespace");
        }

        if (Regex.IsMatch(tinyUrl, @"\s"))
        {
            throw new ArgumentException("tinyUrl should not contain space");
        }
        
        _urlRepository.DeleteUrl(tinyUrl.Split('/').Last());
    }
}