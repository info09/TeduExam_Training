using AutoMapper;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Exams.UpdateExam
{
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, ApiResult<bool>>
    {
        private readonly IExamRepository _examRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public UpdateExamCommandHandler(IExamRepository examRepository, ICategoryRepository categoryRepository, IQuestionRepository questionRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<bool>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _examRepository.GetExamByIdAsync(request.Id);
            if (itemToUpdate == null)
            {
                return new ApiResult<bool>((int)HttpStatusCode.BadRequest, false, $"Item is not found {request.Id}");
            }

            if (request.NumberOfQuestions != itemToUpdate.NumberOfQuestions)
            {
                List<Question> questions;
                if (request.AutoGenerateQuestion)
                {
                    questions = await _questionRepository.GetRandomQuestionsForExamAsync(request.CategoryId,
                        request.Level, request.NumberOfQuestions);
                }
                else
                {
                    questions = _mapper.Map<List<QuestionDto>, List<Question>>(request.Questions);
                }

                itemToUpdate.Questions = questions;
            }

            if (itemToUpdate.Questions.Count < request.NumberOfQuestionCorrectForPass)
            {
                return new ApiResult<bool>((int)HttpStatusCode.BadRequest, false,
                    "Number of question is not enough for required");
            }

            if (request.CategoryId != itemToUpdate.CategoryId)
            {
                var categpry = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
                itemToUpdate.CategoryId = request.CategoryId;
                itemToUpdate.CategoryName = categpry.Name;
            }

            itemToUpdate.Name = request.Name;
            itemToUpdate.Content = request.Content;
            itemToUpdate.ShortDesc = request.ShortDesc;
            itemToUpdate.DurationInMinutes = request.DurationInMinutes;
            itemToUpdate.Level = request.Level;
            itemToUpdate.IsTimeRestricted = request.IsTimeRestricted;
            itemToUpdate.NumberOfQuestionCorrectForPass = request.NumberOfQuestionCorrectForPass;
            itemToUpdate.NumberOfQuestions = request.NumberOfQuestions;

            await _examRepository.UpdateAsync(itemToUpdate);
            return new ApiSuccessResult<bool>((int)HttpStatusCode.OK, true, "Update successful");
        }
    }
}