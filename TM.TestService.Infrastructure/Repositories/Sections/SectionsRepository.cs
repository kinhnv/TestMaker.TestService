using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.TestService.Infrastructure.Entities;

namespace TM.TestService.Infrastructure.Repositories.Sections
{
    public class SectionsRepository: Repository<Section>, ISectionsRepository
    {
        public SectionsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
