using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.Questions
{
    public class QuestionsRepository : Repository<Question>, IQuestionsRepository
    {
        public QuestionsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
