using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Queries.V1.Questions.GetQuestionsPaging
{
    public class GetQuestionsPagingQuery : IRequest<PagedList<QuestionDto>>
    {
        public string CategoryId { get; set; }

        public string SearchKeyword { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public GetQuestionsPagingQuery(string categoryId, string searchKeyword, int pageSize, int pageIndex)
        {
            CategoryId = categoryId;
            SearchKeyword = searchKeyword;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
}
