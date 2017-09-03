namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1221312 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 50),
                        SubTitle = c.String(maxLength: 50),
                        Content = c.String(nullable: false),
                        AccessType = c.Int(nullable: false),
                        FreeDate = c.DateTime(),
                        Suffix = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Model = c.String(maxLength: 10),
                        Vendor = c.String(maxLength: 20),
                        UserAgent = c.String(maxLength: 500),
                        Imei = c.String(maxLength: 50),
                        DeviceType = c.Int(nullable: false),
                        AppVersion = c.String(maxLength: 50),
                        PlusVersion = c.String(maxLength: 50),
                        OsVersion = c.String(maxLength: 50),
                        NetType = c.Int(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.RechargeLogs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        DeviceID = c.String(nullable: false, maxLength: 50),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(nullable: false),
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
                "dbo.UserCategories",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
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
                        LastRechargeDate = c.DateTime(),
                        ExpiryDate = c.DateTime(),
                        LastLoginDate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        RecordState = c.Int(nullable: false),
                        Operator = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfos");
            DropTable("dbo.UserCategories");
            DropTable("dbo.Updates");
            DropTable("dbo.UpdateLogs");
            DropTable("dbo.RechargeLogs");
            DropTable("dbo.FeeBacks");
            DropTable("dbo.Devices");
            DropTable("dbo.Contents");
        }
    }
}
