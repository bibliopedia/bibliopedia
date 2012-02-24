namespace PublishedWorks.Domain
{
    public class LibraryIdentifier : TextValue
    {
        public virtual LibraryIdType Type { get; set; }
    }
}