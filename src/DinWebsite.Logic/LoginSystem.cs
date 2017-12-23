using System;
using DinWebsite.ExternalModels.AD;
using DinWebsite.ExternalModels.Exceptions;
using DinWebsite.Ldap;

namespace DinWebsite.Logic
{
    public static class LoginSystem
    {
        private static readonly LDAPManager LdapManager = new LDAPManager();

        public static Tuple<bool, ADObject> Login(string username, string password)
        {
            if (username == "kodi")
                throw new LoginException("This account does not have the required permissions");

            var ldapresult = LdapManager.AuthenticateUser(username, password);
            return ldapresult.Item1
                ? new Tuple<bool, ADObject>(true, ldapresult.Item2)
                : new Tuple<bool, ADObject>(false, null);
        }
    }
}