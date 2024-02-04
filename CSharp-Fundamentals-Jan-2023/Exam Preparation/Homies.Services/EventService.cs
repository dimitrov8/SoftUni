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

	public async Task<IEnumerable<EventTypeSelectViewModel>> GetEventTypesAsync()
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

	public async Task AddAsync(string organiserId, EventFormModel model)
	{
		var newEvent = new Event
		{
			Name = model.Name,
			Description = model.Description,
			OrganiserId = organiserId,
			CreatedOn = DateTime.Now,
			Start = DateTime.ParseExact(model.Start, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture),
			End = DateTime.ParseExact(model.End, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture),
			TypeId = model.TypeId
		};

		await this._dbContext.Events
			.AddAsync(newEvent);

		await this._dbContext.SaveChangesAsync();
	}
}