using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FarmerApp.Models
{
    public class FarmerReg
    {
        [Key]
        public int Farmer_ID { get; set; }
        [Required(ErrorMessage = "Please Enter Name e.g. John Doe")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [Display(Name = "Mobile")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 Numbers", MinimumLength = 10)]
        public string Contact_no { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [Display(Name = "Address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Display(Name = "City")]
        [StringLength(35)]
        public string City { get; set; }


        [Display(Name = "State")]
        [StringLength(35)]
        public string State { get; set; }


        [Display(Name = "Pincode")]
        [StringLength(8, ErrorMessage = "Please enter a Valid Pincode", MinimumLength = 6)]
        public string Pincode { get; set; }


        [Required]
        [StringLength(200)]
        public double Area { get; set; }


        [Required]
        [StringLength(200)]
        public string Land_Address { get; set; }


        [Required]
        [DataType(DataType.PostalCode)]
        [StringLength(8, ErrorMessage = "Please enter a Valid Pincode", MinimumLength = 6)]
        public string Land_Pincode { get; set; }


        [Display(Name = "Accountno.")]
        [StringLength(16, ErrorMessage = "Please enter a Valid Account No.", MinimumLength = 11)]
        public string AccountNo { get; set; }


        [Required(ErrorMessage = "IFSC is Required")]
        [RegularExpression("[a-z|A-Z]{4}[0-9]{6}", ErrorMessage = "Invalid IFSC")]
        public string IFSC_code { get; set; }

        [Display(Name = "Aadhar Card")]
        [StringLength(12, ErrorMessage = "Please enter a Valid Aadhar Number", MinimumLength = 12)]
        public string Aadhaar { get; set; }

        [Required(ErrorMessage = "PAN is Required")]
        [RegularExpression("[a-z|A-Z]{4}[0-9]{6}", ErrorMessage = "Invalid PAN")]

        public string PAN { get; set; }

        [Required(ErrorMessage ="Please upload Certificate")]
        public string ImagePath { get; set; }
        //public string Certificate { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}