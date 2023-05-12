using NEARegLib.Models;

namespace NEARegLib.Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Create LogEntries
            var logEntries = new LogEntriesFactory("Server=localhost,3306;Database=neaweb;Allow User Variables=true;User Id=neaweb;Password=123456;").GetNew();

            // Get ArchiveversionMetadata
            var avNumber = "avid.ksa.1";
            var av = logEntries.ArchiveversionMetadataRepository.GetArchiveversionMetadataByIdentifier(avNumber).Result;

            // Get SoftwareVersion
            var newSoftwareVersion = logEntries.SoftwareVersionRepository.InsertAndGetSoftwareVersionByNameAndVersion("software", "version" + new Random().Next(1000)).Result;
            
            //Do some updates to the archiveversion here
            av.TotalSize = 4.3434f;
            av.SearchesCount = 10;
            av.Searchable = true;
            av.LocationId = 1;
            av.EncryptionKey = "encrupt";
            av.TotalSize = 23.3f;
            av.FilesCount = 100000;
            av.ZipPackagesCount = 10;

            var logEntry = new LogEntry()
            {
                Description = "test",
                Status = "Everything ok",
                Type = 1,

                ArchiveversionMetadata = av,
                SoftwareVersion = newSoftwareVersion
            };

            #region ideas
            // _logEntries.UpdateArchiveversionSizeAndFiles(avId, size, files, name, version);
            // _logEntries.UpdateArchiveversionLocation(avId, location, name, version);
            // _logEntries.UpdateArchiveversionSearchableAndSearchesCount(avId, searchable, searchesCount);
            // _logEntries.UpdateArchiveversionZipFilesAndEncryptionKey(avId,zipFilesCount, encryptionKey);

            // How to force load of an existing archiveversion before updating?
            // 1) Set it:
            // _logEntries.SetArchiveversion(av)
            // _logEntries.AddLogEntry() // Throws exception if av is not set
            // 2) Load it:
            // _logEntries.LoadArchiveversion(int id)
            // _logEntries.Archiveversion.FilesCount = 100
            // _logEntries.AddLogEntry() // Throws exception if av is not loaded
            // 3) Inject as parameter:
            // _logEntries.AddLog(logEntry, archiveversion) // Throws exception if av with id does not exist
            // 4) Add logs using archiveversion class:
            // _logEntries = new LogEntries(archiveversionId);
            // _logEntries.Archiveversion.FilesCount = 10;
            // _logEntries.AddEntry(logEntry);
            // archiveversion.Id and Number is private set
            // 5) Now:
            // var av = _logEntries.ArchiveversionRepository.RetrieveByNumber()
            // var softwareVersion = _logEntries.SoftwareVersion.RetrieveByNameAndVersion()
            // var log = new LogEntry() { ArchiveversionMetadata = av, SoftwareVersion = softwareVersion };
            // _logEntries.AddLogEntryUpdateArchiveversionMetadata(logEntry);
            #endregion

            var result = logEntries.AddLogEntryUpdateArchiveversionMetadata(logEntry).Result;
            Console.WriteLine(result);
        }
    }
}