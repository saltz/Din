using System.Collections.Generic;

namespace Models.AD
{
    public class ADUser : ADObject
    {
        public List<AdGroup> Groups { get; private set; }
        public string UserPrincipalName { get; private set; }

        public ADUser(string cn, string distinguishedname,
            string name, string objectCategory, string sAMAAccountName, List<AdGroup> memberOf,
            string userPrincipalName) : base(cn,
            distinguishedname, name, objectCategory, sAMAAccountName)
        {
            Groups = memberOf;
            UserPrincipalName = userPrincipalName;
        }
    }
}
