using System;
using System.ComponentModel.DataAnnotations;

namespace _322Api.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
