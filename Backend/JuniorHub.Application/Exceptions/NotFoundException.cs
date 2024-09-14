namespace JuniorHub.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"{name} ({key}) is not found")
    {
    }

    public NotFoundException(string name, object key1, object key2)
        : base($"{name} with ({key1}) from employer is not found")
    {
    }
}

