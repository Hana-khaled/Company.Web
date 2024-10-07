using Company.Data.Models;
using Company.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int? id);
        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto employee);
        void Update(Employee employee);
        void Delete(EmployeeDto employee);
        IEnumerable<EmployeeDto> GetEmployeeByAddress(string address);
        IEnumerable<EmployeeDto> GetEmployeeByName(string name);
    }
}
