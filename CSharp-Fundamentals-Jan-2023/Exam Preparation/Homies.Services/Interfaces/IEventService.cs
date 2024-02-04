namespace Homies.Services.Interfaces;

using Web.ViewModels;

public interface IEventService
{
	Task<IEnumerable<EventTypeSelectViewModel>> GetEventTypesAsync();

	Task AddAsync(string organiserId, EventFormModel model);
}