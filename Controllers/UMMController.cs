using Microsoft.AspNetCore.Mvc;
using NordpoolUMMAppTask.Services;

namespace NordpoolUMMAppTask.Controllers
{
    /// <summary>
    /// Retrieves UMM production unavailability messages for the specified date range and groups the results based on the specified criteria.
    /// /// UMMController handles API requests related to UMM (Urgent Market Messages) production unavailability.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UMMController : ControllerBase
    {
        private readonly UMMApiService _ummApiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UMMController"/> class.
        /// </summary>
        /// <param name="ummApiService">The service used to retrieve UMM production unavailability data.</param>
        public UMMController(UMMApiService ummApiService)
        {
            _ummApiService = ummApiService;
        }

        /// <summary>
        /// Retrieves UMM production unavailability messages for the specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A list of UMM production unavailability messages or a not found result if no messages are found.</returns>
        [HttpGet("production-unavailability/{startDate}&{endDate}")]
        public async Task<IActionResult> GetUMMProductionUnavailability(DateTime startDate, DateTime endDate)
        {
            var messages = await _ummApiService.RetrieveUMMProductionUnavailabilityAsync(startDate, endDate);
            if (messages == null || messages.Count == 0)
            {
                return NotFound("No production unavailability UMMs found for the selected date.");
            }
            return Ok(messages);
        }

        /// <summary>
        /// Retrieves all UMM production unavailability messages without any date filter.
        /// </summary>
        /// <returns>A list of UMM production unavailability messages or a not found result if no messages are found.</returns>
        [HttpGet("production-unavailability")]
        public async Task<IActionResult> GetUMMProductionUnavailabilityNoDate()
        {
            var messages = await _ummApiService.RetrieveUMMProductionUnavailabilityAsyncNoDate();
            if (messages == null || messages.Count == 0)
            {
                return NotFound("No production unavailability UMMs found.");
            }
            return Ok(messages);
        }

        /// <summary>
        /// Retrieves UMM production unavailability messages for the specified date range and groups the results based on the specified criteria.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <param name="isFuelType">A boolean value indicating whether to group the results by fuel type.</param>
        /// <param name="isGeoLocation">A boolean value indicating whether to group the results by geo location.</param>
        /// <returns>A list of UMM production unavailability messages grouped by the specified criteria.</returns>
        [HttpGet("production-unavailability-UI")]
        public async Task<IActionResult> GetUMMProductionUnavailabilityUI([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] bool isFuelType, [FromQuery] bool isGeoLocation)
        {
            Console.WriteLine($"Start Date: {startDate}, End Date: {endDate}");

            var messages = await _ummApiService.RetrieveUMMProductionUnavailabilityAsync(startDate, endDate);
            if (messages == null || messages.Count == 0)
            {
                return NotFound("No production unavailability UMMs found for the selected date.");
            }
            
            if (isFuelType)
            {
                var result = messages
                    .SelectMany(m => m.ProductionUnits)
                    .GroupBy(pu => pu.FuelType)
                    .Select(g => new {
                        FuelType = g.Key,
                        totalUnavailableCapacity = g
                            .SelectMany(pu => pu.TimePeriods)
                            .Where(tp => tp.EventStart.Date >= startDate && tp.EventStop.Date <= endDate)
                            .Sum(tp => tp.UnavailableCapacity)
                    })
                    .ToList();

                return Ok(result);
            } else if (isGeoLocation){
                var result = messages
                    .SelectMany(m => m.ProductionUnits)
                    .GroupBy(pu => pu.AreaName)
                    .Select(g => new {
                        AreaName = g.Key,
                        totalUnavailableCapacity = g
                            .SelectMany(pu => pu.TimePeriods)
                            .Where(tp => tp.EventStart.Date >= startDate && tp.EventStop.Date <= endDate)
                            .Sum(tp => tp.UnavailableCapacity)
                    })
                    .ToList();

                return Ok(result);
            }
              else {
                var result = messages
                    .SelectMany(m => m.ProductionUnits)
                    .SelectMany(pu => pu.TimePeriods)
                    .Where(tp => tp.EventStart.Date >= startDate && tp.EventStop.Date <= endDate)
                    .GroupBy(tp => tp.EventStart.Date)
                    .Select(g => new {
                        Date = g.Key,
                        totalUnavailableCapacity = g.Sum(tp => tp.UnavailableCapacity)
                    })
                    .ToList();

                return Ok(result);
            }
        }
    }
}