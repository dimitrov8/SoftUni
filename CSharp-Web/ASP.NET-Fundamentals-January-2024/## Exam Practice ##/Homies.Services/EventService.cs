namespace Homies.Services;

using System.Globalization;
using Data;
using Data.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

public class EventService : IEventService
{
	private readonly HomiesDbContext _dbContext;

	public EventService(HomiesDbContext dbContext)
	{
		this._dbContext = dbContext;
	}

	private Event CreateNewEvent(string organiserId, EventFormModel model)
	{
		DateTime startDateTime, endDateTime;

		if (!DateTime.TryParseExact(model.Start, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime))
		{
			throw new ArgumentException("Invalid start date time format.", nameof(model.Start));
		}

		if (!DateTime.TryParseExact(model.End, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateTime))
		{
			throw new ArgumentException("Invalid end date time format.", nameof(model.End));
		}

		return new Event
		{
			Name = model.Name,
			Description = model.Description,
			OrganiserId = organiserId,
			CreatedOn = DateTime.Now,
			Start = startDateTime,
			End = endDateTime,
			TypeId = model.TypeId
		};
	}

	private async Task<bool> EventExistsAsync(Event newEvent)
	{
		return await this._dbContext.Events
			.AnyAsync(e =>
				e.Name == newEvent.Name &&
				e.Description == newEvent.Description &&
				e.Start == newEvent.Start &&
				e.End == newEvent.End &&
				e.TypeId == newEvent.TypeId);
	}

	public async Task<IEnumerable<EventTypeSelectViewModel>> GetTypesAsync()
	{
		IEnumerable<EventTypeSelectViewModel> allEventTypes = await this._dbContext
			.Types
			.Select(t => new EventTypeSelectViewModel
			{
				Id = t.Id,
				Name = t.Name
			})
			.AsNoTracking()
			.ToArrayAsync();

		return allEventTypes;
	}

	public async Task<bool> AddAsync(string organiserId, EventFormModel model)
	{
		if (model == null)
		{
			throw new ArgumentNullException(nameof(model), "EventFormModel cannot be null.");
		}

		if (string.IsNullOrWhiteSpace(model.Name))
		{
			throw new ArgumentException("Event name is required.", nameof(model.Name));
		}

		var newEvent = this.CreateNewEvent(organiserId, model);

		bool eventExists = await this.EventExistsAsync(newEvent);

		if (eventExists)
		{
			return false;
		}

		await this._dbContext
			.Events
			.AddAsync(newEvent);

		await this._dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<EventAllViewModel>> GetAllAsync()
	{
		IEnumerable<EventAllViewModel> events = await this._dbContext
			.Events
			.Select(e => new EventAllViewModel
			{
				Id = e.Id,
				Organiser = e.Organiser.ToString(),
				Name = e.Name,
				Start = e.Start.ToString("dd-MM-yyyy H:mm", CultureInfo.InvariantCulture),
				Type = e.Type.Name
			})
			.AsNoTracking()
			.ToArrayAsync();


		return events;
	}

	public async Task<EventDetailsViewModel?> ViewDetailsAsync(int id)
	{
		var viewModel = await this._dbContext
			.Events
			.Select(e => new EventDetailsViewModel
			{
				Id = e.Id,
				Name = e.Name,
				Description = e.Description,
				Start = e.Start.ToString("dd-MM-yyyy H:mm", CultureInfo.InvariantCulture),
				End = e.End.ToString("dd-MM-yyyy H:mm", CultureInfo.InvariantCulture),
				Organiser = e.Organiser.UserName,
				CreatedOn = e.CreatedOn.ToString("dd-MM-yyyy H:mm", CultureInfo.InvariantCulture),
				Type = e.Type.Name
			})
			.AsNoTracking()
			.FirstOrDefaultAsync(e => e.Id == id);

		return viewModel;
	}

	public async Task<EventFormModel?> GetForEditAsync(int eventId)
	{
		var viewModel = await this._dbContext
			.Events
			.Where(e => e.Id == eventId)
			.Select(e => new EventFormModel
			{
				Name = e.Name,
				Description = e.Description,
				Start = e.Start.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture),
				End = e.End.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture),
				TypeId = e.TypeId
			})
			.AsNoTracking()
			.FirstOrDefaultAsync();

		if (viewModel != null)
		{
			viewModel.Types = await this.GetTypesAsync();

			return viewModel;
		}

		return null;
	}

	public async Task EditAsync(int id, EventFormModel model)
	{
		var eventForEdit = await this._dbContext
			.Events
			.FirstOrDefaultAsync(e => e.Id == id);

		if (eventForEdit == null)
		{
			return;
		}

		eventForEdit.Name = model.Name;
		eventForEdit.Description = model.Description;
		eventForEdit.Start = DateTime.ParseExact(model.Start, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
		eventForEdit.End = DateTime.ParseExact(model.End, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
		eventForEdit.TypeId = model.TypeId;

		await this._dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<EventJoinedViewModel>> JoinedAsync(string userId)
	{
		IEnumerable<EventJoinedViewModel> events = await this._dbContext
			.Events
			.Where(e => e.EventsParticipants.Any(ep => ep.HelperId == userId) ||
			            e.OrganiserId == userId)
			.Select(e => new EventJoinedViewModel
			{
				Id = e.Id,
				Name = e.Name,
				Start = e.Start.ToString("dd-MM-yyyy H:mm", CultureInfo.InvariantCulture),
				Type = e.Type.Name,
				Organiser = e.Organiser.UserName
			})
			.AsNoTracking()
			.ToArrayAsync();

		return events;
	}

	public async Task<bool> JoinAsync(int eventId, string userId)
	{
		var eventToJoin = await this._dbContext
			.Events
			.FindAsync(eventId);

		if (eventToJoin == null)
		{
			throw new ArgumentException("Event not found", nameof(eventId));
		}

		var existingParticipant = await this._dbContext
			.EventParticipants.FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.HelperId == userId);

		if (existingParticipant != null)
		{
			return false;
		}

		var participant = new EventParticipant
		{
			EventId = eventId,
			HelperId = userId
		};

		await this._dbContext.EventParticipants.AddAsync(participant);
		await this._dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> LeaveAsync(int eventId, string userId)
	{
		var participant = await this._dbContext
			.EventParticipants
			.FirstOrDefaultAsync(ep => ep.Event.Id == eventId && ep.HelperId == userId);

		if (participant == null)
		{
			return false;
		}

		this._dbContext.EventParticipants.Remove(participant);
		await this._dbContext.SaveChangesAsync();

		return true;
	}
}