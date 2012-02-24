namespace PublishedWorks.Domain
{
    public class Range : PersistedObject
    {
        public virtual int First { get; set; }
        public virtual int Last { get; set; }
    }
}