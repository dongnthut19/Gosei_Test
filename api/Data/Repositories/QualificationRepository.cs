using api.Data.Infrastructure;
using api.Models;
namespace api.Data.Repositories
{
    public interface IQualificationRepository : IRepository<Qualification>
    {
    }

    public class QualificationRepository : RepositoryBase<Qualification>, IQualificationRepository
    {
        public QualificationRepository(EmployeeQualificationContext context) : base(context)
        {
        }
    }
}