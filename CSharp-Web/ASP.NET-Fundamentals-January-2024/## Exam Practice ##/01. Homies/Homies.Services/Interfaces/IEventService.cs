namespace Homies.Services.Interfaces;

using Web.ViewModels;

public interface IEventService
{
	Task<IEnumerable<EventTypeSelectViewModel>> GetTypesAsync();

	Task<bool> AddAsync(string organiserId, EventFormModel model);

	Task<IEnumerable<EventAllViewModel>> GetAllAsync();

	Task<EventDetailsViewModel?> ViewDetailsAsync(int id);

	Task<EventFormModel?> GetForEditAsync(int eventId);

	Task EditAsync(int id, EventFormModel model);

	Task<IEnumerable<EventJoinedViewModel>> JoinedAsync(string organiserId);

	Task<bool> JoinAsync(int eventId, string userId);

	Task<bool> LeaveAsync(int eventId, string userId);
}