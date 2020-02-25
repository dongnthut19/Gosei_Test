using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeQualification = new HashSet<EmployeeQualification>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        public virtual ICollection<EmployeeQualification> EmployeeQualification { get; set; }
    }
}
