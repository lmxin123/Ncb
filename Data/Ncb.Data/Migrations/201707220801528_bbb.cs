namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bbb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contents", "Suffix", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contents", "Suffix", c => c.String());
        }
    }
}
