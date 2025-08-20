namespace Erp_sistemi1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        CustomerCode = c.String(nullable: false, maxLength: 50),
                        CustomerName = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                        Address = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
