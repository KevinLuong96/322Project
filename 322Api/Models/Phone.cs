using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _322Api.Models
{
    public class Phone : Device
    {
        public string[] Providers { get; set; }
        public Phone(string name, decimal score, decimal price)
        {
            this.Name = name;
            this.Score = score;
            this.Price = price;
        }
    }

    [NotMapped]
    public class PhonePatch
    {
        public string PhoneName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public decimal Score { get; set; }
    }
}
