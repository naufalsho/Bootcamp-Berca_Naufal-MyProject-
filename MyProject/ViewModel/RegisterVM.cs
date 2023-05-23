using MyProject.Models;

namespace MyProject.ViewModel
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        //format yyyy-mm-dd
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string Password { get; set; }
    }
}
