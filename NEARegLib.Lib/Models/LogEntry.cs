using System;

namespace neaweb.Models
{
    public class LogEntry
    {
        public int Id { get; }
        public int Type { get; set; }
        public ArchiveversionMetadata ArchiveversionMetadata { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; }
        public string Status { get; set; }
        public SoftwareVersion SoftwareVersion { get; set; }
        public int UserId { get { return -1; } }
        public int ArchiveversionId { get { return ArchiveversionMetadata.Id; } }
        public int SoftwareVersionId { get { return SoftwareVersion.Id; } }
    }
}
