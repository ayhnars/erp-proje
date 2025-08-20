namespace Erp_sistemi1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 255),
                        ProductCode = c.String(maxLength: 100),
                        Quantity = c.Int(nullable: false),
                        UnitType = c.String(maxLength: 50),
                        MinStockLevel = c.Int(nullable: false),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductDescription = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
