using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
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
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(DepartmentDto departmentDto)
        {
            //Department department = new Department
            //{
            //    Name = departmentDto.Name,
            //    Code = departmentDto.Code,
            //    CreatedAt = DateTime.Now,
            //    Id = departmentDto.Id
            //};

            Department department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.departmentRepository.Add(department);

            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            //Department department = new Department
            //{
            //    Name = departmentDto.Name,
            //    Code = departmentDto.Code,
            //    CreatedAt = DateTime.Now,
            //    Id = departmentDto.Id
            //};

            Department department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.departmentRepository.Delete(department);

            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.departmentRepository.GetAll().ToList();
            //return departments.Select(x => new DepartmentDto
            //{
            //    Name = x.Name,
            //    Code = x.Code,
            //    Id = x.Id
            //});
            IEnumerable<DepartmentDto> mappedDept = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return mappedDept;
            //return _unitOfWork.departmentRepository.GetAll()/*.Where(x=>x.IsDeleted != true)*/;//soft delete
        }

        public DepartmentDto GetById(int? id)
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
            DepartmentDto mappedDept = _mapper.Map<DepartmentDto>(dept);
            return mappedDept;
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

            _unitOfWork.departmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
