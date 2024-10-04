using System.Text.Json;
using NordpoolUMMAppTask.Models;

namespace NordpoolUMMAppTask.Services
{
    /// <summary>
    /// Service class to interact with the UMM API and retrieve UMM messages.
    /// </summary>
    public class UMMApiService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UMMApiService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to be used for making API requests.</param>
        public UMMApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Asynchronously retrieves UMM messages from the API.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UMM API response.</returns>
        public async Task<UMMApiResponse> GetUMMMessagesAsync()
        {
            // TODO - Nothing should be hardcoded in the code. This URL should be in the appsettings.json file.
            var url = "https://ummapi.nordpoolgroup.com/messages";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<UMMApiResponse>(content, options);
                return apiResponse ?? new UMMApiResponse(); // Return empty response if deserialization fails
            }

            return new UMMApiResponse();
        }

        /// <summary>
        /// Filters UMM messages to get production unavailability within a specified date range.
        /// </summary>
        /// <param name="apiResponse">The UMM API response containing the messages.</param>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>A list of UMM message items that match the criteria.</returns>
        public List<UMMMessageItem> GetUMMProductionUnavailability(UMMApiResponse apiResponse, DateTime startDate, DateTime endDate)
        {
            // TODO - The message type should be an enum instead of a magic number.
            var messageItems = apiResponse.Items
                .Where(m => m.MessageType == 1 && m.ProductionUnits != null)
                .Where(m => m.ProductionUnits.Any(pu => pu.TimePeriods.Any(tp => tp.EventStart >= startDate && tp.EventStop <= endDate)))
                .ToList();

            Console.WriteLine(messageItems.Count);
            return messageItems;
        }

        /// <summary>
        /// Filters UMM messages to get production unavailability without considering date range.
        /// </summary>
        /// <param name="apiResponse">The UMM API response containing the messages.</param>
        /// <returns>A list of UMM message items that match the criteria.</returns>
        public List<UMMMessageItem> GetUMMProductionUnavailabilityNoDate(UMMApiResponse apiResponse)
        {
            var messageItems = apiResponse.Items
                .Where(m => m.MessageType == 1)
                .ToList();

            return messageItems;
        }

        /// <summary>
        /// Asynchronously retrieves and filters UMM messages to get production unavailability within a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of UMM message items that match the criteria.</returns>
        public async Task<List<UMMMessageItem>> RetrieveUMMProductionUnavailabilityAsync(DateTime startDate, DateTime endDate)
        {
            var apiResponse = await GetUMMMessagesAsync();
            if (apiResponse != null) 
            {
                var filteredMessages = GetUMMProductionUnavailability(apiResponse, startDate, endDate);
                return filteredMessages;
            }
            
            return new List<UMMMessageItem>();
        }

        /// <summary>
        /// Asynchronously retrieves and filters UMM messages to get production unavailability without considering date range.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of UMM message items that match the criteria.</returns>
        public async Task<List<UMMMessageItem>> RetrieveUMMProductionUnavailabilityAsyncNoDate()
        {
            var apiResponse = await GetUMMMessagesAsync();
            if (apiResponse != null) 
            {
                var filteredMessages = GetUMMProductionUnavailabilityNoDate(apiResponse);
                return filteredMessages;
            }
            
            return new List<UMMMessageItem>();
        }
    }
}