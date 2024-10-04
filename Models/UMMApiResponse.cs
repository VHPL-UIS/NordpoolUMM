using System.Text.Json.Serialization;
namespace NordpoolUMMAppTask.Models
{
    /// <summary>
    /// Represents the response from the UMM API.
    /// </summary>
    public class UMMApiResponse
    {
        /// <summary>
        /// Gets or sets the list of UMM message items.
        /// </summary>
        /// <value>
        /// A list of <see cref="UMMMessageItem"/> objects.
        /// </value>
        [JsonPropertyName("items")]
        public List<UMMMessageItem> Items { get; set; } = new List<UMMMessageItem>();
    }
}