using TinyUrl.Models;
using TinyUrl.Utilities;

namespace TinyUrl.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly Dictionary<long, Url> _urls = new Dictionary<long, Url>();
    private readonly Dictionary<string, long> _tinyUrlIndex = new Dictionary<string, long>();
    
    public void CreateUrl(long id, string tinyUrl, string longUrl)
    {
        if (_tinyUrlIndex.ContainsKey(tinyUrl))
        {
            throw new TinyUrlExistsException("Tiny url already exists");
        }
        
        var url = new Url(id, tinyUrl, longUrl);
        _urls[id] = url;
        _tinyUrlIndex[tinyUrl] = id;
    }

    public Url GetUrl(string tinyUrl)
    {
        if (!_tinyUrlIndex.ContainsKey(tinyUrl))
        {
            throw new TinyUrlNotFoundException("Tiny url not found");
        }

        var id = _tinyUrlIndex[tinyUrl];
        var url = _urls[id];
        url.TimesAccessed++;
        return url;
    }

    public void DeleteUrl(string tinyUrl)
    {
        if (!_tinyUrlIndex.ContainsKey(tinyUrl))
        {
            throw new TinyUrlNotFoundException("Tiny url not found");
        }

        var id = _tinyUrlIndex[tinyUrl]; 
        _urls.Remove(id);
        _tinyUrlIndex.Remove(tinyUrl);
    }
}