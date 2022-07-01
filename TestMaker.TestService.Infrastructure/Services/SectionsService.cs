using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models.Section;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Questions;
using TestMaker.TestService.Infrastructure.Repositories.Sections;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class SectionsService : ISectionsService
    {
        private readonly ISectionsRepository _sectionsRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;

        public SectionsService(
            ISectionsRepository sectionsRepository, 
            IQuestionsRepository questionsRepository, 
            IMapper mapper)
        {
            _sectionsRepository = sectionsRepository;
            _questionsRepository = questionsRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SectionForDetails>> CreateSectionAsync(SectionForCreating section)
        {
            var entity = _mapper.Map<Section>(section);

            var result = await _sectionsRepository.CreateAsync(entity);

            return await GetSectionAsync(entity.SectionId);
        }

        public async Task<ServiceResult> DeleteSectionAsync(Guid sectionId)
        {
            var section = await _sectionsRepository.GetAsync(sectionId);
            if (section == null)
            {
                return new ServiceNotFoundResult<Section>(sectionId.ToString());
            }
            var questions = await _questionsRepository.GetAsync(question => question.SectionId == sectionId && question.IsDeleted == false);
            if (questions?.Any() != true)
            {
                section.IsDeleted = true;
            }
            else
            {
                return new ServiceResult("There are some questions is not deleted");
            }
            await EditSectionAsync(_mapper.Map<SectionForEditing>(section));
            return new ServiceResult();
        }

        public async Task<ServiceResult<SectionForDetails>> EditSectionAsync(SectionForEditing section)
        {
            var entity = _mapper.Map<Section>(section);

            var result = await _sectionsRepository.GetAsync(section.SectionId);
            if (result == null || result.IsDeleted == true)
            {
                return new ServiceNotFoundResult<SectionForDetails>(section.SectionId.ToString());
            }

            await _sectionsRepository.UpdateAsync(entity);
            return await GetSectionAsync(entity.SectionId);
        }

        public async Task<ServiceResult<SectionForDetails>> GetSectionAsync(Guid sectionId)
        {
            var section = await _sectionsRepository.GetAsync(sectionId);

            if (section == null) 
                return new ServiceNotFoundResult<SectionForDetails>(sectionId.ToString());

            return await Task.FromResult(new ServiceResult<SectionForDetails>(_mapper.Map<SectionForDetails>(section)));
        }

        public async Task<ServiceResult<GetPaginationResult<SectionForList>>> GetSectionsAsync(GetSectionsParams getSectionsParams)
        {
            Expression<Func<Section, bool>> predicate = x => x.IsDeleted == getSectionsParams.IsDeleted &&
                (getSectionsParams.TestId == null || getSectionsParams.TestId == x.TestId);

            var sections = (await _sectionsRepository.GetAsync(predicate, getSectionsParams.Skip, getSectionsParams.Take)).Select(section => _mapper.Map<SectionForList>(section));
            var count = await _sectionsRepository.CountAsync(predicate);
            var result = new GetPaginationResult<SectionForList>
            {
                Data = sections.ToList(),
                Page = getSectionsParams.Page,
                Take = getSectionsParams.Take,
                TotalPage = count
            };

            return new ServiceResult<GetPaginationResult<SectionForList>>(result);
        }
    }
}
