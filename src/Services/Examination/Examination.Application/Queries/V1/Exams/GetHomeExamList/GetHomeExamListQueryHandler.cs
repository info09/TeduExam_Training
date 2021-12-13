using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Shared.Exams;
using MediatR;
using MongoDB.Driver;

namespace Examination.Application.Queries.V1.Exams.GetHomeExamList
{
    public class GetHomeExamListQueryHandler : IRequestHandler<GetHomeExamListQuery, IEnumerable<ExamDto>>
    {
        private readonly IExamRepository _repository;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly IMapper _mapper;

        public GetHomeExamListQueryHandler(IExamRepository repository, IClientSessionHandle clientSessionHandle, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _clientSessionHandle = clientSessionHandle ?? throw new ArgumentNullException(nameof(_clientSessionHandle));
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExamDto>> Handle(GetHomeExamListQuery request, CancellationToken cancellationToken)
        {
            var exam = await _repository.GetExamListAsync();
            var examDto = _mapper.Map<IEnumerable<ExamDto>>(exam);
            return examDto;
        }
    }
}
