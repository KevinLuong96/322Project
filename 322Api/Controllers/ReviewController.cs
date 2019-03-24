﻿using Microsoft.AspNetCore.Mvc;
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

            if (context.ReviewSources.Count() == 0)
            {
                context.ReviewSources.Add(new ReviewSource
                {
                    SourceName = "TheVerge",
                });
                context.ReviewSources.Add(new ReviewSource
                {
                    SourceName = "TechRadar",
                });
                context.ReviewSources.Add(new ReviewSource
                {
                    SourceName = "Cnet",
                });

                context.SaveChanges();
            }

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

                //return data got from scraping 
                return Ok(null);
            }


            Review[] reviews;
            reviews = this._reviewService.QueryReviewsById(phoneId);
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
                Review temp = this.ConvertReview(review);
                if (this._reviewService.IsReviewUnique(temp.PhoneId, temp.SourceId))
                {
                    reviews.Add(this.ConvertReview(review));
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
        private Review ConvertReview(HttpReview reviewData)
        {
            Review review = new Review
            {
                ReviewText = reviewData.ReviewText,
                Category = reviewData.Category
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
                this._phoneIds[reviewData.PhoneName] = phoneId;
            }

            try
            {
                sourceId = this._sourceIds[reviewData.SourceName];
            }
            catch
            {
                sourceId = this._reviewSourceService.GetReviewSourceIdBySourceName(reviewData.SourceName);
                this._sourceIds[reviewData.SourceName] = sourceId;
            }

            review.PhoneId = phoneId;
            review.SourceId = sourceId;


            return review;
        }
    }
}