using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmerApp.Models
{
    public class Soldhistory
    {
        [Key]
        public int Sold_ID { get; set; }
        public System.DateTime Date { get; set; }
        public int Crop_ID { get; set; }
        public string Crop_name { get; set; }
        public double Quantity { get; set; }
        public double Baseprice { get; set; }
        public double Soldprice { get; set; }
        public double Totalprice { get; set; }

    }
}