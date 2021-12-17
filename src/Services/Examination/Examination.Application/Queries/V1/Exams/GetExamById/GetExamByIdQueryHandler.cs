using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Queries.V1.Exams.GetExamById
{
    public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, ApiResult<ExamDto>>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public GetExamByIdQueryHandler(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<ExamDto>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _examRepository.GetExamByIdAsync(request.Id);

            var data = _mapper.Map<Exam, ExamDto> (result);

            return new ApiSuccessResult<ExamDto>((int) HttpStatusCode.OK, data);
        }
    }
}
