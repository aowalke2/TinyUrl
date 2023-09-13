using TinyUrl.Models;

namespace TinyUrl.Services;

public interface IUrlService
{
    string CreateUrl(string longUrl);
    string CreateUrl(string tinyUrl, string longUrl);
    Url GetUrl(string tinyUrl);
    void DeleteUrl(string tinyUrl);
}