using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.TestService.Domain.Models.Section;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Extensions;
using TestMaker.TestService.Infrastructure.Repositories.Sections;
using TestMaker.TestService.Infrastructure.Services;
using Xunit;

namespace SectionMaker.Business.UnitSections.Services
{
    public class SectionsServicesSection
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly Mock<ISectionsRepository> _mockSectionsRepository;

        private readonly Section _section;
        private readonly SectionForCreating _sectionForCreating;
        private readonly SectionForEditing _sectionForEditing;
        #endregion

        #region Ctrls
        public SectionsServicesSection()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();
            _mockSectionsRepository = new Mock<ISectionsRepository>();

            _section = new()
            {
                SectionId = Guid.NewGuid(),
                Name = "Section 1",
                TestId = Guid.NewGuid()
            };
            _sectionForCreating = new()
            {
                Name = _section.Name,
                TestId = _section.TestId
            };
            _sectionForEditing = new()
            {
                SectionId = _section.SectionId,
                Name = _section.Name,
                TestId = _section.TestId
            };
        }
        #endregion

        #region Sections
        [Fact]
        public async Task GetSectionAsync_ExistedId_Section()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.GetAsync(_section.SectionId)).ReturnsAsync(_section);
            });

            // Run
            var sectionForDetails = await sectionsService.GetSectionAsync(_section.SectionId);

            // Section result
            Assert.Equal(_section.SectionId, sectionForDetails.SectionId);
            Assert.Equal(_section.Name, sectionForDetails.Name);
            Assert.Equal(_section.TestId, sectionForDetails.TestId);

            // Section calling
            _mockSectionsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetSectionAsync_UnexistedId_Null()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(null as Section);
            });

            // Run
            var sectionForDetails = await sectionsService.GetSectionAsync(Guid.NewGuid());

            // Section
            Assert.Null(sectionForDetails);

            // Section calling
            _mockSectionsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetSectionsAsync_NotThings_AllSections()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.GetSectionsAsync(It.IsAny<SectionsFilter>())).ReturnsAsync(new List<Section> { _section });
            });

            // Run
            var sectionForList = await sectionsService.GetSectionsAsync(new GetQuestionsRequest
            {
                TestId = null
            });

            // Section
            Assert.Equal(_section.SectionId, sectionForList.First().SectionId);
            Assert.Equal(_section.Name, sectionForList.First().Name);

            // Section calling
            _mockSectionsRepository.Verify(x => x.GetSectionsAsync(It.IsAny<SectionsFilter>()), Times.Once);
        }

        [Fact]
        public async Task CreateSectionAsync_NewSection_Section()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.CreateAsync(It.IsAny<Section>())).ReturnsAsync(true);
                _mockSectionsRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(_section);
            });

            // Run
            var sectionForDetails = await sectionsService.CreateSectionAsync(_sectionForCreating);

            // Section
            Assert.Equal(_sectionForCreating.Name, sectionForDetails.Name);
            Assert.Equal(_sectionForCreating.TestId, sectionForDetails.TestId);

            // Section calling
            _mockSectionsRepository.Verify(x => x.CreateAsync(It.IsAny<Section>()), Times.Once);
            _mockSectionsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task CreateSectionAsync_NewSection_Null()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.CreateAsync(It.IsAny<Section>())).ReturnsAsync(false);
            });

            // Run
            var sectionForDetails = await sectionsService.CreateSectionAsync(_sectionForCreating);

            // Section
            Assert.Null(sectionForDetails);

            // Section calling
            _mockSectionsRepository.Verify(x => x.CreateAsync(It.IsAny<Section>()), Times.Once);
            _mockSectionsRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DeleteSectionAsync_ExistedId_Success()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            });

            // Run
            var isSuccessed = await sectionsService.DeleteSectionAsync(_section.SectionId);

            // Section
            Assert.True(isSuccessed);

            // Section calling
            _mockSectionsRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSectionAsync_UnexistedId_Failt()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            });

            // Run
            var isSuccessed = await sectionsService.DeleteSectionAsync(_section.SectionId);

            // Section
            Assert.False(isSuccessed);

            // Section calling
            _mockSectionsRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task EditSectionAsync_ExistedSection_Success()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.UpdateAsync(It.Is<Section>(x => x.SectionId == _section.SectionId))).ReturnsAsync(true);
            });

            // Run
            var isSuccessed = await sectionsService.EditSectionAsync(_sectionForEditing);

            // Section
            Assert.True(isSuccessed);

            // Section calling
            _mockSectionsRepository.Verify(x => x.UpdateAsync(It.Is<Section>(x => x.SectionId == _section.SectionId)), Times.Once);
        }

        [Fact]
        public async Task EditSectionAsync_UnexistedSection_Failt()
        {
            // Setup
            ISectionsService sectionsService = CreateSectionsService(() =>
            {
                _mockSectionsRepository.Setup(x => x.UpdateAsync(It.IsAny<Section>())).ReturnsAsync(false);
            });

            // Run
            var isSuccessed = await sectionsService.EditSectionAsync(_sectionForEditing);

            // Section
            Assert.False(isSuccessed);

            // Section calling
            _mockSectionsRepository.Verify(x => x.UpdateAsync(It.IsAny<Section>()), Times.Once);
        }
        #endregion

        #region Supports
        private ISectionsService CreateSectionsService(Action setup)
        {
            _mockSectionsRepository.Reset();
            setup();
            return new SectionsService(_mockSectionsRepository.Object, _mapper);
        }
        #endregion
    }
}