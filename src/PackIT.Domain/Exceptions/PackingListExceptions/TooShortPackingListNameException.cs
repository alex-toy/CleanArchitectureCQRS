using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions.PackingListExceptions;

public class TooShortPackingListNameException : PackItException
{
    public TooShortPackingListNameException(int minimalLength) : base($"Packing list name should be more than {minimalLength} characters long.")
    {
    }
}
