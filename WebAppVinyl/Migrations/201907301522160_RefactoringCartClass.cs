namespace WebAppVinyl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringCartClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "BuyerId", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "BuyerId" });
            DropPrimaryKey("dbo.Carts");
            AddColumn("dbo.Carts", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Carts", "BuyerId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Carts", "Id");
            CreateIndex("dbo.Carts", "BuyerId");
            AddForeignKey("dbo.Carts", "BuyerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "BuyerId", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "BuyerId" });
            DropPrimaryKey("dbo.Carts");
            AlterColumn("dbo.Carts", "BuyerId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Carts", "Id");
            AddPrimaryKey("dbo.Carts", new[] { "VinylId", "BuyerId" });
            CreateIndex("dbo.Carts", "BuyerId");
            AddForeignKey("dbo.Carts", "BuyerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
