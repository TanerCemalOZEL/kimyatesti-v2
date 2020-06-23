using kimyatesti.identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mail adresinizi yazınız.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifrenizi yazınız.")]
        public string Password { get; set; }
    }
    public class Register
    {
        //[Required(ErrorMessage ="Kullanıcı adı giriniz.")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Email adresinizi giriniz.")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Şifre giriniz.")]
        [StringLength(100, ErrorMessage = "Şifreniz en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Adınızı yazınız.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyadınızı yazınız.")]
        public string Surname { get; set; }
    }

    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email adresinizi giriniz.")]
        public string Username { get; set; }
    }
    public class ForgotPassword2
    {
        [Required(ErrorMessage = "Şifre giriniz.")]
        [StringLength(100, ErrorMessage = "Şifreniz en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }


    public class RoleUpdateModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}