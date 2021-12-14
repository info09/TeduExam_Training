using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Queries.V1.Questions.GetQuestionsPaging
{
    public class GetQuestionsPagingQuery : IRequest<PagedList<QuestionDto>>
    {
        public string SearchKeyword { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public GetQuestionsPagingQuery(string searchKeyword, int pageSize, int pageIndex)
        {
            SearchKeyword = searchKeyword;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
}
