using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace KBA.TE.Models
{
    public class ADHelper
    {
        public static bool isMember(string groupName)
        {
            bool result = false;
            PrincipalContext pc = new PrincipalContext(ContextType.Domain);
            GroupPrincipal gp = new GroupPrincipal(pc);

            gp.Name = groupName;

            PrincipalSearcher search = new PrincipalSearcher(gp);

            PrincipalSearchResult<Principal> groups = search.FindAll();
            foreach(Principal group in groups)
            {
                if(UserPrincipal.Current.IsMemberOf((GroupPrincipal)group)) {
                    result = true;
                }
            }
            return result;
        }
    }
}