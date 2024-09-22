using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public void Add(Department department)
        => _context.Add(department);

        public void Delete(Department department)
         => _context.Remove(department);

        public IEnumerable<Department> GetAll()
        => _context.Departments.ToList();

        public Department GetById(int id)
        => _context.Departments.FirstOrDefault(d => d.Id == id);

        public void Update(Department department)
         => _context.Update(department);
    }
}
