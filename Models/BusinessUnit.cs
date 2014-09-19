using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Models
{
    public class BusinessUnit
    {
        [Key]       
        public int BUS_ID { get; set; }        
        [Required]
        [MaxLength(50, ErrorMessage = "Business Unit Name must be 50 characters or less"), MinLength(2)]
        [Display(Name = "Business Unit Name")]
        public string BUSINESS_UNIT { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Effective Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EFF_DATE { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Eff Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EFFF_DATE { get; set; }
        
        [Display(Name = "Status")]
        public bool EFF_STATUS { get; set; }
        
        [MaxLength(75, ErrorMessage = "Full Name must be 75 characters or less"), MinLength(2)]
        [Display(Name = "Full Name")]
        public string BUS_FULL_NAME { get; set; }

        [MaxLength(75, ErrorMessage = "Nick Name must be 75 characters or less"), MinLength(0)]
        [Display(Name = "Nick Name")]
        public string NAME2 { get; set; }

        [MaxLength(100, ErrorMessage = "Address1 must be 100 characters or less"), MinLength(2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address1")]
        public string ADDRESS1 { get; set; }

        [MaxLength(100, ErrorMessage = "Address2 must be 100 characters or less"), MinLength(0)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address2")]
        public string ADDRESS2 { get; set; }

        [MaxLength(100, ErrorMessage = "Address3 must be 100 characters or less"), MinLength(0)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address3")]
        public string ADDRESS3 { get; set; }

        [MaxLength(50, ErrorMessage = "City must be 50 characters or less"), MinLength(2)]
        [Display(Name = "City")]
        public string CITY { get; set; }
        
        [MaxLength(15, ErrorMessage = "Zip Code must be 15 characters or less"), MinLength(2)]
        [Display(Name = "Zip Code")]
        public string POSTAL { get; set; }

        [MaxLength(50, ErrorMessage = "State must be 50 characters or less"), MinLength(2)]
        [Display(Name = "State")]
        public string STATE { get; set; }

        [MaxLength(50, ErrorMessage = "Country Name must be 50 characters or less"), MinLength(2)]
        [Display(Name = "Country Name")]
        public string COUNTRY { get; set; }

        [MaxLength(20, ErrorMessage = "Phone must be 20 characters or less"), MinLength(2)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PHONE { get; set; }

        [MaxLength(75, ErrorMessage = "Contact Person must be 75 characters or less"), MinLength(2)]
        [Display(Name = "Contact Person")]
        public string CONTACT_NAME { get; set; }

        [MaxLength(15, ErrorMessage = "Contact Type must be 15 characters or less"), MinLength(0)]
        [Display(Name = "Contact Type")]
        public string CONTACT_TYPE { get; set; }

        [MaxLength(10, ErrorMessage = "Title must be 10 characters or less"), MinLength(2)]
        [Display(Name = "Title")]
        public string CONTACT_TITLE { get; set; }
        
        [MaxLength(25, ErrorMessage = "Email Id must be 25 characters or less"), MinLength(2)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Id")]
        public string EMAILID { get; set; }

        [MaxLength(15, ErrorMessage = "Preferred Language must be 15 characters or less"), MinLength(0)]
        [Display(Name = "Preferred Language")]
        public string PREFERRED_LANGUAGE { get; set; }

        [MaxLength(25, ErrorMessage = "Url must be 25 characters or less"), MinLength(0)]
        [DataType(DataType.Url)]
        [Display(Name = "Url")]
        public string BUS_URL { get; set; }

        [MaxLength(200, ErrorMessage = "Comments must be 200 characters or less"), MinLength(0)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string BUS_COMMENTS { get; set; }

        [MaxLength(75, ErrorMessage = "Field1 must be 75 characters or less"), MinLength(0)]
        [Display(Name = "Field1")]
        public string BUS_UNIT_FIELD1 { get; set; }

        [MaxLength(75, ErrorMessage = "Field2 must be 75 characters or less"), MinLength(0)]
        [Display(Name = "Field2")]
        public string BUS_UNIT_FIELD2 { get; set; }

        [MaxLength(75, ErrorMessage = "Field3 must be 75 characters or less"), MinLength(0)]
        [Display(Name = "Field3")]
        public string BUS_UNIT_FIELD3 { get; set; }

        [MaxLength(75, ErrorMessage = "Field4 must be 75 characters or less"), MinLength(0)]
        [Display(Name = "Field4")]
        public string BUS_UNIT_FIELD4 { get; set; }

        [MaxLength(75, ErrorMessage = "Field5 must be 75 characters or less"), MinLength(0)]
        [Display(Name = "Field5")]
        public string BUS_UNIT_FIELD5 { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public DateTime? CREATION_DATE { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Modify Date")]
        public DateTime? MODIFY_DATE { get; set; }

        public SelectList ContactTitleList;
        
        
    }

    
}