using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Infrastructure;
using api.Data.Repositories;
using api.Models;
using api.Models.Dtos;

namespace api.Services
{
    public interface IQualificationService
    {
        void Add(Qualification model);
        void SaveChanges();
    }
    public class QualificationService: IQualificationService
    {
        IQualificationRepository _qualificationRepository;
        IUnitOfWork _unitOfWork;

        public QualificationService(IQualificationRepository qualificationRepository, IUnitOfWork unitOfWork)
        {
            this._qualificationRepository = qualificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Qualification model)
        {
            _qualificationRepository.Add(model);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}