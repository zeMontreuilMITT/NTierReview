using NTierReview.Models;
using NTierReview.Data;

namespace NTierReview.DAL
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private NTierReviewContext _context;
        public EmployeeRepository(NTierReviewContext context)
        {
            _context = context;
        }

        public void Add(Employee entity)
        {
            _context.Employee.Add(entity);
        }

        public void Delete(Employee entity)
        {
            _context.Employee.Remove(entity);
        }

        public Employee Get(int id)
        {
            return _context.Employee.First(e => e.Id == id);
        }

        public Employee Get(Func<Employee, bool> predicate)
        {
            return _context.Employee.First(predicate);
        }

        public ICollection<Employee> GetAll()
        {
            return _context.Employee.ToList();
        }

        public ICollection<Employee> GetAllWhere(Func<Employee, bool> predicate)
        {
            return _context.Employee.Where(predicate).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Employee entity)
        {
            _context.Employee.Update(entity);
        }
    }
}
