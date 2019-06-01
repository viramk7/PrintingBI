namespace PrintingBI.Common.Models
{
    public class ClaimModel
    {
        public ClaimModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
