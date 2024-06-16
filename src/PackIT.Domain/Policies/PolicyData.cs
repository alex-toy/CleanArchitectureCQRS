using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies;

public record PolicyData(TravelDays Days, Enums.Gender Gender, ValueObjects.Temperature Temperature, Localization Localization);