using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Common.Models;
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

        public async Task<ServiceResult<SectionForDetails>> CreateSectionAsync(SectionForCreating section)
        {
            var entity = _mapper.Map<Section>(section);
            var result = await _sectionsRepository.CreateAsync(entity);

            return await GetSectionAsync(entity.SectionId);
        }

        public async Task<ServiceResult> DeleteSectionAsync(Guid sectionId)
        {
            await _sectionsRepository.DeleteAsync(sectionId);
            return new ServiceResult();
        }

        public async Task<ServiceResult> EditSectionAsync(SectionForEditing section)
        {
            var entity = _mapper.Map<Section>(section);

            await _sectionsRepository.UpdateAsync(entity);

            return new ServiceResult();
        }

        public async Task<ServiceResult<SectionForDetails>> GetSectionAsync(Guid sectionId)
        {
            var section = await _sectionsRepository.GetAsync(sectionId);

            if (section == null) 
                return new ServiceNotFoundResult<SectionForDetails>(sectionId.ToString());

            return await Task.FromResult(new ServiceResult<SectionForDetails>(_mapper.Map<SectionForDetails>(section)));
        }

        public async Task<ServiceResult<GetPaginationResult<SectionForList>>> GetSectionsAsync(GetSectionsParams request)
        {
            var sections = (await _sectionsRepository.GetAsync(x => x.TestId == request.TestId, request.Skip, request.Take)).Select(section => _mapper.Map<SectionForList>(section));
            var count = await _sectionsRepository.CountAsync(x => x.TestId == request.TestId);
            var result = new GetPaginationResult<SectionForList>
            {
                Data = sections.ToList(),
                Page = request.Page,
                Take = request.Take,
                TotalPage = count
            };

            return new ServiceResult<GetPaginationResult<SectionForList>>(result);
        }
    }
}
