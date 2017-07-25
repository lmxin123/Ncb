namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeeback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeeBacks",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Mac = c.String(nullable: false, maxLength: 50),
                        AppId = c.String(nullable: false, maxLength: 50),
                        Imei = c.String(maxLength: 50),
                        Images = c.String(),
                        Contact = c.String(maxLength: 20),
                        DeviceType = c.Int(nullable: false),
                        Model = c.String(maxLength: 50),
                        UserAgent = c.String(maxLength: 500),
                        AppVersion = c.String(maxLength: 50),
                        PlusVersion = c.String(maxLength: 50),
                        OsVersion = c.String(maxLength: 50),
                        Net = c.String(maxLength: 50),
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
                        AppId = c.String(nullable: false, maxLength: 50),
                        UserAgent = c.String(maxLength: 500),
                        Imei = c.String(maxLength: 50),
                        DeviceType = c.Int(nullable: false),
                        Model = c.String(maxLength: 50),
                        PlusVersion = c.String(maxLength: 50),
                        OsVersion = c.String(maxLength: 50),
                        Net = c.String(maxLength: 50),
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
            
            AlterColumn("dbo.Contents", "Suffix", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contents", "Suffix", c => c.String());
            DropTable("dbo.Updates");
            DropTable("dbo.UpdateLogs");
            DropTable("dbo.FeeBacks");
        }
    }
}
