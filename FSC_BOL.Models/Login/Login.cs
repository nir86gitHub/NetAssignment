using System;
using System.ComponentModel.DataAnnotations;

namespace FSC_BOL.Models.Login
{
    public class FSCLogin
    {
        public Int64 UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class FSCUserDetail
    {
        public Int64 UserId { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNo { get; set; }
        public string AlternateNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsAnonymous { get; set; }
        public string IsAdminPanel { get; set; }
        public string SignUpDate { get; set; }
        public string IsVerified { get; set; }
    }
}
