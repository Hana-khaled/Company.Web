using Company.Data.Models;
using Company.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        void Add(DepartmentDto department);
        void Update(Department department);
        void Delete(DepartmentDto department);
        DepartmentDto GetById(int? id);
        IEnumerable<DepartmentDto> GetAll();
    }
}
