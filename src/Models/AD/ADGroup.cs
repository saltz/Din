namespace Models.AD
{
    public class ADGroup : ADObject
    {
        public string Description { get; private set; }
        public ADGroup(string cn, string distinguishedname, string name, string objectCategory, string sAMAAccountName,
            string description) : base(cn, distinguishedname, name, objectCategory, sAMAAccountName)
        {
            this.Description = description;
        }

        public ADGroup(string distinguishedname) : base(distinguishedname)
        {
        }
    }
}
