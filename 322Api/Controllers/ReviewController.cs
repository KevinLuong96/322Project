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
        private readonly PhoneService _phoneService;

        public ReviewController(DatabaseContext context)
        {
            _reviewService = new ReviewService(context);
            _phoneService = new PhoneService(context);
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
    }
}