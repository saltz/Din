using System.DirectoryServices.Protocols;
using System.Net;

namespace DinWebsite.Ldap.Connectors
{
    public class LdapConnector
    {
        private LdapConnection _ldapConnection;
        private readonly LdapConf _ldapConfiguration;

        
        public LdapConnector(LdapConf conf)
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
