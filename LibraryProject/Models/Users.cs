using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
