using Examination.Domain.SeedWork;
using System.Threading.Tasks;

namespace Examination.Domain.AggregateModels.UserAggregate
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByIdAsync(string externalId);
    }
}
