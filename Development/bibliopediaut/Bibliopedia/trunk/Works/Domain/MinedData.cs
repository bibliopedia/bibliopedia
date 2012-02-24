namespace PublishedWorks.Domain
{
    public class MinedData : PersistedObject
    {
        public virtual DataSourceType DataSource { get; set; }
    }
}