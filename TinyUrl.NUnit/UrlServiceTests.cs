using NUnit.Framework;
using NUnit.Framework.Internal;
using TinyUrl.Repositories;
using TinyUrl.Services;
using TinyUrl.Utilities;

namespace TinyUrl.NUnit;

public class Tests
{
    private IUrlService _urlService;
    
    [SetUp]
    public void Setup()
    {
        _urlService = new UrlService(new UrlRepository());
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void CreateUrl_Throws_WhenPassedLongUrlWithNullOrWhitespace(string longUrl)
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.CreateUrl(longUrl));
        Assert.That("longUrl is null, empty or whitespace", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void CreateUrl_Throws_WhenPassedLongUrlWithTextWithSpaces()
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.CreateUrl("jdfdal  dd aadd"));
        Assert.That("longUrl should not contain space", Is.EqualTo(exception.Message));
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void CreateUrl_Throws_WhenPassedTinyUrlWithNullOrWhitespace(string tinyUrl)
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.CreateUrl(tinyUrl, "https://www.adroit-tt.com"));
        Assert.That("tinyUrl is null, empty or whitespace", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void CreateUrl_Throws_WhenPassedTinyUrlWithTextWithSpaces()
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.CreateUrl("jdfdal  dd aadd", "https://www.adroit-tt.com"));
        Assert.That("tinyUrl should not contain space", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void CreateUrl_Throws_WhenPassedTinyUrlThatAlreadyExists()
    {
        var longUrl = "https://www.adroit-tt.com";
        var customTinyUrl = "custom";
        var tinyUrl = _urlService.CreateUrl(customTinyUrl, longUrl);
        
        var exception = Assert.Throws<TinyUrlExistsException>(() => _urlService.CreateUrl(tinyUrl, longUrl));
        Assert.That("Tiny url already exists", Is.EqualTo(exception.Message));
        
        exception = Assert.Throws<TinyUrlExistsException>(() => _urlService.CreateUrl(customTinyUrl, longUrl));
        Assert.That("Tiny url already exists", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void CreateUrl_CreatesTinyUrl()
    {
        var longUrl = "https://www.adroit-tt.com";
        var tinyUrl = _urlService.CreateUrl(longUrl);
        var url = _urlService.GetUrl(tinyUrl);
        
        Assert.That(longUrl, Is.EqualTo(url.LongUrl));
    }
    
    [Test]
    public void CreateUrl_CreatesTinyUrl_WhenPassedUnusedCustomPhrase()
    {
        var customTinyUrl = "custom";
        var longUrl = "https://www.adroit-tt.com";
        var tinyUrl = _urlService.CreateUrl(customTinyUrl, longUrl);
        var url = _urlService.GetUrl(tinyUrl);
        
        Assert.That(longUrl, Is.EqualTo(url.LongUrl));
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void GetUrl_Throws_WhenPassedTinyUrlWithNullOrWhitespace(string tinyUrl)
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.GetUrl(tinyUrl));
        Assert.That("tinyUrl is null, empty or whitespace", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void GetUrl_Throws_WhenPassedTinyUrlWithTextWithSpaces()
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.GetUrl("jdfdal  dd aadd"));
        Assert.That("tinyUrl should not contain space", Is.EqualTo(exception.Message));
    }

    [Test]
    public void GetUrl_Throws_WhenTinyDoesNotExists()
    {
        var exception = Assert.Throws<TinyUrlNotFoundException>(() => _urlService.GetUrl("tiny"));
        Assert.That("Tiny url not found", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void GetUrl_RetrievesLongUrlAndUpdatesAccessedCount()
    {
        var longUrl = "https://www.adroit-tt.com";
        var tinyUrl = _urlService.CreateUrl(longUrl);
        _urlService.GetUrl(tinyUrl);
        _urlService.GetUrl(tinyUrl);
        var url = _urlService.GetUrl(tinyUrl);
        
        Assert.That(longUrl, Is.EqualTo(url.LongUrl));
        Assert.That(3, Is.EqualTo(url.TimesAccessed));
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void DeleteUrl_Throws_WhenPassedTinyUrlWithNullOrWhitespace(string tinyUrl)
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.DeleteUrl(tinyUrl));
        Assert.That("tinyUrl is null, empty or whitespace", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void DeleteUrl_Throws_WhenPassedTinyUrlWithTextWithSpaces()
    {
        var exception = Assert.Throws<ArgumentException>(() => _urlService.DeleteUrl("jdfdal  dd aadd"));
        Assert.That("tinyUrl should not contain space", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void DeleteUrl_Throws_WhenTinyDoesNotExists()
    {
        var exception = Assert.Throws<TinyUrlNotFoundException>(() => _urlService.DeleteUrl("tiny"));
        Assert.That("Tiny url not found", Is.EqualTo(exception.Message));
    }
    
    [Test]
    public void DeleteUrl_DeleteTinyUrl()
    {
        var longUrl = "https://www.adroit-tt.com";
        var tinyUrl = _urlService.CreateUrl(longUrl);
        _urlService.DeleteUrl(tinyUrl);
        
        var exception = Assert.Throws<TinyUrlNotFoundException>(() => _urlService.GetUrl(tinyUrl));
        Assert.That("Tiny url not found", Is.EqualTo(exception.Message));
    }
}