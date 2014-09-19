using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HIMS.Models
{
    public class RoleDefinition
    {
        [Key]
        public int ROLE_ID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Role Name must be 50 characters or less"), MinLength(2)]
        [Display(Name = "Role Name")]
        public string ROLE_Name { get; set; }

        [MaxLength(100, ErrorMessage = "Role Description must be 100 characters or less"), MinLength(2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address1")]
        public string ROLE_DESCRIPTION { get; set; }

    }
}