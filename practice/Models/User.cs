using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required (ErrorMessage ="Retype the password here")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="write the same password as the one above.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}
