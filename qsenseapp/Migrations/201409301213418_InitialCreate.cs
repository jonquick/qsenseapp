namespace qsenseapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessPartner",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ChargeRateHourNet = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        BusinessPartnerID = c.Int(nullable: false),
                        Name = c.String(),
                        JobType = c.Int(),
                        ChargeType = c.Int(),
                        ChargeRateHourNet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReceivedOn = c.DateTime(),
                        ReceivedBy = c.String(),
                        DueOn = c.DateTime(),
                        StartOn = c.DateTime(),
                        FinishNn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.JobID)
                .ForeignKey("dbo.BusinessPartner", t => t.BusinessPartnerID, cascadeDelete: true)
                .Index(t => t.BusinessPartnerID);
            
            CreateTable(
                "dbo.JobExpense",
                c => new
                    {
                        JobExpenseID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        ExpenseType = c.Int(nullable: false),
                        Name = c.String(),
                        StartOn = c.DateTime(),
                        FinishOn = c.DateTime(),
                        DurationFormatted = c.String(),
                        Quantity = c.Double(),
                        PriceNet = c.Double(),
                        ActualNet = c.Double(),
                        AdjustNet = c.Double(),
                        ValueNet = c.Double(),
                    })
                .PrimaryKey(t => t.JobExpenseID)
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobExpense", "JobID", "dbo.Job");
            DropForeignKey("dbo.Job", "BusinessPartnerID", "dbo.BusinessPartner");
            DropIndex("dbo.JobExpense", new[] { "JobID" });
            DropIndex("dbo.Job", new[] { "BusinessPartnerID" });
            DropTable("dbo.JobExpense");
            DropTable("dbo.Job");
            DropTable("dbo.BusinessPartner");
        }
    }
}
