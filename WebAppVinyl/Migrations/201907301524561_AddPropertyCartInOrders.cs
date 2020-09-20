namespace WebAppVinyl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyCartInOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "OrderId" });
            AddColumn("dbo.Orders", "CartId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CartId");
            AddForeignKey("dbo.Orders", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "OrderId", c => c.Int());
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropColumn("dbo.Orders", "CartId");
            CreateIndex("dbo.Carts", "OrderId");
            AddForeignKey("dbo.Carts", "OrderId", "dbo.Orders", "Id");
        }
    }
}
