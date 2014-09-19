using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace HIMS.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterationModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public enum enLookupType
    {
        Global=1,
        Brand=2,
        BoardResolution=3,
        City=4,
        Country=5,
        Region=6,
        CustomerType=7,
        DueDiligenceStatus=8,
        PlanningStatus=9,
        ProjectReason=10,
        ProjectStatus=11,
        ProjectModel=12,
        ProjectType=13,
        General01=14,
        YNLIMITED=15,
        YN=16,
        FnB=17       
    }
    public enum enUserType
    {
        HospitalAdmin = 1,
        HospitalManager = 2,
        Nurse = 3,
        Physician = 4,
        Technician = 5 
    }
    public enum enUserTitle
    {
        Mr = 1,
        Mrs = 2,
        Ms = 3,
        Dr = 4
    }

    public static class EnumHelper
    {
        public static SelectList GetSelectList<TEnum>()
        {
            List<SelectListItem> enumList = new List<SelectListItem>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (TEnum data in Enum.GetValues(typeof(TEnum)))
            {
                dictionary.Add(((int)Enum.Parse(typeof(TEnum), data.ToString())).ToString(), data.ToString().Replace("_", " "));                
            }
            SelectList slnumList = new SelectList(dictionary, "value", "key");
            return slnumList;
        }
        public static Dictionary<string, string> GetDictionaryList<TEnum>()
        {
            List<SelectListItem> enumList = new List<SelectListItem>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (TEnum data in Enum.GetValues(typeof(TEnum)))
            {
                dictionary.Add(((int)Enum.Parse(typeof(TEnum), data.ToString())).ToString(), data.ToString().Replace("_", " "));
            }

            return dictionary;
        }



    }

}
