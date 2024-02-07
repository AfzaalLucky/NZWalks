using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true, int pageNumber = 1, int pageSize = 1000);
    }
}
