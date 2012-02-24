using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ontologies.semanticarts.com.SLMDublinCore;
using Jstor.Domain;
using NC3A.SI.Rowlex;
using SemWeb;

namespace PersistJstorAsTriples
{
    public class DublinCoreConverter
    {
        const string BibliopediaUri = @"http://bibliopedia.org/resources/";

        private readonly Store Store;

        public RdfDocument RdfDocument { get; private set; }

        public DublinCoreConverter(Store store)
        {
            Store = store;
            RdfDocument = new RdfDocument(store);
        }

        public DublinCoreConverter Convert(IEnumerable<dc> records)
        {
            foreach (var record in records)
            {
                Convert(record);
            }
            return this;
        }

        public Document_ Convert(dc input)
        {
            var output = new Document_(MakeUri(input.Id), RdfDocument);

            output.title = new RdfLiteralString(input.title);
            if (input.coverage != null) output.coverage = new RdfLiteralString(input.coverage);
            if (input.description != null) output.AddComment(new RdfLiteralString(input.description));

            output.AddComment(new RdfLiteralString(input.RawText));

            if (input.language != null) output.language_ = CreateResource(input.language, RdfDocument);
            if (input.publisher != null) output.publisher_ = CreateResource(input.publisher, RdfDocument);
            if (input.relation != null) output.relation_ = CreateResource(input.relation, RdfDocument);

            // just escape these things and they'll any repeats will be identical
            if (input.creator != null) output.creator_s = CreateResources(input.creator, RdfDocument); 
            if (input.date != null) output.date_s = CreateResources(input.date, RdfDocument); 
            if (input.identifier != null) output.identifier_s = CreateResources(input.identifier, RdfDocument);
            if (input.subject != null) output.category_s = CreateResources(input.subject, RdfDocument);
            if (input.type != null) output.format_s = CreateResources(input.type, RdfDocument);
            
            return output;
        }

        protected string MakeUri(string resource)
        {
            return Uri.EscapeUriString(BibliopediaUri + resource);
        }

        protected RdfResource CreateResource(string thing, RdfDocument RdfDocument)
        {
            return new RdfLiteralString(thing);
        }

        private RdfResource[] CreateResources(IEnumerable<DcValue> items, RdfDocument RdfDocument)
        {
            var resources = new List<RdfResource>();
            if (items == null) return null;
            foreach (var item in items)
            {
                resources.Add(CreateResource(item.Value, RdfDocument));
            }
            return resources.ToArray();
        }
    }
}
