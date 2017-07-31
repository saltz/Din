﻿using System;
using Ldap;
using Models.Exceptions;
using Models.AD;

namespace Logic
{
    public static class LoginSystem
    {
        private static readonly LdapManager LdapManager = new LdapManager();
        public static Tuple<bool, AdObject> Login(string username, string password)
        {
            if (username == "kodi")
                throw new LoginException("This account does not have the required permissions");

            var ldapresult = LdapManager.AuthenticateUser(username, password);
            return ldapresult.Item1 ? new Tuple<bool, AdObject>(true, ldapresult.Item2) : new Tuple<bool, AdObject>(false, null);
        }
    }
}