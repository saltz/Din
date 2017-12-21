namespace DinWebsite.ExternalModels.AD
{
    public abstract class ADObject
    {
        protected ADObject(string cn, string distinguishedname, string name,
            string objectCategory, string sAMAAccountName)
        {
            CN = cn;
            DistinguishedName = distinguishedname;
            Name = name;
            ObjectCategory = objectCategory;
            SAMAccountName = sAMAAccountName;
        }

        protected ADObject(string distinguishedname)
        {
            DistinguishedName = distinguishedname;
        }

        public string CN { get; }
        public string DistinguishedName { get; }
        public string Name { get; }
        public string ObjectCategory { get; }
        public string SAMAccountName { get; }
    }
}