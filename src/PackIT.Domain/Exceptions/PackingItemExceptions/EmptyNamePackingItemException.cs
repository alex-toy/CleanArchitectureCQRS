using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions.PackingItemExceptions;

public class EmptyNamePackingItemException : PackItException
{
    public EmptyNamePackingItemException() : base("Packing item name cannot be empty.")
    {
    }
}