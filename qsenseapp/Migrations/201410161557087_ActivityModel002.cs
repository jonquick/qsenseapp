namespace qsenseapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityModel002 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activity", "StartOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Activity", "FinishOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activity", "FinishOn", c => c.DateTime());
            AlterColumn("dbo.Activity", "StartOn", c => c.DateTime());
        }
    }
}
