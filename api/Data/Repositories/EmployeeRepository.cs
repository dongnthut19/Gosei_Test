using api.Data.Infrastructure;
using api.Models;

namespace api.Data.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeQualificationContext context) : base(context)
        {
        }
    }
}