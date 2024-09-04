using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(Guid id);
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Guid id);
        void UpdateEmployee(Employee employee);
    }
}
