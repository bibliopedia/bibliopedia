using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Data.Extensions;
using Iesi.Collections.Generic;
using FluentNHibernate.Data;
using Iesi.Collections;
using System.Diagnostics.Contracts;

namespace Jstor.Domain
{
    public class DcValue : StringEntity
    {
        public DcValue() { }
        public DcValue(string value) { Value = value; }

        public virtual string Value { get; protected set; }

        public static Iesi.Collections.Generic.ISet<DcValue> FromArray(Jstor.JstorService.IHasValue[] values)
        {
            if (values == null) return null;
            var hashedValues = values.Select(value =>
            {
                return new DcValue { Value = value.Value };
            });
            var set = new OrderedSet<DcValue>((ICollection<DcValue>)hashedValues.ToList());
            return set;
        }

        public override object ComputeIdentity()
        {
            return Value;
        }

    }

    public abstract class RecordData : StringEntity
    {
        public override abstract object ComputeIdentity();
    }

    public class Record : StringEntity
    {
        public Record() { Citations = new Iesi.Collections.Generic.SortedSet<Citation>(); }
        public Record(RecordData data) : this() { Data = data; }
        public Record(RecordData data, Iesi.Collections.Generic.ISet<Citation> citations) : this(data)
        { Citations = new Iesi.Collections.Generic.SortedSet<Citation>(citations); }

        public virtual RecordData Data { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<Citation> Citations { get; protected set; }

        public override void AssignIdentity()
        {
            Data.AssignIdentity();
            Id = Data.Id;
        }

        public override object ComputeIdentity()
        {
            return Data.ComputeIdentity();
        }
    }

    public class Citation : StringEntity
    {
        public Citation() { }
        public Citation(Record from, Record to) { From = from; To = to; }
        public virtual Record From { get; protected set; }
        public virtual Record To { get; protected set; }

        public override object ComputeIdentity()
        {
            return new StringBuilder().AppendFormat("{{0},{1}}", From.Id, To.Id);
        }
    }

    /// Dublin Core XML (not RDF) format
    public class dc : RecordData, IDcProducer
    {
        public dc() { }

        public dc(JstorService.searchRetrieveResponseRecordsRecordRecordData other)
        {
            relation = other.relation;
            language = other.language;
            coverage = other.coverage;
            publisher = other.publisher;
            title = other.title;
            description = other.description;
            creator = DcValue.FromArray(other.creator);
            identifier = DcValue.FromArray(other.identifier);
            type = DcValue.FromArray(other.type);
            date = DcValue.FromArray(other.date);
            subject = DcValue.FromArray(other.subject);

            var rawText = other.RawText();
            RawText = rawText;

            UpdateDoi();
        }

        public override object ComputeIdentity()
        {
            if (Id != null) return Id;

            Func<string, string> RemoveWhitespace = (@string) =>
                @string.Split(',', ' ', '.').AsEnumerable().Fold("",
                    (idSubstr, state) =>
                    {
                        state += idSubstr;
                        return state;
                    });

            Func<string, string> ChopWhitespace = (@string) =>
                @string.Substring(0, @string.IndexOfAny(new[] { ' ', ',' }));

            var prepTitle = RemoveWhitespace(title ?? "");

            var prepCreator =
                creator.AsEnumerable().Fold("",
                    (creatorItem, stringSoFar) =>
                    {
                        stringSoFar += ChopWhitespace(creatorItem.Value ?? "");
                        return stringSoFar;
                    });

            var prepCoverage = RemoveWhitespace(coverage ?? "");

            var wholeId = prepTitle + prepCreator + prepCoverage;
            if (wholeId.Length > 255) wholeId = wholeId.Substring(0, 255);
            return wholeId;
        }

        public virtual void MergeWith(dc item)
        {
            if (item.relation != null) relation = item.relation;
            if (item.language != null) language = item.language;
            if (item.coverage != null) coverage = item.coverage;
            if (item.publisher != null) publisher = item.publisher;
            if (item.title != null) title = item.title;
            if (item.description != null) description = item.description;

            if (item.creator != null) creator.Union(item.creator);
            if (item.identifier != null) identifier.Union(item.identifier);
            if (item.type != null) type.Union(item.type);
            if (item.date != null) date.Union(item.date);
            if (item.subject != null) subject.Union(item.subject);
            if (item.ReferencedBy != null) ReferencedBy.Union(item.ReferencedBy);
            if (item.ReferenceIndexes != null) ReferenceIndexes.Union(item.ReferenceIndexes);

            item.UpdateDoi();

        }

        protected void UpdateDoi()
        {
            var doiList = identifier.Where(dcVal =>
            {
                if (dcVal.Value.ToLowerInvariant().Contains("uid"))
                {
                    return true;
                }
                return false;
            }).ToList();
            Doi = doiList[0].Value.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
        }

        public override void AssignIdentity()
        {
            Id = ComputeIdentity().ToString();
        }

        public virtual string RawText { get; protected set; }

        public virtual string Doi { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> creator { get; protected set; }

        public virtual string relation { get; protected set; }
        public virtual string language { get; protected set; }
        public virtual string coverage { get; protected set; }
        public virtual string publisher { get; protected set; }
        public virtual string title { get; protected set; }
        public virtual string description { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> identifier { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> type { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> date { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> subject { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> ReferencedBy { get; protected set; }
        public virtual Iesi.Collections.Generic.ISet<DcValue> ReferenceIndexes { get; protected set; }

        public static IEnumerable<dc> FromResponse(Jstor.Domain.Helpers.searchRetrieveResponse searchRetrieveResponse)
        {
            var records = searchRetrieveResponse.records;
            var query = from record in records
                        from data in record.recordData
                        select data.dc;
            return query.AsEnumerable();
        }

        public static dc TestDc1()
        {
            var dc1 = new dc
            {
                title = "Test Title",
                coverage = "dc1coverage"
            };
            dc1.creator = new OrderedSet<DcValue>();
            dc1.creator.Add(new DcValue("Aaa, J"));
            dc1.creator.Add(new DcValue("Bbb, K"));

            return dc1;
        }

        public static dc TestDc2()
        {
            var dc2 = new dc
            {
                title = "Test :,. Title",
                description = "dc2description"
            };
            dc2.creator = new OrderedSet<DcValue>();
            dc2.creator.Add(new DcValue("Bbb  K"));
            dc2.creator.Add(new DcValue("Aaa.. .nn .n .. J"));
            return dc2;
        }

        #region IDcProducer Members

        public IEnumerable<dc> ProduceDc()
        {
            yield return this;
        }

        #endregion
    }

    public class DoiHolder : RecordData, IDcProducer
    {
        public DoiHolder() { }
        public DoiHolder(string doi) { Doi = doi; }

        public virtual string Doi { get; protected set; }

        public override object ComputeIdentity()
        {
            return Doi;
        }

        #region IDcProducer Members

        public IEnumerable<dc> ProduceDc()
        {
            Contract.Ensures(null != null);
            throw new NotImplementedException();
        }

        #endregion
    }

}
