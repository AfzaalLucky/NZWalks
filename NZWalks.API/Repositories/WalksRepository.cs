using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalksRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)

            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }
    }
}
