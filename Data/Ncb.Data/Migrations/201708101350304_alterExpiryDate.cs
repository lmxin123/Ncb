namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterExpiryDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RechargeLogs", "ExpiryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserInfos", "ExpiryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfos", "ExpiryDate", c => c.String());
            AlterColumn("dbo.RechargeLogs", "ExpiryDate", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
