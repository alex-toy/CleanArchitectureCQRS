using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions.PackingListExceptions;

public class EmptyPackingListNameException : PackItException
{
    public EmptyPackingListNameException() : base("packing list name cannot be empty.")
    {
    }
}