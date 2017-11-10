namespace Models.AD
{
    public class AdGroup : ADObject
    {
        private string Description { get; }
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
