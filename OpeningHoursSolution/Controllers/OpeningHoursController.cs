using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpeningHoursSolution.Models;

namespace OpeningHoursSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningHoursController : ControllerBase
    {
        private readonly OpeningHoursService _openingHoursService;
        public OpeningHoursController(OpeningHoursService openingHoursService)
        {
            _openingHoursService = openingHoursService;
        }

        [HttpPost("/format_hours")]
        public ActionResult<OpeningHoursResponse> FormatOpeningHours([FromBody] OpeningHoursRequest request)
        {
            try
            {
                var formattedHours = _openingHoursService.FormatOpeningHours(request);
                var response = new OpeningHoursResponse { FormattedHours = formattedHours };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

       
    }
}


