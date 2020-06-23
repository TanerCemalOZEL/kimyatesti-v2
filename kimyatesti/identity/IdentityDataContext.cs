using kimyatesti.identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Identity
{
    public class IdentityDataContext: IdentityDbContext<AppUser>
    {
        public IdentityDataContext():base("identityConnection")
        {

        }
    }
}