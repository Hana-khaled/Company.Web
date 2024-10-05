using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Employee employee)
        {
            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(Employee employee)
        {
            _unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
        => _unitOfWork.employeeRepository.GetAll().ToList();

        public Employee GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var Emp = _unitOfWork.employeeRepository.GetById(id.Value);
            if(Emp is null)
            {
                return null;
            }
            return Emp;
        }

        public IEnumerable<Employee> GetEmployeeByAddress(string address)
        => _unitOfWork.employeeRepository.GetEmployeeByAddress(address);

        public IEnumerable<Employee> GetEmployeeByName(string name)
        => _unitOfWork.employeeRepository.GetEmployeeByName(name);

        public void Update(Employee employee)
        {
            var Emp = GetById(employee.Id);
            if(Emp.Name != employee.Name)
            {
                if (GetAll().Any(x => x.Name == employee.Name))
                {
                    throw new Exception("DuplicationEmployeesName");
                }
            }
            _unitOfWork.employeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
