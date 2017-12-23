using System.Collections.Generic;

namespace DinWebsite.ExternalModels.AD
{
    public class ADUser : ADObject
    {
        public ADUser(string cn, string distinguishedname,
            string name, string objectCategory, string sAMAAccountName, List<AdGroup> memberOf,
            string userPrincipalName) : base(cn,
            distinguishedname, name, objectCategory, sAMAAccountName)
        {
            Groups = memberOf;
            UserPrincipalName = userPrincipalName;
        }

        public List<AdGroup> Groups { get; }
        public string UserPrincipalName { get; }
    }
}