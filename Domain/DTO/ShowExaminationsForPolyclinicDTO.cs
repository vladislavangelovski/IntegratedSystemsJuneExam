using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ShowExaminationsForPolyclinicDTO
    {
        public List<HealthExamination>? Examinations { get; set; }
        public Guid PolyclinicId { get; set; }
        public Polyclinic Polyclinic{ get; set; }
    }
}
