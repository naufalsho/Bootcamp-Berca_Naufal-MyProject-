using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository.Interface;

namespace MyProject.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext myContext;

        public DepartmentRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //normal tanpa async
        //public IEnumerable<Department> Get()
        //{
        //    return myContext.Departments.ToList();
        //}

        //pakai async
        public IEnumerable<Department> Get()
        {
            return  myContext.Departments.ToList();
        }
        public Department Get(int Id)
        {
            return myContext.Departments.SingleOrDefault(d => d.Id == Id);
        }

        public int Insert(Department department)
        {
            myContext.Departments.Add(department);
            var save = myContext.SaveChanges();
            return save;
        }

        public int Update(Department department)
        {
            myContext.Departments.Update(department);
            var update = myContext.SaveChanges();
            return update;
        }

        public int Delete(int Id)
        {
            Department department = this.Get(Id);
            myContext.Departments.Remove(department);
            var delete = myContext.SaveChanges();
            return delete;
        }

        
    }
}
