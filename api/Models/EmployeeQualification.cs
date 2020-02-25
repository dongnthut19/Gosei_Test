using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class EmployeeQualification
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int QualificationId { get; set; }
        public string Institution { get; set; }
        public string City { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Note { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Qualification Qualification { get; set; }
    }
}
