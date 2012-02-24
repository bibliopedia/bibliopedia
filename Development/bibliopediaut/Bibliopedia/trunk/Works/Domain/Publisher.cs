namespace PublishedWorks.Domain
{
    public class Publisher : PersistedObject
    {
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
    }
}