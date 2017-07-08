namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1232 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contents", "Banner", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contents", "Banner", c => c.String());
        }
    }
}
