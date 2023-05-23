using MyProject.Models;
using MyProject.ViewModel;

namespace MyProject.Repository.Interface
{
    public interface IAccountRepository
    {
        //GET All Data SELECT *
        IEnumerable<Account> Get();
        //Account Login(string email);
        Account Login(LoginVM loginVM);
    }
}
