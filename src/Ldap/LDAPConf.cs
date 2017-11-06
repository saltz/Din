using System.DirectoryServices.Protocols;

namespace Ldap
{
    public class LdapConf
    {
        //private ConnectMode _mode;
        private readonly LdapDirectoryIdentifier _server;

        public LdapConf()
        {
            _server = new LdapDirectoryIdentifier("localhost");
        }

        public LdapDirectoryIdentifier GetServer()
        {
            return _server;
        }
    }
}
