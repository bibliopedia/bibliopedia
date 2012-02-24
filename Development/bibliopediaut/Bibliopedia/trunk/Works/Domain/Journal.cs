namespace PublishedWorks.Domain
{
    public class Journal : Work
    {
        /// <summary>
        /// Using string instead of date format because Sql engines have issues with very old dates
        /// </summary>
        public virtual string Edition { get; set; }
    }
}