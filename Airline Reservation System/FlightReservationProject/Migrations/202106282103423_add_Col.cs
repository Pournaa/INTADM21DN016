namespace FlightReservationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Col : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedback", "feed", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedback", "feed", c => c.String(nullable: false));
        }
    }
}
