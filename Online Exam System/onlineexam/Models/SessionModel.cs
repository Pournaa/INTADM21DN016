﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineexam.Models
{
    public class SessionModel
    {
        public int TestId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile_no { get; set; }
    }
}