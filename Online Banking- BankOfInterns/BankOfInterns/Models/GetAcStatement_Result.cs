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
    
    public partial class GetAcStatement_Result
    {
        public Nullable<decimal> AccNo { get; set; }
        public string AccHolder { get; set; }
        public string TransType { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}