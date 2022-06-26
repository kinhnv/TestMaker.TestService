using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.TestService.Domain.Models.Section;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Sections;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class SectionsService : ISectionsService
    {
        private readonly ISectionsRepository _sectionsRepository;
        private readonly IMapper _mapper;

        public SectionsService(ISectionsRepository sectionsRepository, IMapper mapper)
        {
            _sectionsRepository = sectionsRepository;
            _mapper = mapper;
        }

        public async Task<SectionForDetails> CreateSectionAsync(SectionForCreating section)
        {
            var entity = _mapper.Map<Section>(section);
            var result = await _sectionsRepository.CreateAsync(entity);

            if (result)
                return await GetSectionAsync(entity.SectionId);
            else
                return null;
        }

        public async Task<bool> DeleteSectionAsync(Guid sectionId)
        {
            return  await _sectionsRepository.DeleteAsync(sectionId);
        }

        public async Task<bool> EditSectionAsync(SectionForEditing section)
        {
            var entity = _mapper.Map<Section>(section);

            return  await _sectionsRepository.UpdateAsync(entity);
        }

        public async Task<SectionForDetails> GetSectionAsync(Guid sectionId)
        {
            var section = await _sectionsRepository.GetAsync(sectionId);

            return await Task.FromResult(_mapper.Map<SectionForDetails>(section));
        }

        public async Task<IEnumerable<SectionForList>> GetSectionsAsync(GetQuestionsRequest request)
        {
            var filter = new SectionsFilter
            {
                TestId = request?.TestId ?? null,
            };
            var sections = (await _sectionsRepository.GetSectionsAsync(filter)).Select(section => _mapper.Map<SectionForList>(section));
            return await Task.FromResult(sections);
        }
    }
}
