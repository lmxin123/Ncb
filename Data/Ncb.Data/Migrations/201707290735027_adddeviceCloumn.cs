namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddeviceCloumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Vendor", c => c.String(maxLength: 20));
            AddColumn("dbo.Devices", "NetType", c => c.Int(nullable: false));
            DropColumn("dbo.Devices", "Net");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "Net", c => c.String(maxLength: 50));
            DropColumn("dbo.Devices", "NetType");
            DropColumn("dbo.Devices", "Vendor");
        }
    }
}
