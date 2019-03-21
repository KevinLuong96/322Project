using System;
using System.ComponentModel.DataAnnotations;



namespace _322Api.Models
{
    public enum Roles { Admin, User };

    public class User
    {
        public int Id { get; private set; }
        public readonly Roles role;
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string[] History { get; set; }
    }
}
