using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Infrastructure;
using api.Data.Repositories;
using api.Models;
using api.Models.Dtos;
using api.Models.Enums;

namespace api.Services
{
    public interface IEmployeeService
    {
        void Add(Employee model);

        void Update(Employee model);

        void Delete(Employee enity);

        Task<List<Employee>> GetAll();

        Task<ListResultDto<Employee>> GetAllPaging(string txtFirstOrLastName, string orderByField,
            string sortOrder, int page, int pageSize);

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

        public async Task<ListResultDto<Employee>> GetAllPaging(string txtFirstOrLastName, string orderByField,
            string sortOrder, int page, int pageSize)
        {
            txtFirstOrLastName = txtFirstOrLastName.ToLower();
            var totalRow = await _employeeRepository.Count();

            var orderClause = new IOrderByClause<Employee>[] {
                new OrderByClause<Employee, int>(one=> one.Id, SortDirection.Ascending)
            };

            if (sortOrder == "Asc") {
                if (orderByField == "FirstName") {
                    orderClause = new IOrderByClause<Employee>[] {
                            new OrderByClause<Employee, string>(one=> one.FirstName, SortDirection.Ascending)
                        };
                }
                else if (orderByField == "LastName") 
                {
                    orderClause = new IOrderByClause<Employee>[] {
                                                new OrderByClause<Employee, string>(one=> one.LastName, SortDirection.Ascending)
                                            };
                }
            }
            else
            {
                if (sortOrder == "Desc") {
                    if (orderByField == "FirstName") {
                        orderClause = new IOrderByClause<Employee>[] {
                                new OrderByClause<Employee, string>(one=> one.FirstName, SortDirection.Descending)
                            };
                    }
                    else if (orderByField == "LastName") 
                    {
                        orderClause = new IOrderByClause<Employee>[] {
                                                    new OrderByClause<Employee, string>(one=> one.LastName, SortDirection.Descending)
                                                };
                    }
                }
            }

            var items = await _employeeRepository.GetMultiSortingPaging(
                x => x.FirstName.ToLower().Contains(txtFirstOrLastName) || x.LastName.ToLower().Contains(txtFirstOrLastName),
                orderClause, 
                page, pageSize, null);

            if (string.IsNullOrEmpty(txtFirstOrLastName) || txtFirstOrLastName == "null") {
                items = await _employeeRepository.GetMultiSortingPaging(
                    null,
                    orderClause, 
                    page, pageSize, null);

            }
            return new ListResultDto<Employee>()
            {
                Total = totalRow,
                Items = items,
            };
        }

        public async ValueTask<Employee> GetById(int id)
        {
            return await _employeeRepository.GetSingleByCondition(x => x.Id == id, new string[] { "EmployeeQualification" });
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