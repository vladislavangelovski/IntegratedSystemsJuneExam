using Domain.Domain_Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeServiceImpl(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void CreateEmployee(Employee employee)
        {
            _employeeRepository.Insert(employee);
        }

        public void DeleteEmployee(Guid id)
        {
            var employee = _employeeRepository.Get(id);
            _employeeRepository.Delete(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll().ToList();
        }

        public Employee GetEmployee(Guid id)
        {
            return _employeeRepository.Get(id);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }
    }
}
