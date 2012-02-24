namespace PublishedWorks.Domain
{
    public class DataMinedWork : PersistedObject
    {
        public virtual Work Work { get; set; }
        public virtual MinedData SourceData { get; set; }
    }
}


