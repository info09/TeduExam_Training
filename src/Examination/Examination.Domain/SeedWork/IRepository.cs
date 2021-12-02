namespace Examination.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork unitOfWork { get; }
    }
}
