using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Infrastructure;
using api.Data.Repositories;
using api.Models;
using api.Models.Dtos;

namespace api.Services
{
    public interface IEmployeeQualificationService
    {
        void Add(EmployeeQualification model);

        void Update(EmployeeQualification model);

        void Delete(EmployeeQualification enity);

        Task<List<EmployeeQualification>> GetAll();

        Task<ListResultDto<EmployeeQualification>> GetAllPaging(int page, int pageSize);

        ValueTask<EmployeeQualification> GetById(int id);

        void SaveChanges();
    }
    public class EmployeeQualificationService: IEmployeeQualificationService
    {
        IEmployeeQualificationRepository _employeeQualificationRepository;
        IUnitOfWork _unitOfWork;

        public EmployeeQualificationService(IEmployeeQualificationRepository employeeQualificationRepository, IUnitOfWork unitOfWork)
        {
            this._employeeQualificationRepository = employeeQualificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(EmployeeQualification model)
        {
            _employeeQualificationRepository.Add(model);
        }

        public void Delete(EmployeeQualification enity)
        {
            _employeeQualificationRepository.Delete(enity);
        }

        public async Task<List<EmployeeQualification>> GetAll()
        {
            return await _employeeQualificationRepository.GetAll();
        }

        public async Task<ListResultDto<EmployeeQualification>> GetAllPaging(int page, int pageSize)
        {
            var totalRow = await  _employeeQualificationRepository.Count();
            var items = await _employeeQualificationRepository.GetMultiPaging(null, page, pageSize);
            return new ListResultDto<EmployeeQualification>()
            {
                Total = totalRow,
                Items = items,
            };
        }

        public async ValueTask<EmployeeQualification> GetById(int id)
        {
            return await _employeeQualificationRepository.GetSingleById(id);
        }
        
        public void Update(EmployeeQualification model)
        {
            _employeeQualificationRepository.Update(model);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}