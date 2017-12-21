using System.DirectoryServices.Protocols;
using System.Net;

namespace DinWebsite.Ldap.Connectors
{
    public class LdapConnector
    {
        private readonly LdapConf _ldapConfiguration;
        private LdapConnection _ldapConnection;


        public LdapConnector(LdapConf conf)
        {
            _ldapConfiguration = conf;
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