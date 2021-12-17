using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Examination.Application.Extensions;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Exams;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Examination.Application.Commands.V1.Exams.CreateExam
{
    public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, ApiResult<ExamDto>>
    {
        private readonly IExamRepository _examRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateExamCommandHandler(IExamRepository examRepository, ICategoryRepository categoryRepository, IQuestionRepository questionRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _examRepository = examRepository;
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResult<ExamDto>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var question = new List<Question>();
            if (request.AutoGenerateQuestion)
            {
                question = await _questionRepository.GetRandomQuestionsForExamAsync(request.CategoryId, request.Level,
                    request.NumberOfQuestions);
            }
            else
            {
                question = _mapper.Map<List<QuestionDto>, List<Question>>(request.Questions);
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            var currentUserId = _httpContextAccessor.GetUserId();
            var itemToAdd = new Exam(name: request.Name, shortDesc: request.ShortDesc, content: request.Content,
                numberOfQuestions: request.NumberOfQuestions,
                numberOfQuestionCorrectForPass: request.NumberOfQuestionCorrectForPass,
                durationInMinutes: request.DurationInMinutes, questions: question, level: request.Level,
                ownerUserId: currentUserId, isTimeRestricted: request.IsTimeRestricted, categoryId: request.CategoryId,
                categoryName: category.Name);

            await _examRepository.InsertAsync(itemToAdd);
            var result = _mapper.Map<Exam, ExamDto>(itemToAdd);
            return new ApiSuccessResult<ExamDto>((int) HttpStatusCode.OK, result);
        }
    }
}