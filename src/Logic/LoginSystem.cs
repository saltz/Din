using System;
using Ldap;
using Models.AD;

namespace Logic
{
    public class LoginSystem
    {
        private readonly LDAPManager _ldapManager = new LDAPManager();
        public Tuple<bool, ADObject> Login(string username, string password)
        {
            if (username == "kodi" || password == "kodi")
            {
                return new Tuple<bool, ADObject>(false, null);
            }
            Tuple<bool, ADObject> ldapresult = _ldapManager.AuthenticateUser(username, password);
            if (ldapresult.Item1)
            {
               return new Tuple<bool, ADObject>(true, ldapresult.Item2); 
            }
            return new Tuple<bool, ADObject>(false, null);
        }
    }
}
