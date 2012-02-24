using System.Collections.Generic;

namespace PublishedWorks.Domain
{
    /// <summary>
    /// Represents a work (e.g. numerous editions of a book, article, etc.)
    /// </summary>
    public class Work : PersistedObject
    {
        public Work()
        {
            Details = new WorkDetails(this);
            DataMinedVersions = new List<DataMinedWork>();
        }

        public virtual string Title { get; set; }

        public virtual WorkDetails Details { get; set; }

        public virtual IList<DataMinedWork> DataMinedVersions { get; set; }

    }
}


