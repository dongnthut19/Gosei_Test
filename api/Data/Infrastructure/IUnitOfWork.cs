namespace api.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}