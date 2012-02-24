#light

module Crawl

open AgentSystem.LAgent;
open WikiInteraction.Domain;

type QueueCommand = 
| Process

type Crawler() =

    let ResultQueueVal = new WikiInteraction.Domain.ResultQueue()
    let CitationQueueVal = new WikiInteraction.Domain.CitationQueue()

    let rec ResultAgent = 
        spawnWorker (fun (command : QueueCommand) ->
            // pop doi:
            let doi = ResultQueueVal.Pop()
            let dc = ()// jstor.repository<dc>(uid)
            // dc = repository<dc>(uid)

            // persist dc

            //CitationQueueVal.Push(dc.JstorDoi());
            ResultAgent <-- Process
            ());
            

    let CitationAgent = 
        spawnWorker (fun command -> ())
            

    member x.ResultQueue = ResultQueueVal
    member x.CitationQueue = CitationQueueVal

    member x.CitationQueueFunc(command : QueueCommand) = 
        // pop uid
        // foreach citation in repository<dc>[uid].citation(referencedby, citeduid/null, value)
            // wikiRecord[citation(referencedby, uid/null, value).Update(citation);
            // if (citeduid!=null) 
                // enqueue uid in RESULTQ
       ()

    static member Instance = Crawler()
    static member Create(results) =
        // foreach result. persist

        // foreach result, enqueue UID in result queue
            
        // create WikiEntry(dc)

        // join on citation queue
            
        // update referencedby

        // foreach result, render to the wiki
        ()

