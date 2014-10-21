namespace qsenseapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityModel003 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Activity", "JobID");
            AddForeignKey("dbo.Activity", "JobID", "dbo.Job", "JobID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activity", "JobID", "dbo.Job");
            DropIndex("dbo.Activity", new[] { "JobID" });
        }
    }
}
