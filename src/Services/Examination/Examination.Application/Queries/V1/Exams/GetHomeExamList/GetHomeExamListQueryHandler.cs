using AutoMapper;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Queries.V1.Exams.GetHomeExamList
{
    public class GetHomeExamListQueryHandler : IRequestHandler<GetHomeExamListQuery, ApiResult<IEnumerable<ExamDto>>>
    {
        private readonly IExamRepository _repository;
        private readonly IMapper _mapper;

        public GetHomeExamListQueryHandler(IExamRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<ExamDto>>> Handle(GetHomeExamListQuery request, CancellationToken cancellationToken)
        {
            var exam = await _repository.GetExamListAsync();
            var examDto = _mapper.Map<IEnumerable<ExamDto>>(exam);
            return new ApiSuccessResult<IEnumerable<ExamDto>>(examDto);
        }
    }
}
