using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;
using Jstor;

namespace JstorCrawl.Domain
{
    public abstract class SearchCommand : Command, IDcProducer
    {


        protected abstract IEnumerable<dc> JstorSearch(Search search);

        public virtual IEnumerable<dc> ProduceDc()
        {
            return JstorSearch(new Search());
        }

        /// <summary>
        /// Implementation of this command
        /// </summary>
        /// <returns>Summary of results: Dois, Subjects, Creators found</returns>
        public override CommandResult Execute()
        {
            var result = new CommandResult(Summary);
            foreach (var dc in ProduceDc())
            {
                result += dc;
            }
        }
}
                




































































































































































































































































































































            }
        }


        #region IDcProducer Members

        public virtual IEnumerable<dc> ProduceDc()
        {
           return JstorSearch(Search);
        }

        #endregion
    }
}