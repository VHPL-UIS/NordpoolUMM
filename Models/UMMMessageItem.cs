namespace NordpoolUMMAppTask.Models
    {
    /// <summary>
    /// Represents a UMM (Urgent Market Message) item in the Nordpool UMM application.
    /// </summary>
    public class UMMMessageItem
    {
        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the message is outdated.
        /// </summary>
        public bool IsOutdated { get; set; }

        /// <summary>
        /// Gets or sets the type of unavailability.
        /// </summary>
        public int UnavailabilityType { get; set; }

        /// <summary>
        /// Gets or sets the reason code for the unavailability.
        /// </summary>
        public int ReasonCode { get; set; }

        /// <summary>
        /// Gets or sets the reason for the unavailability.
        /// </summary>
        public string UnavailabilityReason { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the remarks associated with the message.
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of production units associated with the message.
        /// </summary>
        public List<UMMProductionUnit> ProductionUnits { get; set; } = new List<UMMProductionUnit>();

        /// <summary>
        /// Gets or sets the list of market participants associated with the message.
        /// </summary>
        public List<UMMMarketParticipant> MarketParticipants { get; set; } = new List<UMMMarketParticipant>();

        /// <summary>
        /// Gets or sets the status of the event.
        /// </summary>
        public int EventStatus { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the message.
        /// </summary>
        public string MessageId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version of the message.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the publication date of the message.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the publisher.
        /// </summary>
        public string PublisherId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the publisher.
        /// </summary>
        public string PublisherName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of ACER RSS message identifiers associated with the message.
        /// </summary>
        public List<string> AcerRssMessageIds { get; set; } = new List<string>();
    }
}