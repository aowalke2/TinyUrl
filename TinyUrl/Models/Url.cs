namespace TinyUrl.Models;

public class Url
{
    public Url(long id, string tinyUrl, string longUrl)
    {
        Id = id;
        TinyUrl = tinyUrl;
        LongUrl = longUrl;
    }
    
    public long Id { get; }
    public string TinyUrl { get; }
    public string LongUrl { get; }
    public int TimesAccessed { get; set; }
}