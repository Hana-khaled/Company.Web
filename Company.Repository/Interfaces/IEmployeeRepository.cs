using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<Employee> GetEmployeeByName(string name);
        public IEnumerable<Employee> GetEmployeeByAddress(string address);
    }
}
