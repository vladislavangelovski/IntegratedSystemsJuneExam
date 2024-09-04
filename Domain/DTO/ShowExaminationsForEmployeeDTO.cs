using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain_Models;

namespace Domain.DTO
{
    public class ShowExaminationsForEmployeeDTO
    {
        public List<HealthExamination>? Examinations { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
