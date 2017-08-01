using Ldap;
using Microsoft.Win32;
using Models.AD;

namespace Logic
{
    public static class AccountManagment
    {
        private static readonly LdapManager LdapManager = new LdapManager();

        public static bool ChangePassword(string username, string newpassword)
        {
            return LdapManager.ChangePassword(username, newpassword);
        }

    }
}
