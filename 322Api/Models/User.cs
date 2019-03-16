using System;
using System.ComponentModel.DataAnnotations;



namespace _322Api.Models
{
    public enum Roles { Admin, User };

    public class User
    {
        public int Id { get; private set; }
        public readonly Roles role;
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] History { get; set; }
    }
}
