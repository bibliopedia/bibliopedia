using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublishedWorks.Domain;
using PublishedWorks.RepositoryImplementations;

namespace PublishedWorks
{
    public static class Repositories
    {
        static Repositories()
        {
            Work = new WorkRepository();
        }

        public static IWorkRepository Work { get; internal set; }
    }
}
