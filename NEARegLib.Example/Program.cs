using NEARegLib.Models;

namespace NEARegLib.Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var archiveversionId = "AVID.KSA.1";
            var archiveversionFullPath = "\\\\ksa-arkiv6\\filarkiv\\AVID.KSA.1";
            var logDescription = "LogDescriptionText - Processed files = 36";
            var totalSize = 4242343;
            var filesCount = 1000;

            // Common use cases:
            var neaRegLib = new NEARegFactory("Server=localhost,3306;Database=neaweb;Allow User Variables=true;User Id=neaweb;Password=123456;IgnoreCommandTransaction=true;").GetNew();

            try
            {
                //Use case 1: Update archiveversion metadata with size, filesCount and location
                var av = neaRegLib.ArchiveversionMetadataRepository.GetArchiveversionMetadataByIdentifier(archiveversionId).Result ?? throw new ArgumentException($"No metadata found for {archiveversionId}");
                av.LocationId = neaRegLib.LocationRepository.GetLocationByPath(archiveversionFullPath).Result.Id;
                av.TotalSize = totalSize;
                av.FilesCount = filesCount;

                //NOTE: We expect the repository to make a log entry concerning the update by itself!
                var result = neaRegLib.UpdateArchiveversionAddLogEntry(av, LogEntryType.FileArchive).Result;

                //NOTE: We expect the repository to be aware of the software version and name by it self!
                var result2 = neaRegLib.AddLogEntry(av.Id, logDescription, LogEntryType.FileArchive, false).Result;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Could not update archiveversion or add log entry: {ex.Message}");
            }
        }
    }
}