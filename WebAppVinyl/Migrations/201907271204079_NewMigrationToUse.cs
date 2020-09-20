namespace WebAppVinyl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrationToUse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", new[] { "Cart_VinylId", "Cart_BuyerId" }, "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "Cart_VinylId", "Cart_BuyerId" });
            AddColumn("dbo.Carts", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "OrderId");
            AddForeignKey("dbo.Carts", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "CartId");
            DropColumn("dbo.Orders", "Cart_VinylId");
            DropColumn("dbo.Orders", "Cart_BuyerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Cart_BuyerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "Cart_VinylId", c => c.Int());
            AddColumn("dbo.Orders", "CartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "OrderId" });
            DropColumn("dbo.Carts", "OrderId");
            CreateIndex("dbo.Orders", new[] { "Cart_VinylId", "Cart_BuyerId" });
            AddForeignKey("dbo.Orders", new[] { "Cart_VinylId", "Cart_BuyerId" }, "dbo.Carts", new[] { "VinylId", "BuyerId" });
        }
    }
}
