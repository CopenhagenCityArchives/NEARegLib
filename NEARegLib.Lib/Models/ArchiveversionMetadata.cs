using System;

namespace NEARegLib.Models
{
    public class ArchiveversionMetadata
    {
        public int Id { get; set; }
        public string Number { get; set; }
        //This value is calculated from bytes like this: Math.Round((decimal)TotalSize / 1024 / 1024 / 1024, 2);
        public decimal TotalSizeGB { get; set; }
        public int FilesCount { get; set; }
        public bool Searchable { get; set; }
        public int SearchesCount { get; set; }
        public int LocationId { get; set; }
        public int ZipPackagesCount { get; set; }
        public int ZipSoftwareVersion { get; set; }
        public int CPRIndexRowCount { get; set; }

        // Checks all properties including id
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;

            ArchiveversionMetadata ae = (ArchiveversionMetadata)obj;

            return
                Id == ae.Id &&
                Number == ae.Number &&
                TotalSizeGB == ae.TotalSizeGB &&
                FilesCount == ae.FilesCount &&
                Searchable == ae.Searchable &&
                SearchesCount == ae.SearchesCount &&
                ZipPackagesCount == ae.ZipPackagesCount &&
                LocationId == ae.LocationId;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Number);
            hash.Add(TotalSizeGB);
            hash.Add(FilesCount);
            hash.Add(Searchable);
            hash.Add(SearchesCount);
            hash.Add(LocationId);
            hash.Add(ZipPackagesCount);
            return hash.ToHashCode();
        }
    }
}
