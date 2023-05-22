using System;

namespace NEARegLib.Models
{
    public class ArchiveversionMetadata
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public float TotalSize { get; set; }
        public int FilesCount { get; set; }
        public bool Searchable { get; set; }
        public int SearchesCount { get; set; }
        public int LocationId { get; set; }
        public int ZipPackagesCount { get; set; }

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
                TotalSize == ae.TotalSize &&
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
            hash.Add(TotalSize);
            hash.Add(FilesCount);
            hash.Add(Searchable);
            hash.Add(SearchesCount);
            hash.Add(LocationId);
            hash.Add(ZipPackagesCount);
            return hash.ToHashCode();
        }
    }
}
