namespace qsenseapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityModel001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ActivityID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        HasAppointment = c.Boolean(nullable: false),
                        AppointmentID = c.String(),
                        StartOn = c.DateTime(),
                        FinishOn = c.DateTime(),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.ActivityID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Activity");
        }
    }
}
