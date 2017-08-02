using Ldap;
using Microsoft.Win32;
using Models.AD;

namespace Logic
{
    public static class AccountManagment
    {
        private static readonly LdapManager LdapManager = new LdapManager();

        public static bool ChangePassword(string username, string newPassword1, string newPassword2)
        {
            if(newPassword1 == newPassword2)
                return LdapManager.ChangePassword(username, newPassword1);

            return false;
        }

    }
}
