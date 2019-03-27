using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _322Api.Models
{
    public abstract class ReviewBase
    {
        public int Id { get; private set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public string ReviewUrl { get; set; }
        [Required]
        public string SourceName { get; set; }
    }

    public class Review : ReviewBase
    {
        [ForeignKey("Phone")]
        [Required]
        public int PhoneId { get; set; }
        [ForeignKey("ReviewSource")]
        [Required]
        public int SourceId { get; set; }
    }

    [NotMapped]
    public class HttpReview : ReviewBase
    {
        [Required]
        public string PhoneName { get; }

        public HttpReview(string Category, string ReviewText, string PhoneName, string SourceName)
        {
            this.Category = Category;
            this.ReviewText = ReviewText;
            this.PhoneName = PhoneName;
            this.SourceName = SourceName;
        }
    }
}
