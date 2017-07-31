namespace Models.AD
{
    public class AdGroup : AdObject
    {
        public string Description { get; private set; }
        public AdGroup(string cn, string distinguishedname, string name, string objectCategory, string sAmaAccountName,
            string description) : base(cn, distinguishedname, name, objectCategory, sAmaAccountName)
        {
            Description = description;
        }

        public AdGroup(string distinguishedname) : base(distinguishedname)
        {
        }
    }
}
