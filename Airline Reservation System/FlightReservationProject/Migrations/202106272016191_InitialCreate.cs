namespace FlightReservationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        feedbackId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        name = c.String(nullable: false),
                        feed = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.feedbackId);
            
            CreateTable(
                "dbo.Flight",
                c => new
                    {
                        flightId = c.Int(nullable: false, identity: true),
                        flightName = c.String(nullable: false),
                        seatFirstclass = c.Int(nullable: false),
                        seatBusinessclass = c.Int(nullable: false),
                        seatEconomyclass = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.flightId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        scheduleId = c.Int(nullable: false, identity: true),
                        flightId = c.Int(nullable: false),
                        source = c.String(nullable: false),
                        sourceId = c.Int(nullable: false),
                        dest = c.String(nullable: false),
                        destId = c.Int(nullable: false),
                        scheduleDate = c.DateTime(nullable: false),
                        depatureTime = c.Time(nullable: false, precision: 7),
                        arrivalDate = c.DateTime(nullable: false),
                        arrivalTime = c.Time(nullable: false, precision: 7),
                        status = c.String(nullable: false),
                        FCseats = c.Int(nullable: false),
                        BCseats = c.Int(nullable: false),
                        ECseats = c.Int(nullable: false),
                        costFirst = c.Double(nullable: false),
                        costBusiness = c.Double(nullable: false),
                        costEconomy = c.Double(nullable: false),
                        traveltype = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.scheduleId)
                .ForeignKey("dbo.Flight", t => t.flightId)
                .Index(t => t.flightId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        ticketId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        flightId = c.Int(nullable: false),
                        scheduleId = c.Int(nullable: false),
                        paymentId = c.Int(nullable: false),
                        dateOfJourney = c.DateTime(nullable: false),
                        seatNo = c.Int(nullable: false),
                        passengerName = c.String(),
                        phoneNumber = c.String(),
                        address = c.String(),
                        emergencyContact = c.String(),
                        travelclass = c.String(),
                        nooftickets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ticketId)
                .ForeignKey("dbo.Flight", t => t.flightId)
                .ForeignKey("dbo.Payment", t => t.paymentId)
                .ForeignKey("dbo.Schedule", t => t.scheduleId)
                .Index(t => t.flightId)
                .Index(t => t.scheduleId)
                .Index(t => t.paymentId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        paymentId = c.Int(nullable: false, identity: true),
                        paymentMode = c.String(),
                        totalAmount = c.Double(nullable: false),
                        bankDetails = c.String(),
                    })
                .PrimaryKey(t => t.paymentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "scheduleId", "dbo.Schedule");
            DropForeignKey("dbo.Ticket", "paymentId", "dbo.Payment");
            DropForeignKey("dbo.Ticket", "flightId", "dbo.Flight");
            DropForeignKey("dbo.Schedule", "flightId", "dbo.Flight");
            DropIndex("dbo.Ticket", new[] { "paymentId" });
            DropIndex("dbo.Ticket", new[] { "scheduleId" });
            DropIndex("dbo.Ticket", new[] { "flightId" });
            DropIndex("dbo.Schedule", new[] { "flightId" });
            DropTable("dbo.Payment");
            DropTable("dbo.Ticket");
            DropTable("dbo.Schedule");
            DropTable("dbo.Flight");
            DropTable("dbo.Feedback");
        }
    }
}
