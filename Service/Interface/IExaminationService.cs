using Domain.Domain_Models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IExaminationService
    {
        List<HealthExamination> ShowExaminationsForEmployee(Guid id);
        void ExamineEmployee(ExamineEmployeeDTO examineEmployeeDTO);
        List<HealthExamination> ShowExaminationsForPolyclinic(Guid id);
        public HealthExamination GetDetailsForExamination(BaseEntity entity);
    }
}
