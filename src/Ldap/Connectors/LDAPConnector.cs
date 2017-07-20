using System.DirectoryServices.Protocols;
using System.Net;

namespace Ldap.Connectors
{
    public class LDAPConnector
    {
        private LdapConnection _ldapConnection;
        private readonly LDAPConf _ldapConfiguration;

        
        public LDAPConnector(LDAPConf conf)
        {
            this._ldapConfiguration = conf;
        }

        public LdapConnection Connection()
        {
           return _ldapConnection = new LdapConnection(_ldapConfiguration.GetServer());
        }

        public LdapConnection Connection(NetworkCredential cred)
        {
            return _ldapConnection = new LdapConnection(_ldapConfiguration.GetServer(), cred);
        }
    }
}
