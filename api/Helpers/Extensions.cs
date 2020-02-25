using api.Models;
using api.Models.Dtos;
using Microsoft.AspNetCore.Http;

namespace api.Helpers
{
    public static class Extensions
    {
        public static void UpdateEmployee(this Employee model, EmployeeDto modelVm)
        {
            model.Id = modelVm.Id;
            model.FirstName = modelVm.FirstName;
            model.MiddleName = modelVm.MiddleName;
            model.LastName = modelVm.LastName;
            model.BirthDate = modelVm.BirthDate;
            model.Gender = modelVm.Gender;
            model.Email = modelVm.Email;
            model.Note = modelVm.Note;
        }

        public static void UpdateQualification(this Qualification model, QualificationDto modelVm)
        {
            model.Id = modelVm.Id;
            model.Name = modelVm.Name;
            model.Code = modelVm.Code;
        }

        public static void UpdateEmployeeQualification(this EmployeeQualification model, EmployeeQualificationDto modelVm)
        {
            model.Id = modelVm.Id;
            model.EmployeeId = modelVm.EmployeeId;
            model.QualificationId = modelVm.QualificationId;
            model.Institution = modelVm.Institution;
            model.City = modelVm.City;
            model.ValidFrom = modelVm.ValidFrom;
            model.ValidTo = modelVm.ValidTo;
            model.Note = modelVm.Note;
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}