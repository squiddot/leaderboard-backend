using LeaderboardBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardBackend.Services;

public class ModshipService : IModshipService
{
	private readonly ApplicationContext _applicationContext;

	public ModshipService(ApplicationContext applicationContext)
	{
		_applicationContext = applicationContext;
	}

	public async Task<Modship?> GetModship(Guid userId)
	{
		return await _applicationContext.Modships
			.FirstOrDefaultAsync(m => m.UserId == userId);
	}

	public async Task<Modship?> GetModshipForLeaderboard(long leaderboardId, Guid userId)
	{
		return await _applicationContext.Modships
			.SingleOrDefaultAsync(m => m.LeaderboardId == leaderboardId && m.UserId == userId);
	}

	public async Task CreateModship(Modship modship)
	{
		_applicationContext.Modships.Add(modship);
		await _applicationContext.SaveChangesAsync();
	}

	public async Task<List<Modship>> LoadUserModships(Guid userId)
	{
		return await _applicationContext.Modships
			.Where(m => m.UserId == userId)
			.ToListAsync();
	}

	public async Task DeleteModship(Modship modship)
	{
		_applicationContext.Entry(modship).State = EntityState.Deleted;

		await _applicationContext.SaveChangesAsync();
	}
}
