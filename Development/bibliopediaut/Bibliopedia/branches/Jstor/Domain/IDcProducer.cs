using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;

namespace Jstor.Domain
{
    public interface IDcProducer
    {
        IEnumerable<dc> ProduceDc();
    }
}
