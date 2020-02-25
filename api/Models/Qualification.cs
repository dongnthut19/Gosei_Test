using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            EmployeeQualification = new HashSet<EmployeeQualification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<EmployeeQualification> EmployeeQualification { get; set; }
    }
}
