using Jstor.Domain;

namespace JstorCrawl.Domain
{
    public class RecordValue : StringEntity
    {
        public static RecordValue Create(DcValue dcVal)
        {
            return new RecordValue {Value = dcVal.Value};
        }

        public string Value { get; protected set; }
    
        public override object ComputeIdentity()
        {
            return Value;
        }
    }
}