using System.Threading.Tasks;
using PackIT.Application.Commands;
using PackIT.Application.DTO.External;
using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Entities;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Handlers;

public class CreatePackingListWithItemsHandler : ICommandHandler<CreatePackingListWithItemsCommand>
{
    private readonly IPackingListRepository _repository;
    private readonly IPackingListFactory _factory;
    private readonly IPackingListReadService _readService;
    private readonly IWeatherService _weatherService;

    public CreatePackingListWithItemsHandler(IPackingListRepository repository, IPackingListFactory factory,
        IPackingListReadService readService, IWeatherService weatherService)
    {
        _repository = repository;
        _factory = factory;
        _readService = readService;
        _weatherService = weatherService;
    }

    public async Task HandleAsync(CreatePackingListWithItemsCommand command)
    {
        var (id, name, days, gender, localizationWriteModel) = command;

        if (await _readService.ExistsByNameAsync(name)) throw new PackingListAlreadyExistsException(name);

        Localization localization = new (localizationWriteModel.City, localizationWriteModel.Country);
        WeatherDto weather = await _weatherService.GetWeatherAsync(localization);

        if (weather is null)  throw new MissingLocalizationWeatherException(localization);

        PackingList packingList = _factory.CreateWithDefaultItems(id, name, days, gender, weather.Temperature, localization);

        await _repository.AddAsync(packingList);
    }
}