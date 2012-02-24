using PublishedWorks.Domain;

namespace PublishedWorks.RepositoryImplementations
{
    public class WorkRepository : IWorkRepository
    {
        public Work Save(Work work)
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                Database.Session.SaveOrUpdate(work);
                transaction.Commit();
            }
            return work;
        }
    }
}


