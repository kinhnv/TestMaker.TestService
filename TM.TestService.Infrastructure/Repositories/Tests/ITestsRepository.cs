﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.TestService.Infrastructure.Entities;

namespace TM.TestService.Infrastructure.Repositories.Tests
{
    public interface ITestsRepository: IRepository<Test>
    {
    }
}
