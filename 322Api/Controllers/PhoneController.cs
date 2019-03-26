using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using _322Api.Models;
using _322Api.Services;

namespace _322Api.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class PhoneController
    {
        private readonly PhoneService _phoneService;
        public PhoneController(DatabaseContext context)
        {
            this._phoneService = new PhoneService(context);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Phone>> GetPhones(string phoneName)
        {
            if (phoneName is null)
            {
                return new BadRequestObjectResult("No phone name provided");
            }
            Phone[] phones;
            phones = this._phoneService.QueryPhonesByName(phoneName);
            return new OkObjectResult(phones);
        }
    }
}
