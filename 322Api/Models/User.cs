using System;
using System.ComponentModel.DataAnnotations;

namespace _322Api.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
