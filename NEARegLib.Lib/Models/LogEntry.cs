﻿using System;

namespace NEARegLib.Models
{
    public enum LogEntryType : ushort
    {
        FileArchive = 6,
        WolfPack = 5,
        MiNEA = 7,
    }

    public class LogEntry
    {
        public int Id { get; }
        public LogEntryType Type { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; }
        public bool ErrorsOccurred { get; set; }
        public int UserId { get { return -1; } }
        public int ArchiveversionId { get; set; }
        public int SoftwareVersionId { get; set; }
    }
}
