using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.Sections
{
    public interface ISectionsRepository: IRepository<Section>
    {
        Task<List<Section>> GetSectionsAsync(SectionsFilter filter);
    }
}
