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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // Constructor Injection
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeeByAddress(string address)
          => _context.Employee.Where(e => e.Address.Trim().ToLower().Contains(address.Trim().ToLower())).ToList();

        public IEnumerable<Employee> GetEmployeeByName(string name)
        {
            string handelName = name.Trim().ToLower();
            var result = _context.Employee.Where(e =>
            e.Name.Trim().ToLower() == handelName ||
            e.Email.Trim().ToLower() == handelName ||
            e.PhoneNumber.Trim().ToLower() == handelName).ToList();
            return result;
        }

    }
}
