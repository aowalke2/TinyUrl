namespace TinyUrl.Utilities;

public class TinyUrlNotFoundException : Exception
{
    public TinyUrlNotFoundException()
    {
    }

    public TinyUrlNotFoundException(string message) : base(message)
    {
    }
}