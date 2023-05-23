using MyProject.Models;

namespace MyProject.Repository.Interface
{
    public interface IDepartmentRepository
    {
        //GET All Data SELECT *
        IEnumerable<Department> Get();
        //Get by Id
        Department Get(int Id);
        int Insert(Department department);
        int Update(Department department);
        int Delete(int Id);
    }
}
