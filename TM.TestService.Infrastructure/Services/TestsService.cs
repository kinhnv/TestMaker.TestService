using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.TestService.Domain.Models;
using TM.TestService.Domain.Models.Test;
using TM.TestService.Domain.Services;
using TM.TestService.Infrastructure.Entities;
using TM.TestService.Infrastructure.Repositories.Tests;

namespace TM.TestService.Infrastructure.Services
{
    public class TestsService : ITestsService
    {
        #region Fields
        private readonly ITestsRepository _testRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctrls
        public TestsService(ITestsRepository repository, IMapper mapper)
        {
            _testRepository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<TestForDetails> GetTestAsync(Guid testId)
        {
            var test = await _testRepository.GetAsync(testId);

            if (test == null)
                return null;

            return await Task.FromResult(_mapper.Map<TestForDetails>(test));
        }

        public async Task<IEnumerable<TestForList>> GetTestsAsync()
        {
            var result = (await _testRepository.GetAllAsync()).Select(test => _mapper.Map<TestForList>(test));

            return result;
        }

        public async Task<TestForDetails> CreateTestAsync(TestForCreating test)
        {
            var entity = _mapper.Map<Test>(test);

            var result = await _testRepository.CreateAsync(entity);
            if (result)
                return await GetTestAsync(entity.TestId);
            else
                return null;
        }

        public async Task<bool> DeleteTestAsync(Guid testId)
        {
            return await _testRepository.DeleteAsync(testId);
        }

        public async Task<bool> EditTestAsync(TestForEditing test)
        {
            var entity = _mapper.Map<Test>(test);

            return await _testRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<SelectOption>> GetTestsAsSelectOptionsAsync()
        {
            return (await _testRepository.GetAllAsync()).Select(x => new SelectOption
            {
                Title = x.Name,
                Value = x.TestId.ToString()
            });
        }
        #endregion
    }
}
