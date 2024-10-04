namespace NordpoolUMMAppTask.Models
{
    /// <summary>
    /// Represents a time period for an Unavailability Maintenance Message (UMM) in the Nordpool UMM application.
    /// </summary>
    public class UMMTimePeriod
    {
        /// <summary>
        /// Gets or sets the capacity that is unavailable during the time period.
        /// </summary>
        public int UnavailableCapacity { get; set; }

        /// <summary>
        /// Gets or sets the capacity that is available during the time period.
        /// </summary>
        public int AvailableCapacity { get; set; }

        /// <summary>
        /// Gets or sets the start date and time of the event.
        /// </summary>
        public DateTime EventStart { get; set; }

        /// <summary>
        /// Gets or sets the stop date and time of the event.
        /// </summary>
        public DateTime EventStop { get; set; }
    }
}