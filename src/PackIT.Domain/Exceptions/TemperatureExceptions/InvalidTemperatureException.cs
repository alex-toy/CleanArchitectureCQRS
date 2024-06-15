using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions.TemperatureExceptions
{
    public class InvalidTemperatureException : PackItException
    {
        public double Value { get; }

        public InvalidTemperatureException(double value) : base($"Value '{value}' is invalid temperature.")
            => Value = value;
    }
}