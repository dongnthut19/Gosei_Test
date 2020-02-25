using api.Models;

namespace api.Data.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private EmployeeQualificationContext _dbContext;

        public UnitOfWork(EmployeeQualificationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}