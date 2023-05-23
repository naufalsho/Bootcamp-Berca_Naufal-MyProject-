using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    //untuk ganti nama dari table database
    [Table("TB_Employee")]
    public class Employee
    {
        //Id, EmployeeId, "Employee Id" << definisi pm-key otomatis tanpa anotation
        [Key] //anotation untuk mendefinisikan Id yang memiliki nama field beda dari Id
        public string? NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        //format yyyy-mm-dd
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Department? Department { get; set; } //otomatis kebuat dengan format nama Obj+modelFK = Department+Id 

        //// pemberian anotasi agar dapat dirubah nama dari tablenya
        //[ForeignKey("department")]
        //public int? department_id { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
