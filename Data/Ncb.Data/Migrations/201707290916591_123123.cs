namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123123 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfos", "LastRechargeDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfos", "LastRechargeDate", c => c.DateTime(nullable: false));
        }
    }
}
