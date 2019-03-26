﻿using Microsoft.AspNetCore.Mvc;
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
    public class PhoneController : ControllerBase
    {
        private readonly PhoneService _phoneService;
        public PhoneController(DatabaseContext context)
        {
            this._phoneService = new PhoneService(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Phone>> GetPhones(string phoneName)
        {
            if (phoneName is null)
            {
                return BadRequest("No phone name provided");
            }
            Phone[] phones;
            phones = this._phoneService.QueryPhonesByName(phoneName);
            return Ok(phones);
        }

        [HttpPost]
        public async Task<ActionResult<Phone>> CreatePhone([FromBody] string phoneName)
        {
            if (phoneName == "")
            {
                return BadRequest("No phone name provided");
            }
            phoneName = phoneName.Trim().ToLower();

            int phoneId = this._phoneService.GetPhoneIdByName(phoneName);
            if (phoneId != 0)
            {
                return BadRequest("A phone with this name already exists in database");
            }

            Phone phone;
            phone = await this._phoneService.CreatePhone(phoneName);
            return Ok(phone);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePhone(PhonePatch p)
        {
            if (p.PhoneName == "" || (p.Price == 0 && p.ImageUrl == ""))
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
