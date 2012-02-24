using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JstorCrawl.Domain.ValueObjects
{
    public class CommandResponse
    {
        public CommandResponse(string summaryOfResults, IEnumerable<Command> commandsToEnqueue)
        {
            SummaryOfResults = summaryOfResults;
            CommandsToEnqueue = commandsToEnqueue;
        }

        public string SummaryOfResults { get; protected set; }
        public IEnumerable<Command> CommandsToEnqueue { get; protected set; }
    }
}
