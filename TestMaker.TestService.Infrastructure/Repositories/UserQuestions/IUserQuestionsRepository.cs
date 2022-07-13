using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.UserQuestions
{
    public interface IUserQuestionsRepository: IRepository<UserQuestion>
    {
    }
}
