﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class FarmersEntities : DbContext
{
    public FarmersEntities()
        : base("name=FarmersEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Adminapproval> Adminapprovals { get; set; }

    public virtual DbSet<BidderReg> BidderRegs { get; set; }

    public virtual DbSet<Bidding> Biddings { get; set; }

    public virtual DbSet<FarmerReg> FarmerRegs { get; set; }

    public virtual DbSet<SellRequest> SellRequests { get; set; }

    public virtual DbSet<Soldhistory> Soldhistories { get; set; }

}

}

