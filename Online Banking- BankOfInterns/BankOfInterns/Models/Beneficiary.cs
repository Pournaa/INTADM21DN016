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
    
    public partial class Beneficiary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Beneficiary()
        {
            this.TransTables = new HashSet<TransTable>();
        }

        [Display(Name = "AccountNumber")]
        [Required(ErrorMessage = "This field is Required")]
        public Nullable<decimal> AccNo { get; set; }
        [Display(Name = "BeneficiaryAccountNo")]
        [Required(ErrorMessage = "This field is Required")]
        public Nullable<decimal> BenAccNo { get; set; }
        [Display(Name = "BeneficiaryName")]
        [Required(ErrorMessage = "This field is Required")]
        public string BenName { get; set; }
        [Display(Name = "NickName")]
        public string NickName { get; set; }
        [Display(Name = "BeneficiaryBankName")]
        [Required(ErrorMessage = "This field is Required")]
        public string BenBankName { get; set; }
        [Display(Name = "IFSCCode")]
        [Required(ErrorMessage = "This field is Required")]
        public string IFSCCode { get; set; }
        public decimal BeneficiaryID { get; set; }
        [Display(Name = "BeneficiaryBankType")]
        [Required(ErrorMessage = "This field is Required")]
        public string BeneficiaryType { get; set; }
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public virtual AccountMaster AccountMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransTable> TransTables { get; set; }
    }
}
