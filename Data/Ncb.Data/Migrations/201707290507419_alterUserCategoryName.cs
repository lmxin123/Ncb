namespace Ncb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterUserCategoryName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DeviceCategories", newName: "UserCategories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserCategories", newName: "DeviceCategories");
        }
    }
}
