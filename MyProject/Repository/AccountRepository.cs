using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository.Interface;
using MyProject.ViewModel;

namespace MyProject.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext myContext;
        
        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<Account> Get()
        {
            var get = myContext.Accounts.Include(a => a.Employee).Include(e => e.Employee.Department);
            return get.ToList();
        }

        public bool IsAxistNIK(string NIK)
        {
            return myContext.Employees.Any(e => e.NIK == NIK);
        }

        //public Account Login(string email)
        //{
        //    var get = myContext.Accounts.Include(a => a.Employee).SingleOrDefault(a => a.Employee.Email == email);

        //    return get;
        //}

        public Account Login(LoginVM loginVM)
        {

            //var getAccount = myContext.Accounts.Where(a => a.Employee.Email == loginVM.Email && BCrypt.Net.BCrypt.Verify(loginVM.Password, a.Password) == true).ToList();

            var account =  myContext.Accounts.Include(a => a.Employee).FirstOrDefault(a => a.Employee.Email == loginVM.Email);
            //if (account != null && BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password))
            //{
            //    return account;
            //}

            return account;
        }
    }
}
