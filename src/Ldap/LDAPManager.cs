using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text;
using Ldap.Connectors;
using Ldap.Enums;
using Models.AD;
using SearchScope = System.DirectoryServices.Protocols.SearchScope;

namespace Ldap
{
    public class LDAPManager
    {
        private readonly LDAPConnector _connector;
        private NetworkCredential _credential;
        private PermissionLevel _mode = PermissionLevel.Anonymous;

        private const string Domain = "din.nl";

        public LDAPManager()
        {
            this._connector = new LDAPConnector(new LDAPConf());
        }

        public Tuple<bool, ADObject> AuthenticateUser(string username, string password)
        {
            if (ValidateUserByBind(new NetworkCredential(username, password, Domain)))
            {
                try
                {
                    var request = new SearchRequest("DC=din,DC=nl", "(objectClass=user)", SearchScope.Subtree,
                        null);
                    var response = (SearchResponse)_connector.Connection(_credential).SendRequest(request);
                    foreach (SearchResultEntry e in response.Entries)
                    {
                        if (e.Attributes["sAmAccountName"][0].ToString().ToLower().Contains(username.ToLower()))
                        {
                            List<ADGroup> groups = new List<ADGroup>();
                            for (var index = 0; index < e.Attributes["memberOf"].Count; index++)
                            {
                                var g = e.Attributes["memberOf"][index].ToString();
                                groups.Add(new ADGroup(g));
                            }

                            ADUser user = new ADUser(
                                e.Attributes["cn"][0].ToString(),
                                e.Attributes["distinguishedName"][0].ToString(),
                                e.Attributes["name"][0].ToString(),
                                e.Attributes["objectCategory"][0].ToString(),
                                e.Attributes["sAMAccountName"][0].ToString(),
                                groups,
                                e.Attributes["userPrincipalName"][0].ToString());

                            return new Tuple<bool, ADObject>(true, user);
                        }
                    }
                    return new Tuple<bool, ADObject>(true, null);
                }
                catch (NullReferenceException e)
                {
                    //request went wrong
                    return null;
                }
            }
            else
            {
                //unsuccesful login attempt
                return new Tuple<bool, ADObject>(false, null);
            }
        }

        private bool ValidateUserByBind(NetworkCredential credentials)
        {
            bool result = true;
            try
            {
                _connector.Connection().Bind(credentials);
                _credential = credentials;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
