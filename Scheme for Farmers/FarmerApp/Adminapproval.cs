
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace FarmerApp
{

using System;
    using System.Collections.Generic;
    
public partial class Adminapproval
{

    public int Farmer_ID { get; set; }

    public int Crop_ID { get; set; }

    public string Status { get; set; }

    public Nullable<int> Bidder_ID { get; set; }

    public int Adminapp_ID { get; set; }



    public virtual BidderReg BidderReg { get; set; }

    public virtual SellRequest SellRequest { get; set; }

    public virtual FarmerReg FarmerReg { get; set; }

}

}
