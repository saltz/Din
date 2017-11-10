namespace Models.AD
{
    public abstract class ADObject
    {
        public string CN { get; private set; }
        public string DistinguishedName { get; private set; }
        public string Name { get; private set; }
        public string ObjectCategory { get; private set; }
        public string SAMAccountName { get; private set; }

        protected ADObject(string cn, string distinguishedname, string name,
            string objectCategory, string sAMAAccountName)
        {
            this.CN = cn;
            this.DistinguishedName = distinguishedname;
            this.Name = name;
            this.ObjectCategory = objectCategory;
            this.SAMAccountName = sAMAAccountName;
        }

        protected ADObject(string distinguishedname)
        {
            DistinguishedName = distinguishedname;
        }
    } 
}
