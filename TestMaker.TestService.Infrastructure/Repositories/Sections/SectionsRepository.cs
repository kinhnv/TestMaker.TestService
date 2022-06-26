using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.Sections
{
    public class SectionsRepository: Repository<Section>, ISectionsRepository
    {
        public SectionsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Section>> GetSectionsAsync(SectionsFilter filter)
        {
            var query = _dbContext.Sections.AsQueryable();

            if (filter?.TestId != null)
            {
                query = query.Where(x => x.TestId == filter.TestId);
            }

            return await Task.FromResult(query.ToList());
        }
    }
}
