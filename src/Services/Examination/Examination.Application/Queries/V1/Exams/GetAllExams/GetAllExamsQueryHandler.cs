using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Queries.V1.Exams.GetAllExams
{
    public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsQuery, ApiResult<IEnumerable<ExamDto>>>
    {
        private readonly IExamRepository _repository;
        private readonly IMapper _mapper;

        public GetAllExamsQueryHandler(IExamRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<ExamDto>>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
            var exam = await _repository.GetAllExamListAsync();
            var examDto = _mapper.Map<IEnumerable<ExamDto>>(exam);
            return new ApiSuccessResult<IEnumerable<ExamDto>>((int)HttpStatusCode.OK, examDto);
        }
    }
}
