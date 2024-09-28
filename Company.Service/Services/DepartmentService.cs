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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Department department)
        {
            _unitOfWork.departmentRepository.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
            _unitOfWork.departmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            return _unitOfWork.departmentRepository.GetAll()/*.Where(x=>x.IsDeleted != true)*/;//soft delete
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var dept = _unitOfWork.departmentRepository.GetById(id.Value);
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

            _unitOfWork.departmentRepository.Update(dept);
            _unitOfWork.Complete();
        }
    }
}
