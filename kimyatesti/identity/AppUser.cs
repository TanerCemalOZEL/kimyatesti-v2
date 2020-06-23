using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.identity
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}