using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Infrastructure;
using api.Data.Repositories;
using api.Models;
using api.Models.Dtos;

namespace api.Services
{
    public interface IEmployeeService
    {
        void Add(Employee model);

        void Update(Employee model);

        void Delete(Employee enity);

        Task<List<Employee>> GetAll();

        Task<ListResultDto<Employee>> GetAllPaging(int page, int pageSize);

        ValueTask<Employee> GetById(int id);

        void SaveChanges();
    }
    public class EmployeeService: IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            this._employeeRepository = employeeRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Employee age)
        {
            _employeeRepository.Add(age);
        }

        public void Delete(Employee enity)
        {
            _employeeRepository.Delete(enity);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _employeeRepository.GetAll();
        }

        public async Task<ListResultDto<Employee>> GetAllPaging(int page, int pageSize)
        {
            var totalRow = await  _employeeRepository.Count();
            var items = await _employeeRepository.GetMultiPaging(null, page, pageSize);
            return new ListResultDto<Employee>()
            {
                Total = totalRow,
                Items = items,
            };
        }

        public async ValueTask<Employee> GetById(int id)
        {
            return await _employeeRepository.GetSingleById(id);
        }
        
        public void Update(Employee model)
        {
            _employeeRepository.Update(model);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}