using MyProject.Models;
using MyProject.ViewModel;

namespace MyProject.Repository.Interface
{
    public interface IEmployeeRepository
    {
        //GET All Data SELECT *
        IEnumerable<Employee> Get();

        //GET All by NIK
        Employee Get(string NIK);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);
        //GET ALL Data (Employee Include Department), NIK, FullName(Firstname + LastName), NamaDepartment
        IEnumerable<Employee> GetAllEmployeeDept();
        int Register(RegisterVM registerVM);
    }
}
