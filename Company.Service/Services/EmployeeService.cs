using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
using Company.Service.Helper;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            // Manaual Mapping
            //Employee employee = new Employee
            //{
            //    Name = employeeDto.Name,
            //    Age = employeeDto.Age,
            //    Email = employeeDto.Email,
            //    Address = employeeDto.Address,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Salary = employeeDto.Salary,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageUrl = employeeDto.ImageUrl,
            //    PhoneNumber = employeeDto.PhoneNumber

            //};
            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "images");
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            //Employee employee = new Employee
            //{
            //    Name = employeeDto.Name,
            //    Age = employeeDto.Age,
            //    Email = employeeDto.Email,
            //    Address = employeeDto.Address,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Salary = employeeDto.Salary,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageUrl = employeeDto.ImageUrl,
            //    PhoneNumber = employeeDto.PhoneNumber

            //};

            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll() 
        {
            var emp = _unitOfWork.employeeRepository.GetAll().ToList();
            //var mappedEmp = emp.Select(x => new EmployeeDto
            //{
            //    Name = x.Name,
            //    Age = x.Age,
            //    Email = x.Email,
            //    Address = x.Address,
            //    DepartmentId = x.DepartmentId,
            //    Salary = x.Salary,
            //    HiringDate = x.HiringDate,
            //    ImageUrl = x.ImageUrl,
            //    PhoneNumber = x.PhoneNumber
            //});
            IEnumerable<EmployeeDto> mappedEmp = _mapper.Map<IEnumerable<EmployeeDto>>(emp);
            return mappedEmp;
        }

        public EmployeeDto GetById(int? id)
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
            //EmployeeDto employeeDto = new EmployeeDto
            //{
            //    Name = Emp.Name,
            //    Age = Emp.Age,
            //    Email = Emp.Email,
            //    Address = Emp.Address,
            //    DepartmentId = Emp.DepartmentId,
            //    Salary = Emp.Salary,
            //    HiringDate = Emp.HiringDate,
            //    ImageUrl = Emp.ImageUrl,
            //    PhoneNumber = Emp.PhoneNumber

            //};
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(Emp);
            return employeeDto;
        }

        //public IEnumerable<EmployeeDto> GetEmployeeByAddress(string address)
        //{
        //    var Emp = _unitOfWork.employeeRepository.GetEmployeeByAddress(address);
        //    //var mappedEmp = Emp.Select(x => new EmployeeDto
        //    //{
        //    //    Name = x.Name,
        //    //    Age = x.Age,
        //    //    Email = x.Email,
        //    //    Address = x.Address,
        //    //    DepartmentId = x.DepartmentId,
        //    //    Salary = x.Salary,
        //    //    HiringDate = x.HiringDate,
        //    //    ImageUrl = x.ImageUrl,
        //    //    PhoneNumber = x.PhoneNumber
        //    //});
        //    IEnumerable<EmployeeDto> mappedEmp = _mapper.Map<IEnumerable<EmployeeDto>>(Emp);
        //    return mappedEmp;
        //}

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
            var Emp = _unitOfWork.employeeRepository.GetEmployeeByName(name);
            //var mappedEmp = Emp.Select(x => new EmployeeDto
            //{
            //    Name = x.Name,
            //    Age = x.Age,
            //    Email = x.Email,
            //    Address = x.Address,
            //    DepartmentId = x.DepartmentId,
            //    Salary = x.Salary,
            //    HiringDate = x.HiringDate,
            //    ImageUrl = x.ImageUrl,
            //    PhoneNumber = x.PhoneNumber
            //});
            IEnumerable<EmployeeDto> mappedEmp = _mapper.Map<IEnumerable<EmployeeDto>>(Emp);
            return mappedEmp;
        }

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
