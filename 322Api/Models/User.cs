using System.ComponentModel.DataAnnotations;

namespace _322Api.Models
{
    public enum Roles { User, Admin };

    public class User
    {
        public int Id { get; private set; }
        public Roles Role { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string[] History { get; set; }
    }
}
