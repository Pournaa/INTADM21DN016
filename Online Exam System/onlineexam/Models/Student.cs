//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onlineexam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Registrations = new HashSet<Registration>();
        }
    
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter your Name")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your Email")]
        public string Email { get; set; }
       
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the Password")]

        public string User_Password { get; set; }
        
        public string LoginErrorMsg { get; set; }
        [Required(ErrorMessage = "Please enter the City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter the state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter the postal code")]
        public Nullable<int> Postal_Code { get; set; }
        [Required(ErrorMessage = "Please enter the date of birth")]
        public Nullable<System.DateTime> DOB { get; set; }
        [Required(ErrorMessage = "Please enter the mobile number")]
        public string Mobile_no { get; set; }
        [Required(ErrorMessage = "Please enter your qualification")]
        public string Qualification { get; set; }
       
        public Nullable<System.DateTime> Year_of_Completion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}