namespace qsenseapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebsiteAddedToBusinessPartner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessPartner", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BusinessPartner", "Website");
        }
    }
}
