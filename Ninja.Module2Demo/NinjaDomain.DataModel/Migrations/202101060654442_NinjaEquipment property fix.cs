namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinjaEquipmentpropertyfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NinjaEquipments", "Type_Id", "dbo.NinjaEquipments");
            DropIndex("dbo.NinjaEquipments", new[] { "Type_Id" });
            AddColumn("dbo.NinjaEquipments", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.NinjaEquipments", "Type_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NinjaEquipments", "Type_Id", c => c.Int());
            DropColumn("dbo.NinjaEquipments", "Type");
            CreateIndex("dbo.NinjaEquipments", "Type_Id");
            AddForeignKey("dbo.NinjaEquipments", "Type_Id", "dbo.NinjaEquipments", "Id");
        }
    }
}
