﻿using System;
using System.ComponentModel.DataAnnotations;
namespace _322Api.Models
{
    public abstract class Device
    {
        public int Id { get; private set; }
        [Required]
        public string Name { get; set; }
        public DateTime LastCrawl { get; set; }
        public decimal Score { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public Device()
        {
        }
    }
}
