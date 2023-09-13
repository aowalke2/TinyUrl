using TinyUrl.Models;

namespace TinyUrl.Repositories;

public interface IUrlRepository
{
    void CreateUrl(long id, string tinyUrl, string longUrl);
    Url GetUrl(string tinyUrl);
    void DeleteUrl(string tinyUrl);
}