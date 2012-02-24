using FluentNHibernate.Data;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using Jstor.JstorService;

namespace Jstor.Domain
{

    public class searchRetrieveResponse : Entity
    {
        public searchRetrieveResponse() { }

        public searchRetrieveResponse(JstorService.searchRetrieveResponse other)
        {
            version = other.version;
            numberOfRecords = Int32.Parse(other.numberOfRecords);
            nextRecordPosition = Int32.Parse(other.nextRecordPosition);
            records = searchRetrieveResponseRecordsRecord.FromArray(other.records);
            echoedSearchRetrieveRequest = searchRetrieveResponseEchoedSearchRetrieveRequest.FromArray(other.echoedSearchRetrieveRequest);
        }

        public static IList<searchRetrieveResponse> FromArray(JstorService.searchRetrieveResponse[] array)
        {
            var results = new List<searchRetrieveResponse>();
            foreach (var item in array)
            {
                results.Add(new searchRetrieveResponse(item));
            }
            return results;
        }

        /// <remarks/>
        public virtual string version { get; set; }

        /// <remarks/>
        public virtual int numberOfRecords { get; set; }

        /// <remarks/>
        public virtual int nextRecordPosition { get; set; }

        /// <remarks/>
        public virtual IList<searchRetrieveResponseRecordsRecord> records { get; set; }

        /// <remarks/>
        public virtual IList<searchRetrieveResponseEchoedSearchRetrieveRequest> echoedSearchRetrieveRequest { get; set; }
    }

    public class searchRetrieveResponseRecordsRecord : Entity
    {
        public searchRetrieveResponseRecordsRecord() { }

        public searchRetrieveResponseRecordsRecord(JstorService.searchRetrieveResponseRecordsRecord other)
        {
            recordSchema = other.recordSchema;
            recordPacking = other.recordPacking;
            recordPosition = other.recordPosition;
            recordData = searchRetrieveResponseRecordsRecordRecordData.FromArray(other.recordData);
        }

        public static IList<searchRetrieveResponseRecordsRecord> FromArray(JstorService.searchRetrieveResponseRecordsRecord[] array)
        {
            var results = new List<searchRetrieveResponseRecordsRecord>();
            foreach (var item in array)
            {
                results.Add(new searchRetrieveResponseRecordsRecord(item));
            }
            return results;
        }

        /// <remarks/>
        public virtual string recordSchema { get; set; }

        /// <remarks/>
        public virtual string recordPacking { get; set; }

        /// <remarks/>
        public virtual string recordPosition { get; set; }

        /// <remarks/>
        public virtual IList<searchRetrieveResponseRecordsRecordRecordData> recordData { get; set; }
    }

    public class searchRetrieveResponseRecordsRecordRecordData : Entity
    {
        public searchRetrieveResponseRecordsRecordRecordData() { }

        public searchRetrieveResponseRecordsRecordRecordData(JstorService.searchRetrieveResponseRecordsRecordRecordData other)
        {
            dc = new dc(other);
        }

        /// <remarks/>
        public virtual dc dc { get; set; }

        public static IList<searchRetrieveResponseRecordsRecordRecordData> FromArray(JstorService.searchRetrieveResponseRecordsRecordRecordData[] array)
        {
            var results = new List<searchRetrieveResponseRecordsRecordRecordData>();
            foreach (var item in array)
            {
                results.Add(new searchRetrieveResponseRecordsRecordRecordData(item));
            }
            return results;
        }
    }

    public class HashedValue : Entity, IComparable
    {
        public HashedValue() { }

        public HashedValue(string value) { Value = value; }

        private string _value;
        public virtual string Value
        {
            get
            {
                return _value;
            }
            protected set
            {
                _value = value;
                UpdateSha1Hash();
            }
        }

        protected virtual void UpdateSha1Hash()
        {
            var value = Value ?? "";
            var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var stream = new System.IO.MemoryStream(ASCIIEncoding.Default.GetBytes(value));
            var result = sha1.ComputeHash(stream).Select(b => Convert.ToString(b));
            Sha1Hash = "{" + String.Join(",", result.ToArray()) + "}";
        }

        public virtual string Sha1Hash { get; protected set; }

        public virtual int CompareTo(object obj)
        {
            if (!(obj is dc)) return -1;
            var other = obj as dc;
            return Comparer<string>.Default.Compare(Sha1Hash, other.Sha1Hash);
        }
    }

    /// Dublin Core XML (not RDF) format
    public class dc : HashedValue
    {
        public dc() { }

        public dc(JstorService.searchRetrieveResponseRecordsRecordRecordData other)
            : base(other.RawText())
        {
            relation = other.relation;
            language = other.language;
            coverage = other.coverage;
            publisher = other.publisher;
            title = other.title;
            description = other.description;
            creator = FromArray(other.creator);
            identifier = FromArray(other.identifier);
            type = FromArray(other.type);
            date = FromArray(other.date);
            subject = FromArray(other.subject);
        }

        protected ISet<HashedValue> FromArray(IHasValue[] values)
        {
            if (values == null) return null;
            var hashedValues = values.Select(value => 
                {
                    return new HashedValue(value.Value);
                });
            var set = new SortedSet<HashedValue>((ICollection<HashedValue>)hashedValues.ToList());
            return set;
        }

        public virtual string relation { get; protected set; }

        public virtual string language { get; protected set; }

        public virtual string coverage { get; protected set; }

        public virtual string publisher { get; protected set; }

        public virtual string title { get; protected set; }

        public virtual string description { get; protected set; }

        public virtual ISet<HashedValue> creator { get; protected set; }

        public virtual ISet<HashedValue> identifier { get; protected set; }

        public virtual ISet<HashedValue> type { get; protected set; }

        public virtual ISet<HashedValue> date { get; protected set; }

        public virtual ISet<HashedValue> subject { get; protected set; }

        public virtual ISet<HashedValue> ReferencedBy { get; protected set; }

        public virtual ISet<HashedValue> ReferenceIndexes { get; protected set; }
    }

    public class searchRetrieveResponseEchoedSearchRetrieveRequest : Entity
    {
        public searchRetrieveResponseEchoedSearchRetrieveRequest() { }

        public searchRetrieveResponseEchoedSearchRetrieveRequest(JstorService.searchRetrieveResponseEchoedSearchRetrieveRequest other)
        {
            version = other.version;
            query = other.query;
            startRecord = other.startRecord;
            maximumRecords = other.maximumRecords;
            recordPacking = other.recordPacking;
            recordSchema = other.recordSchema;
        }

        /// <remarks/>
        public virtual string version { get; set; }

        /// <remarks/>
        public virtual string query { get; set; }

        /// <remarks/>
        public virtual string startRecord { get; set; }

        /// <remarks/>
        public virtual string maximumRecords { get; set; }

        /// <remarks/>
        public virtual string recordPacking { get; set; }

        /// <remarks/>
        public virtual string recordSchema { get; set; }

        public static IList<searchRetrieveResponseEchoedSearchRetrieveRequest> FromArray(JstorService.searchRetrieveResponseEchoedSearchRetrieveRequest[] array)
        {
            var results = new List<searchRetrieveResponseEchoedSearchRetrieveRequest>();
            foreach (var item in array)
            {
                results.Add(new searchRetrieveResponseEchoedSearchRetrieveRequest(item));
            }
            return results;
        }
    }

    public class NewDataSet : Entity
    {
        public NewDataSet() { }

        public NewDataSet(JstorService.NewDataSet other)
        {
            Items = searchRetrieveResponse.FromArray(other.Items);
        }

        public virtual IList<searchRetrieveResponse> Items { get; set; }
    }
}
