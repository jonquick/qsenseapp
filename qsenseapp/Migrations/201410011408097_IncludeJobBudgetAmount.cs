namespace qsenseapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludeJobBudgetAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Job", "BudgetHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Job", "BudgetHours");
        }
    }
}
