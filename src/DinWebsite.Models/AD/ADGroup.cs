namespace DinWebsite.ExternalModels.AD
{
    public class AdGroup : ADObject
    {
        public AdGroup(string cn, string distinguishedname, string name, string objectCategory, string sAmaAccountName,
            string description) : base(cn, distinguishedname, name, objectCategory, sAmaAccountName)
        {
            Description = description;
        }

        public AdGroup(string distinguishedname) : base(distinguishedname)
        {
        }

        private string Description { get; }
    }
}