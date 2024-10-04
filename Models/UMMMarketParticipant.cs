namespace NordpoolUMMAppTask.Models{
    /// <summary>
    /// Represents a market participant in the UMM (Urgent Market Messages) system.
    /// </summary>
    public class UMMMarketParticipant
    {
        /// <summary>
        /// Gets or sets the name of the market participant.
        /// </summary>
        /// <value>The name of the market participant.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the code of the market participant.
        /// </summary>
        /// <value>The code of the market participant.</value>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ACER code of the market participant.
        /// </summary>
        /// <value>The ACER code of the market participant.</value>
        public string AcerCode { get; set; } = string.Empty;
    }
}