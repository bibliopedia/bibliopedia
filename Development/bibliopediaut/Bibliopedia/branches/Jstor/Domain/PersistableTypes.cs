using FluentNHibernate.Data;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using Data.Extensions;
using Data;

namespace Jstor.Domain.Helpers
{
    public class searchRetrieveResponse 
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

    public class searchRetrieveResponseRecordsRecord 
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

    public class searchRetrieveResponseRecordsRecordRecordData 
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
    public class NewDataSet
    {
        public NewDataSet() { }

        public NewDataSet(JstorService.NewDataSet other)
        {
            Items = Jstor.Domain.Helpers.searchRetrieveResponse.FromArray(other.Items);
        }

        public virtual IList<Jstor.Domain.Helpers.searchRetrieveResponse> Items { get; set; }
    }
    public class searchRetrieveResponseEchoedSearchRetrieveRequest
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
}

