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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Add(Department department)
        {
            _departmentRepository.Add(department);
        }

        public void Delete(Department department)
        {
            _departmentRepository.Delete(department);
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var dept = _departmentRepository.GetById(id.Value);
            if (dept is null)
            {
                return null;
            }
            return dept;
        }

        public void Update(Department department)
        {
            var dept = GetById(department.Id);
            if (dept.Name != department.Name)
            {
                if (GetAll().Any(x => x.Name == department.Name))
                {
                    throw new Exception("DuplicationDepartmentsName");
                }
            }
            dept.Code = department.Code;
            dept.Name = department.Name;

            _departmentRepository.Update(dept);
        }
    }
}
