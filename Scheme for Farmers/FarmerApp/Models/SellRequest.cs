using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FarmerApp.Models
{
    public class SellRequest
    {
        [Key]
        public int Crop_ID { get; set; }
        [Required]
        public string Crop_type { get; set; }
        [Required]
        public string Crop_name { get; set; }
        [Required]
        public string Fertilizer { get; set; }
        [Required]
        [Range(50,250,ErrorMessage ="Please Enter quantity between 50 and 250.")]
        public double Quantity { get; set; }
        [Required]
        public string Soil_PH { get; set; }
        [Required]
        public Nullable<double> Baseprice { get; set; }
        [Required]
        public Nullable<int> Farmer_ID { get; set; }
       
    }
}