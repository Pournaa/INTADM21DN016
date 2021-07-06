using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankOfIntern.Models
{
    public class EnterOtp
    {
        [Required]
        public string OTP { get; set; }
        public string OtpErrorMsg { get;  set; }
    }
}