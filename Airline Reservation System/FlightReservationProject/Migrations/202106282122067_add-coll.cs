namespace FlightReservationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ticket", "address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ticket", "address", c => c.String());
        }
    }
}
