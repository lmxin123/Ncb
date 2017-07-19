namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "Suffix", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contents", "Suffix");
        }
    }
}
