using api.Data.Infrastructure;
using api.Models;
namespace api.Data.Repositories
{
    public interface IEmployeeQualificationRepository : IRepository<EmployeeQualification>
    {
    }
    public class EmployeeQualificationRepository : RepositoryBase<EmployeeQualification>, IEmployeeQualificationRepository
    {
        public EmployeeQualificationRepository(EmployeeQualificationContext context) : base(context)
        {
        }
    }
}