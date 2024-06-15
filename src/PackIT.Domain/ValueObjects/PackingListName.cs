using PackIT.Domain.Exceptions;

namespace PackIT.Domain.ValueObjects;

public record PackingListName
{
    public string Value { get; }

    private int _minimalLength = 3;

    public PackingListName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyPackingListNameException();
        if (value.Length < _minimalLength) throw new TooShortPackingListNameException(_minimalLength);

        Value = value;
    }

    public static implicit operator string(PackingListName name) => name.Value;
    
    public static implicit operator PackingListName(string name) => new(name);
}