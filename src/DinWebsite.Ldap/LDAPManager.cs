using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using DinWebsite.ExternalModels.AD;
using DinWebsite.Ldap.Connectors;
using SearchScope = System.DirectoryServices.Protocols.SearchScope;

namespace DinWebsite.Ldap
{
    public class LDAPManager
    {
        private const string Domain = "din.nl";
        private readonly string _accOperatorName = File.ReadLines("C:/din_properties/acc_operator").First();
        private readonly string _accOperatorPwd = File.ReadLines("C:/din_properties/acc_operator").ElementAt(1);
        private readonly LdapConnector _connector;
        private NetworkCredential _credential;

        public LDAPManager()
        {
            _connector = new LdapConnector(new LdapConf());
        }

        public Tuple<bool, ADObject> AuthenticateUser(string username, string password)
        {
            if (ValidateUserByBind(new NetworkCredential(username, password, Domain)))
                try
                {
                    var request = new SearchRequest("DC=din,DC=nl", "(objectClass=user)", SearchScope.Subtree,
                        null);
                    var response = (SearchResponse) _connector.Connection(_credential).SendRequest(request);
                    if (response != null)
                        foreach (SearchResultEntry e in response.Entries)
                            if (e.Attributes["sAmAccountName"][0].ToString().ToLower().Contains(username.ToLower()))
                            {
                                var groups = new List<AdGroup>();
                                for (var index = 0; index < e.Attributes["memberOf"].Count; index++)
                                {
                                    var g = e.Attributes["memberOf"][index].ToString();
                                    groups.Add(new AdGroup(g));
                                }

                                var user = new ADUser(
                                    e.Attributes["cn"][0].ToString(),
                                    e.Attributes["distinguishedName"][0].ToString(),
                                    e.Attributes["name"][0].ToString(),
                                    e.Attributes["objectCategory"][0].ToString(),
                                    e.Attributes["sAMAccountName"][0].ToString(),
                                    groups,
                                    e.Attributes["userPrincipalName"][0].ToString());

                                return new Tuple<bool, ADObject>(true, user);
                            }
                    return new Tuple<bool, ADObject>(true, null);
                }
                catch (NullReferenceException)
                {
                    //request went wrong
                    return null;
                }
            return new Tuple<bool, ADObject>(false, null);
        }

        public bool ChangePassword(string username, string newpassword)
        {
            const AuthenticationTypes authenticationTypes = AuthenticationTypes.Secure |
                                                            AuthenticationTypes.Sealing |
                                                            AuthenticationTypes.ServerBind;
            DirectoryEntry searchRoot = null;
            DirectorySearcher searcher = null;
            DirectoryEntry userEntry = null;

            try
            {
                searchRoot = new DirectoryEntry(string.Format("LDAP://{0}/{1}",
                        "Newton", "DC=din,DC=nl"),
                    _accOperatorName, _accOperatorPwd, authenticationTypes);

                searcher = new DirectorySearcher(searchRoot);
                searcher.Filter = string.Format("sAMAccountName={0}", username);
                searcher.SearchScope = (System.DirectoryServices.SearchScope) SearchScope.Subtree;
                searcher.CacheResults = false;

                var searchResult = searcher.FindOne();
                if (searchResult == null) return false;

                userEntry = searchResult.GetDirectoryEntry();

                userEntry.Invoke("SetPassword", newpassword);
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
            var result = true;
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