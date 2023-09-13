namespace TinyUrl.Utilities;

public class TinyUrlExistsException : Exception
{
    public TinyUrlExistsException()
    {
    }

    public TinyUrlExistsException(string message) : base(message)
    {
    }
}