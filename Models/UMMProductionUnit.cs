namespace NordpoolUMMAppTask.Models
    {
    /// <summary>
    /// Represents a production unit in the Nordpool UMM application.
    /// </summary>
    public class UMMProductionUnit
    {
        /// <summary>
        /// Gets or sets the name of the production unit.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the EIC (Energy Identification Code) of the production unit.
        /// </summary>
        public string Eic { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the fuel type of the production unit.
        /// </summary>
        public int FuelType { get; set; }

        /// <summary>
        /// Gets or sets the area EIC (Energy Identification Code) where the production unit is located.
        /// </summary>
        public string AreaEic { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the area where the production unit is located.
        /// </summary>
        public string AreaName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the installed capacity of the production unit.
        /// </summary>
        public int InstalledCapacity { get; set; }

        /// <summary>
        /// Gets or sets the list of time periods associated with the production unit.
        /// </summary>
        public List<UMMTimePeriod> TimePeriods { get; set; } = new List<UMMTimePeriod>();
    }
}