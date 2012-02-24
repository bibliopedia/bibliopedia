namespace PublishedWorks.Domain
{
    public class Article : Work
    {
        public virtual Journal PublishedIn { get; set; }
        public virtual Range PageRange { get; set; }
    }
}