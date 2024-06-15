using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions.PackingItemExceptions;

public class NegativeQuantityPackingItemException : PackItException
{
    public NegativeQuantityPackingItemException() : base("quantity should be a positive decimal")
    {
    }
}
