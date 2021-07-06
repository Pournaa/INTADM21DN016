namespace FlightReservationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_drop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ticket", "nooftickets");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ticket", "nooftickets", c => c.Int(nullable: false));
        }
    }
}
