using System;
using System.Collections.Generic;

namespace api.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public List<EmployeeQualificationDto> EmployeeQualification{get;set;}
    }
}