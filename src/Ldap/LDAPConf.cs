using System.DirectoryServices.Protocols;

namespace Ldap
{
    public class LDAPConf
    {
        //private ConnectMode _mode;
        private readonly LdapDirectoryIdentifier _server;

        public LDAPConf()
        {
            this._server = new LdapDirectoryIdentifier("localhost");
        }

        public LdapDirectoryIdentifier GetServer()
        {
            return _server;
        }
    }
}
