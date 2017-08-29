namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12312356 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contents", "FreeDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contents", "FreeDate", c => c.String(maxLength: 10));
        }
    }
}
