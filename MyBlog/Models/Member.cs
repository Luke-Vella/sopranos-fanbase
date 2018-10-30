using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Member
    {
        [Display(Name ="Id")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Username Required")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Minimum 6 characters required")]
        public string Password{ get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirm Password and Password do not match")]
        public string ConfirmPassword { get; set; }

        public bool IsEmailVerified { get; set; }

        public System.Guid ActivationCode { get; set; }
    }
}