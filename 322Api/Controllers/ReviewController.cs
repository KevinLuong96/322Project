using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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

        public ReviewController(DatabaseContext context)
        {
            _reviewService = new ReviewService(context);
        }

        [HttpGet]
        public ActionResult GetDeviceReviewByName([FromQuery] string deviceName)
        {
            if (deviceName is null)
            {
                return BadRequest("No device name sent");
            }

            Review[] reviews;
            reviews = this._reviewService.QueryReviewsByName(deviceName);
            return Ok(reviews);
        }
    }
}