using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace _322Api.Models
{
    public class Component
    {
        public int Id { get; private set; }
        [ForeignKey("Device")]
        public int DeviceID { get; set; }
        public string ComponentName { get; set; }

        public Component()
        {
        }
    }
}
