namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeeBacks",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Mac = c.String(nullable: false, maxLength: 50),
                        Images = c.String(),
                        Contact = c.String(maxLength: 20),
                        Question = c.String(maxLength: 50),
                        Star = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UpdateLogs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Version = c.String(nullable: false, maxLength: 50),
                        Mac = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Updates",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Version = c.String(nullable: false, maxLength: 50),
                        AppName = c.String(maxLength: 50),
                        AppId = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        Note = c.String(nullable: false, maxLength: 500),
                        DowloadUrl = c.String(nullable: false, maxLength: 500),
                        DowloadCount = c.Int(nullable: false),
                        Size = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInfos",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        CategoryID = c.Short(nullable: false),
                        Name = c.String(maxLength: 10),
                        Gender = c.Int(nullable: false),
                        PhoneNumber = c.String(maxLength: 20),
                        Province = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Region = c.String(maxLength: 50),
                        Town = c.String(maxLength: 50),
                        Village = c.String(maxLength: 50),
                        Address = c.String(maxLength: 100),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastRechargeDate = c.DateTime(nullable: false),
                        ExpiryDate = c.String(),
                        LastLoginDate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Devices", "Model", c => c.String(maxLength: 10));
            AddColumn("dbo.Devices", "Imei", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "DeviceType", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "AppVersion", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "PlusVersion", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "OsVersion", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "Net", c => c.String(maxLength: 50));
            DropColumn("dbo.Devices", "CategoryID");
            DropColumn("dbo.Devices", "Name");
            DropColumn("dbo.Devices", "Gender");
            DropColumn("dbo.Devices", "PhoneNumber");
            DropColumn("dbo.Devices", "Province");
            DropColumn("dbo.Devices", "City");
            DropColumn("dbo.Devices", "Region");
            DropColumn("dbo.Devices", "Town");
            DropColumn("dbo.Devices", "Village");
            DropColumn("dbo.Devices", "Address");
            DropColumn("dbo.Devices", "Amount");
            DropColumn("dbo.Devices", "LastRechargeDate");
            DropColumn("dbo.Devices", "ExpiryDate");
            DropColumn("dbo.Devices", "LastLoginDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "LastLoginDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Devices", "ExpiryDate", c => c.String());
            AddColumn("dbo.Devices", "LastRechargeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Devices", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Devices", "Address", c => c.String(maxLength: 100));
            AddColumn("dbo.Devices", "Village", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "Town", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "Region", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "Province", c => c.String(maxLength: 50));
            AddColumn("dbo.Devices", "PhoneNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.Devices", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "Name", c => c.String(maxLength: 10));
            AddColumn("dbo.Devices", "CategoryID", c => c.Short(nullable: false));
            DropColumn("dbo.Devices", "Net");
            DropColumn("dbo.Devices", "OsVersion");
            DropColumn("dbo.Devices", "PlusVersion");
            DropColumn("dbo.Devices", "AppVersion");
            DropColumn("dbo.Devices", "DeviceType");
            DropColumn("dbo.Devices", "Imei");
            DropColumn("dbo.Devices", "Model");
            DropTable("dbo.UserInfos");
            DropTable("dbo.Updates");
            DropTable("dbo.UpdateLogs");
            DropTable("dbo.FeeBacks");
        }
    }
}
