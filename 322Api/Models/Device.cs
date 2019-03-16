using System;
namespace _322Api.Models
{
    public abstract class Device
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public DateTime LastCrawl { get; set; }
        public int Score { get; set; }

        public Device()
        {
        }
    }
}
