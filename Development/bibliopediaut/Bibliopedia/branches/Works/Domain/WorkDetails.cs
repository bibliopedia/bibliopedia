using System.Collections.Generic;

namespace PublishedWorks.Domain
{
    public class WorkDetails : PersistedObject
    {
        public WorkDetails()
            : this(null)
        {
        }

        public WorkDetails(Work work)
        {
            Work = work;
            Identifiers = new List<LibraryIdentifier>();
            Authors = new List<Author>();
            Subjects = new List<Subject>();
            Categories = new List<Category>();
            Keywords = new List<Keyword>();
            Comments = new List<Comment>();
            CrossReferences = new List<Work>();
        }

        public virtual Work Work { get; set; }
        public virtual IList<Work> CrossReferences { get; set; }
        public virtual IList<LibraryIdentifier> Identifiers { get; set; }
        public virtual IList<Author> Authors { get; set; }
        public virtual IList<Subject> Subjects { get; set; }
        public virtual IList<Category> Categories { get; set; }
        public virtual IList<Keyword> Keywords { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}