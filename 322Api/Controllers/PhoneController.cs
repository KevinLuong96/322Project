using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PhoneController : BaseController
    {
        private readonly PhoneService _phoneService;
        public PhoneController(DatabaseContext context)
        {
            this._phoneService = new PhoneService(context);
            this._userService = new UserService(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetPhones(string phoneName)
        {
            if (phoneName is null)
            {
                return BadRequest("No phone name provided");
            }
            Phone[] phones;
            phones = this._phoneService.QueryPhonesByName(phoneName.ToLower().Trim());

            User user = this.GetUserFromClaims();
            if (!(user is null))
            {
                await this._userService.AddToHistory(user.Id, phoneName);
            }
            return Ok(phones);
        }

        [HttpPost]
        public async Task<ActionResult<Phone[]>> CreatePhones([FromBody] Phone[] phones)
        {
            List<Phone> phoneList = new List<Phone> { };

            foreach (Phone p in phones)
            {
                string phoneName = p.Name.Trim().ToLower();

                int phoneId = this._phoneService.GetPhoneIdByName(phoneName);
                if (phoneId != 0)
                {
                    continue;
                }
                phoneList.Add(p);
            }

            if (phoneList.Count == 0)
            {
                return BadRequest("No unique phones sent");
            }
            else
            {
                Phone[] createdPhones = await this._phoneService.CreatePhones(phoneList.ToArray());
                return createdPhones;
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePhone(PhonePatch p)
        {
            if (p.PhoneName == "" || (p.Price == 0 && p.ImageUrl == "" && p.Score == 0))
            {
                return BadRequest();
            }

            int phoneId = this._phoneService.GetPhoneIdByName(p.PhoneName);
            if (phoneId == 0)
            {
                return NotFound();
            }

            Phone phone = await this._phoneService.UpdatePhone(phoneId, p);

            return new OkObjectResult(phone);
        }
    }
}
