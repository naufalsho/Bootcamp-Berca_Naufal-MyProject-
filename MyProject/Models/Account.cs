using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("TB_Account")]
    public class Account
    {
        [Key,ForeignKey("Employee")]
        public string NIK { get; set; }
        public string Password { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
