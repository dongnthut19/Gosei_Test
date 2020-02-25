using System;

namespace api.Models.Dtos
{
    public class EmployeeQualificationDto
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int QualificationId { get; set; }
        public string Institution { get; set; }
        public string City { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Note { get; set; }
    }
}