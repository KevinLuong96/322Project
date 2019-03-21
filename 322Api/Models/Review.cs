using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace _322Api.Models
{
    public class Review
    {
        public int Id { get; private set; }
        [ForeignKey("Phone")]
        public int PhoneId { get; set; }
        [ForeignKey("ReviewSource")]
        public int SourceId { get; set; }
        public string[] Category { get; set; }
        public string ReviewText { get; set; }

        public Review()
        {
        }
    }
}
