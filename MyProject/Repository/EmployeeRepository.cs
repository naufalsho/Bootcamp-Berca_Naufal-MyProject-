using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository.Interface;
using MyProject.ViewModel;

namespace MyProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.Include(e => e.Department).ToList();
           
        }

        public Employee Get(string NIK)
        {
            //return myContext.Employees.Find(NIK);
            //return mycontext.employees.firstordefault(e => e.nik == nik);
            //return mycontext.employees.where(e => e.nik == nik).firstordefault();
            //return myContext.Employees.Where(e => e.NIK == NIK).SingleOrDefault();
            return myContext.Employees.Include(e => e.Department).SingleOrDefault(e => e.NIK == NIK);
        }

        public int Insert(Employee employee)
        {
            //myContext.Employees.Add(employee);
            //var save = myContext.SaveChanges();
            //return save;
            myContext.Entry(employee).State = EntityState.Added;
            var save = myContext.SaveChanges();
            return save;
        }
        public int Delete(string NIK)
        {
            Employee employee = this.Get(NIK);
            myContext.Employees.Remove(employee);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public int Update(Employee employee)
        {

            //var put = myContext.Employees.FirstOrDefault(n => n.NIK == NIK);
            //if (put != null)
            //{
            //    put.FirstName = employee.FirstName;
            //    put.LastName = employee.LastName;
            //    put.Phone = employee.Phone;
            //    put.BirthDate = employee.BirthDate;
            //    put.Salary = employee.Salary;
            //    put.Email = employee.Email;
            //    put.Gender = employee.Gender;
            //    put.Department.Name = employee.Department.Name;
            //}

            myContext.Attach(employee);
            myContext.Entry(employee).State = EntityState.Unchanged;
            var getId = myContext.Employees.Find(employee.NIK);
            myContext.Entry(getId).State = EntityState.Modified;

            var update = myContext.SaveChanges();
            return update;
        }

        public bool IsExistsNIK(string nik)
        {
            
            return myContext.Employees.Any(e => e.NIK == nik);
        }
        public bool IsExistsPhone(string phone)
        {
            return myContext.Employees.Any(e => e.Phone == phone);
        }

        public bool IsExistsEmail(string email)
        {
            return myContext.Employees.Any(e => e.Email == email);
        }

        public bool IsIdValid(int id)
        {
            return myContext.Departments.Any(d => d.Id == id);
        }

        //public Employee GetLatestEmployee()
        //{
        //    var latestEmployee = myContext.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
        //    Console.WriteLine(latestEmployee);
        //    return latestEmployee;
        //}
        public string GenerateNewNik()
        {
            var getGenerateNewNik = DateTime.Now.ToString("ddMMyy") + (myContext.Employees.Count() + 1).ToString("D3");
            return getGenerateNewNik;

            //// Get today date DDMMYYYY format
            //string today = DateTime.Now.ToString("ddMMyyyy");

            //// Get the latest NIK from the repository
            //Employee latestEmployee = GetLatestEmployee();

            //// Generate new NIK by incrementing the last 3 digits of the latest NIK
            //int newSeq = 1;
            //if (latestEmployee != null)
            //{
            //    string latestNik = latestEmployee.NIK;
            //    string latestSeqStr = latestNik.Substring(8);
            //    int latestSeq;
            //    if (int.TryParse(latestSeqStr, out latestSeq))
            //    {
            //        newSeq = latestSeq + 1;
            //    }
            //}

            //string newSeqStr = newSeq.ToString().PadLeft(3, '0');
            //return today + newSeqStr;
        }

        public IEnumerable<Employee> GetAllEmployeeDept()
        {
            return myContext.Employees.Include(e => e.Department);
        }

        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee
            {
                NIK = GenerateNewNik(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = registerVM.Gender,
                Department = new Department {Id = registerVM.DepartmentId}
            };
            myContext.Entry(employee).State = EntityState.Added;
            Account account = new Account
            {
                NIK = employee.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
            };
            myContext.Accounts.Add(account);


            return myContext.SaveChanges();
         
        }
    }
}
