namespace TinyUrl.Utilities;

public static class IdGenerator
{
    private static long _currentId = 1000000;
    private static readonly object Lock = new object();

    public static long GetNextId()
    {
        lock (Lock)
        {
            _currentId++;
            return _currentId;
        }
    }
}