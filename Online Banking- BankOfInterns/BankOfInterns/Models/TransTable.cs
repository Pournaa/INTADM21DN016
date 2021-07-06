//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankOfIntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class TransTable
    {
        [Display(Name = "AccountNo")]
        [Required(ErrorMessage = "This field is Required")]
        public Nullable<decimal> AccNo { get; set; }
        public Nullable<decimal> FromAccNo { get; set; }
        [Display(Name = "ToAccountNo")]
        [Required(ErrorMessage = "This field is Required")]
        public Nullable<decimal> ToAccNo { get; set; }
        public Nullable<decimal> BeneficiaryID { get; set; }
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "This field is Required")]
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime TransDate { get; set; }
        public System.TimeSpan TransTime { get; set; }
        public string TransType { get; set; }
        public decimal TransactionID { get; set; }
        [Display(Name = "Comments")]

        public string Remarks { get; set; }
        public virtual AccountMaster AccountMaster { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }
    }
}
