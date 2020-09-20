namespace WebAppVinyl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableOrderIdInCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "OrderId" });
            AlterColumn("dbo.Carts", "OrderId", c => c.Int());
            CreateIndex("dbo.Carts", "OrderId");
            AddForeignKey("dbo.Carts", "OrderId", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "OrderId" });
            AlterColumn("dbo.Carts", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "OrderId");
            AddForeignKey("dbo.Carts", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
