using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly PhoneService _phoneService;
        private readonly ReviewSourceService _reviewSourceService;
        private readonly SortedDictionary<string, int> _phoneIds;
        private readonly SortedDictionary<string, int> _sourceIds;

        public ReviewController(DatabaseContext context)
        {
            _reviewService = new ReviewService(context);
            _phoneService = new PhoneService(context);
            _reviewSourceService = new ReviewSourceService(context);
            _phoneIds = new SortedDictionary<string, int> { };
            _sourceIds = new SortedDictionary<string, int> { };
        }

        [HttpGet]
        public async Task<ActionResult> GetDeviceReviewByName([FromQuery] string deviceName)
        {
            if (deviceName is null)
            {
                return BadRequest("No device name sent");
            }
            deviceName = deviceName.ToLower();

            int phoneId = this._phoneService.GetPhoneIdByName(deviceName);
            //if phone doesnt exist in DB
            if (phoneId == 0)
            {
                var tasks = new List<Task>
                {
                    this._phoneService.CreatePhone(deviceName)
                    //perform crawl in here too
                };
                await Task.WhenAll(tasks.ToArray());
            }


            Review[] reviews;
            reviews = this._reviewService.QueryReviewsById(phoneId);
            return Ok(reviews);
        }
        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Review[]> GetDeviceReviewsById(int id)
        {
            Review[] reviews;
            reviews = this._reviewService.QueryReviewsById(id);
            return Ok(reviews);
        }


        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<Review>), StatusCodes.Status201Created)]
        public async Task<ActionResult> SubmitReviews([FromBody] HttpReview[] r)
        {
            List<Review> reviews = new List<Review>();

            foreach (HttpReview review in r)
            {
                Review temp = await this.ConvertReview(review);
                if (this._reviewService.IsReviewUnique(temp.ReviewUrl))
                {
                    reviews.Add(temp);
                }
            }

            if (reviews.Count == 0)
            {
                return NoContent();
            }

            await this._reviewService.SaveReviews(reviews.ToArray());

            return CreatedAtAction("SubmitReviews", reviews);
        }

        //Converts an HTTP review with sourcename and phone name into a review
        //with sourceId and phoneId
        private async Task<Review> ConvertReview(HttpReview reviewData)
        {
            Review review = new Review
            {
                ReviewText = reviewData.ReviewText,
                Category = reviewData.Category,
                ReviewUrl = reviewData.ReviewUrl,
                SourceName = reviewData.SourceName
            };
            int phoneId;
            int sourceId;

            try
            {
                phoneId = this._phoneIds[reviewData.PhoneName];
            }
            catch
            {
                phoneId = this._phoneService.GetPhoneIdByName(reviewData.PhoneName);
                //no phone found, insert into db
                if (phoneId == 0)
                {
                    Phone newPhone = await this._phoneService.CreatePhone(reviewData.PhoneName);
                    phoneId = newPhone.Id;
                }
                this._phoneIds[reviewData.PhoneName] = phoneId;
            }

            try
            {
                sourceId = this._sourceIds[reviewData.SourceName];
            }
            catch
            {
                sourceId = this._reviewSourceService.GetReviewSourceIdBySourceName(reviewData.SourceName);
                //no source Id found, put into db
                if (sourceId == 0)
                {
                    ReviewSource newReviewSource = await this._reviewSourceService.CreateReviewSource(reviewData.SourceName);
                    sourceId = newReviewSource.Id;
                }
                this._sourceIds[reviewData.SourceName] = sourceId;
            }

            review.PhoneId = phoneId;
            review.SourceId = sourceId;


            return review;
        }
    }
}