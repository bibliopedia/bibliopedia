namespace PublishedWorks.Domain
{
    public class Book : Work
    {
        public virtual Publisher Publisher { get; set; }
        /// <summary>
        /// Sql databases cannot store dates old enough for our purposes
        /// </summary>
        public virtual string PublishDate { get; set; }
    }
}