using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _322Api.Models
{
    public class Phone : Device
    {
        public string[] Providers { get; set; }
        public Phone()
        {
        }
    }

    [NotMapped]
    public class PhonePatch
    {
        public string PhoneName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
