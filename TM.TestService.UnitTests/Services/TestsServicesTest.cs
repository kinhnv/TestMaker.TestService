using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.TestService.Domain.Models.Test;
using TM.TestService.Domain.Services;
using TM.TestService.Infrastructure.Entities;
using TM.TestService.Infrastructure.Extensions;
using TM.TestService.Infrastructure.Repositories.Tests;
using TM.TestService.Infrastructure.Services;
using Xunit;

namespace TestMaker.Business.Admin.UnitTests.Services
{
    public class TestsServicesTest
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly Mock<ITestsRepository> _mockTestsRepository;

        private readonly Test _test;
        private readonly TestForCreating _testForCreating;
        private readonly TestForEditing _testForEditing;
        #endregion

        #region Ctrls
        public TestsServicesTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();
            _mockTestsRepository = new Mock<ITestsRepository>();

            _test = new()
            {
                TestId = Guid.NewGuid(),
                Name = "Test 1",
                Description = "Test 1"
            };
            _testForCreating = new()
            {
                Name = _test.Name,
                Description = _test.Description,
            };
            _testForEditing = new()
            {
                TestId = _test.TestId,
                Name = _test.Name,
                Description = _test.Description
            };
        }
        #endregion

        #region Tests
        [Fact]
        public async Task GetTestAsync_ExistedId_Test()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.GetAsync(_test.TestId)).ReturnsAsync(_test);
            });

            // Run
            var testForDetails = await testsService.GetTestAsync(_test.TestId);

            // Test result
            Assert.Equal(_test.TestId, testForDetails.TestId);
            Assert.Equal(_test.Name, testForDetails.Name);
            Assert.Equal(_test.Description, testForDetails.Description);

            // Test calling
            _mockTestsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetTestAsync_UnexistedId_Null()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(null as Test);
            });

            // Run
            var testForDetails = await testsService.GetTestAsync(Guid.NewGuid());

            // Test
            Assert.Null(testForDetails);

            // Test calling
            _mockTestsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetTestsAsync_NotThings_AllTests()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Test> { _test });
            });

            // Run
            var testForList = await testsService.GetTestsAsync();

            // Test
            Assert.Equal(_test.TestId, testForList.First().TestId);
            Assert.Equal(_test.Name, testForList.First().Name);
            Assert.Equal(_test.Description, testForList.First().Description);

            // Test calling
            _mockTestsRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateTestAsync_NewTest_Test()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.CreateAsync(It.IsAny<Test>())).ReturnsAsync(true);
                _mockTestsRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(_test);
            });

            // Run
            var testForDetails = await testsService.CreateTestAsync(_testForCreating);

            // Test
            Assert.Equal(_testForCreating.Name, testForDetails.Name);
            Assert.Equal(_testForCreating.Description, testForDetails.Description);

            // Test calling
            _mockTestsRepository.Verify(x => x.CreateAsync(It.IsAny<Test>()), Times.Once);
            _mockTestsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task CreateTestAsync_NewTest_Null()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.CreateAsync(It.IsAny<Test>())).ReturnsAsync(false);
            });

            // Run
            var testForDetails = await testsService.CreateTestAsync(_testForCreating);

            // Test
            Assert.Null(testForDetails);

            // Test calling
            _mockTestsRepository.Verify(x => x.CreateAsync(It.IsAny<Test>()), Times.Once);
            _mockTestsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTestAsync_ExistedId_Success()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            });

            // Run
            var isSuccessed = await testsService.DeleteTestAsync(_test.TestId);

            // Test
            Assert.True(isSuccessed);

            // Test calling
            _mockTestsRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteTestAsync_UnexistedId_Failt()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            });

            // Run
            var isSuccessed = await testsService.DeleteTestAsync(_test.TestId);

            // Test
            Assert.False(isSuccessed);

            // Test calling
            _mockTestsRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task EditTestAsync_ExistedTest_Success()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.UpdateAsync(It.Is<Test>(x => x.TestId == _test.TestId))).ReturnsAsync(true);
            });

            // Run
            var isSuccessed = await testsService.EditTestAsync(_testForEditing);

            // Test
            Assert.True(isSuccessed);

            // Test calling
            _mockTestsRepository.Verify(x => x.UpdateAsync(It.Is<Test>(x => x.TestId == _test.TestId)), Times.Once);
        }

        [Fact]
        public async Task EditTestAsync_UnexistedTest_Failt()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.UpdateAsync(It.IsAny<Test>())).ReturnsAsync(false);
            });

            // Run
            var isSuccessed = await testsService.EditTestAsync(_testForEditing);

            // Test
            Assert.False(isSuccessed);

            // Test calling
            _mockTestsRepository.Verify(x => x.UpdateAsync(It.IsAny<Test>()), Times.Once);
        }

        [Fact]
        public async Task GetTestsAsSelectOptionsAsync_NoThings_AllOptions()
        {
            // Setup
            ITestsService testsService = CreateTestsService(() =>
            {
                _mockTestsRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Test> { _test });
            });

            // Run
            var selectOptions = await testsService.GetTestsAsSelectOptionsAsync();

            // Test
            Assert.Equal(_test.TestId.ToString(), selectOptions.First().Value);
            Assert.Equal(_test.Name.ToString(), selectOptions.First().Title);

            // Test calling
            _mockTestsRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }
        #endregion

        #region Supports
        private ITestsService CreateTestsService(Action setup)
        {
            _mockTestsRepository.Reset();
            setup();
            return new TestsService(_mockTestsRepository.Object, _mapper);
        }
        #endregion
    }
}
