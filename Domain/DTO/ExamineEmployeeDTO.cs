using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ExamineEmployeeDTO
    {
        public List<Polyclinic>? Polyclinics { get; set; }
        public Guid? PolyclinicId { get; set; }
        public string? Description { get; set; }
        public Company? Company{ get; set; }
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
