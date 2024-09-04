using Domain.Domain_Models;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ExaminationServiceImpl : IExaminationService
    {
        private readonly IRepository<HealthExamination> _examinationRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Polyclinic> _polyclinicRepository;
        private DbSet<HealthExamination> _examinations;

        public ExaminationServiceImpl(IRepository<HealthExamination> examinationRepository, IRepository<Employee> employeeRepository, IRepository<Polyclinic> polyclinicRepository)
        {
            _examinationRepository = examinationRepository;
            _employeeRepository = employeeRepository;
            _polyclinicRepository = polyclinicRepository;
        }

        public void ExamineEmployee(ExamineEmployeeDTO examineEmployeeDTO)
        {
            HealthExamination healthExamination = new HealthExamination()
            {
                Description = examineEmployeeDTO.Description,
                DateTaken = DateTime.Now,
                EmployeeId = examineEmployeeDTO.EmployeeId,
                Employee = _employeeRepository.Get(examineEmployeeDTO.EmployeeId),
                PolyclinicId = (Guid)examineEmployeeDTO.PolyclinicId,
                Polyclinic = _polyclinicRepository.Get(examineEmployeeDTO.PolyclinicId)
            };
            _examinationRepository.Insert(healthExamination);
        }

        public HealthExamination GetDetailsForExamination(BaseEntity entity)
        {
            return this._examinations.Include(x => x.Polyclinic).Include(x => x.Employee).SingleOrDefaultAsync(x => x.Id == entity.Id).Result;
        }

        public List<HealthExamination> ShowExaminationsForEmployee(Guid id)
        {
            return _examinationRepository.GetAll().Where(e => e.EmployeeId == id).ToList();
        }

        public List<HealthExamination> ShowExaminationsForPolyclinic(Guid id)
        {
            return _examinationRepository.GetAll().Where(e => e.PolyclinicId == id).ToList();
        }
    }
}
