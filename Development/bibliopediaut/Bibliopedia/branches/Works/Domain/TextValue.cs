namespace PublishedWorks.Domain
{
    public abstract class TextValue : PersistedObject
    {
        public virtual string Value { get; set; }
    }
}