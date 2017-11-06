using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using Ldap.Connectors;
using Models.AD;
using SearchScope = System.DirectoryServices.Protocols.SearchScope;

namespace Ldap
{
    public class LdapManager
    {
        private readonly LDAPConnector _connector;
        private NetworkCredential _credential;
        private const string Domain = "din.nl";

        public LdapManager()
        {
            _connector = new LDAPConnector(new LdapConf());
        }

        public Tuple<bool, AdObject> AuthenticateUser(string username, string password)
        {
            if (ValidateUserByBind(new NetworkCredential(username, password, Domain)))
            {
                try
                {
                    var request = new SearchRequest("DC=din,DC=nl", "(objectClass=user)", SearchScope.Subtree,
                        null);
                    var response = (SearchResponse)_connector.Connection(_credential).SendRequest(request);
                    if (response != null)
                        foreach (SearchResultEntry e in response.Entries)
                        {
                            if (e.Attributes["sAmAccountName"][0].ToString().ToLower().Contains(username.ToLower()))
                            {
                                List<AdGroup> groups = new List<AdGroup>();
                                for (var index = 0; index < e.Attributes["memberOf"].Count; index++)
                                {
                                    var g = e.Attributes["memberOf"][index].ToString();
                                    groups.Add(new AdGroup(g));
                                }

                                AdUser user = new AdUser(
                                    e.Attributes["cn"][0].ToString(),
                                    e.Attributes["distinguishedName"][0].ToString(),
                                    e.Attributes["name"][0].ToString(),
                                    e.Attributes["objectCategory"][0].ToString(),
                                    e.Attributes["sAMAccountName"][0].ToString(),
                                    groups,
                                    e.Attributes["userPrincipalName"][0].ToString());

                                return new Tuple<bool, AdObject>(true, user);
                            }
                        }
                    return new Tuple<bool, AdObject>(true, null);
                }
                catch (NullReferenceException)
                {
                    //request went wrong
                    return null;
                }
            }
            else
            {
                //unsuccesful login attempt
                return new Tuple<bool, AdObject>(false, null);
            }
        }

        public bool ChangePassword(string username, string newpassword)
        {
            const AuthenticationTypes authenticationTypes = AuthenticationTypes.Secure |
                                                            AuthenticationTypes.Sealing | AuthenticationTypes.ServerBind;
            DirectoryEntry searchRoot = null;
            DirectorySearcher searcher = null;
            DirectoryEntry userEntry = null;

            try
            {
                searchRoot = new DirectoryEntry(String.Format("LDAP://{0}/{1}",
                        "Newton", "DC=din,DC=nl"),
                    "padmin", "PASSWORDRESETTER12", authenticationTypes);

                searcher = new DirectorySearcher(searchRoot);
                searcher.Filter = String.Format("sAMAccountName={0}", username);
                searcher.SearchScope = (System.DirectoryServices.SearchScope)SearchScope.Subtree;
                searcher.CacheResults = false;

                SearchResult searchResult = searcher.FindOne();
                if (searchResult == null) return false;

                userEntry = searchResult.GetDirectoryEntry();

                userEntry.Invoke("SetPassword", new object[] { newpassword });
                userEntry.CommitChanges();

                //password has been changed
                return true;
            }
            catch (Exception e)
            {
                var exception = e;
                //somethng went wrong password had not been chnaged
                return false;
            }
            finally
            {
                userEntry?.Dispose();
                searcher?.Dispose();
                searchRoot?.Dispose();
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
