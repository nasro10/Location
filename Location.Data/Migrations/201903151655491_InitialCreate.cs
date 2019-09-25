namespace Location.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        bookingState = c.Int(nullable: false),
                        CarsNumber = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Driver = c.Boolean(nullable: false),
                        Delivery = c.Boolean(nullable: false),
                        Note = c.String(),
                        Car_CarID = c.Int(),
                        Invoice_InvoiceID = c.Int(),
                        Client_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Cars", t => t.Car_CarID)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceID)
                .ForeignKey("dbo.Users", t => t.Client_UserID)
                .Index(t => t.Car_CarID)
                .Index(t => t.Invoice_InvoiceID)
                .Index(t => t.Client_UserID);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        SerialNumber = c.Int(nullable: false),
                        Note = c.String(),
                        Fuel = c.Int(nullable: false),
                        LoanMinPrice = c.Double(nullable: false),
                        PricePerHour = c.Double(nullable: false),
                        PricePerDay = c.Double(nullable: false),
                        PricePerMonth = c.Double(nullable: false),
                        seatNumber = c.Int(nullable: false),
                        Gear = c.Int(nullable: false),
                        CarModel_CarModelID = c.Int(),
                        Category_CategoryID = c.Int(),
                        CreationYear_CreationYearID = c.Int(),
                        TypeCar_TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.CarID)
                .ForeignKey("dbo.CarModels", t => t.CarModel_CarModelID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .ForeignKey("dbo.CreationYears", t => t.CreationYear_CreationYearID)
                .ForeignKey("dbo.TypeCars", t => t.TypeCar_TypeID)
                .Index(t => t.CarModel_CarModelID)
                .Index(t => t.Category_CategoryID)
                .Index(t => t.CreationYear_CreationYearID)
                .Index(t => t.TypeCar_TypeID);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        CarModelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CarModelID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subscription = c.Single(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.CreationYears",
                c => new
                    {
                        CreationYearID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CreationYearID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Car_CarID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_CarID)
                .Index(t => t.Car_CarID);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        RateID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Number = c.Int(nullable: false),
                        Car_CarID = c.Int(),
                        Client_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.RateID)
                .ForeignKey("dbo.Cars", t => t.Car_CarID)
                .ForeignKey("dbo.Users", t => t.Client_UserID)
                .Index(t => t.Car_CarID)
                .Index(t => t.Client_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        Name = c.String(),
                        CIN = c.Int(nullable: false),
                        phone = c.Int(nullable: false),
                        SignUpDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Picture = c.String(),
                        Password = c.String(nullable: false),
                        CinPicture = c.String(),
                        WorkingLicencePicture = c.String(),
                        WorkReport = c.String(),
                        CreditCard = c.Int(),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Gender = c.String(),
                        Email = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Country_CountryID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Countries", t => t.Country_CountryID)
                .Index(t => t.Country_CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        Country_CountryID = c.Int(),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.Countries", t => t.Country_CountryID)
                .Index(t => t.Country_CountryID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationID = c.Int(nullable: false, identity: true),
                        SendDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Description = c.String(),
                        Admin_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.NotificationID)
                .ForeignKey("dbo.Users", t => t.Admin_UserID)
                .Index(t => t.Admin_UserID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Price = c.Single(nullable: false),
                        Duration = c.Int(nullable: false),
                        Client_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Users", t => t.Client_UserID)
                .Index(t => t.Client_UserID);
            
            CreateTable(
                "dbo.TypeCars",
                c => new
                    {
                        TypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Picture = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "TypeCar_TypeID", "dbo.TypeCars");
            DropForeignKey("dbo.Bookings", "Client_UserID", "dbo.Users");
            DropForeignKey("dbo.Rates", "Client_UserID", "dbo.Users");
            DropForeignKey("dbo.Invoices", "Client_UserID", "dbo.Users");
            DropForeignKey("dbo.Bookings", "Invoice_InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.Notifications", "Admin_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "Country_CountryID", "dbo.Countries");
            DropForeignKey("dbo.States", "Country_CountryID", "dbo.Countries");
            DropForeignKey("dbo.Rates", "Car_CarID", "dbo.Cars");
            DropForeignKey("dbo.Images", "Car_CarID", "dbo.Cars");
            DropForeignKey("dbo.Cars", "CreationYear_CreationYearID", "dbo.CreationYears");
            DropForeignKey("dbo.SubCategories", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Cars", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Cars", "CarModel_CarModelID", "dbo.CarModels");
            DropForeignKey("dbo.Bookings", "Car_CarID", "dbo.Cars");
            DropIndex("dbo.Invoices", new[] { "Client_UserID" });
            DropIndex("dbo.Notifications", new[] { "Admin_UserID" });
            DropIndex("dbo.States", new[] { "Country_CountryID" });
            DropIndex("dbo.Users", new[] { "Country_CountryID" });
            DropIndex("dbo.Rates", new[] { "Client_UserID" });
            DropIndex("dbo.Rates", new[] { "Car_CarID" });
            DropIndex("dbo.Images", new[] { "Car_CarID" });
            DropIndex("dbo.SubCategories", new[] { "Category_CategoryID" });
            DropIndex("dbo.Cars", new[] { "TypeCar_TypeID" });
            DropIndex("dbo.Cars", new[] { "CreationYear_CreationYearID" });
            DropIndex("dbo.Cars", new[] { "Category_CategoryID" });
            DropIndex("dbo.Cars", new[] { "CarModel_CarModelID" });
            DropIndex("dbo.Bookings", new[] { "Client_UserID" });
            DropIndex("dbo.Bookings", new[] { "Invoice_InvoiceID" });
            DropIndex("dbo.Bookings", new[] { "Car_CarID" });
            DropTable("dbo.TypeCars");
            DropTable("dbo.Invoices");
            DropTable("dbo.Notifications");
            DropTable("dbo.States");
            DropTable("dbo.Countries");
            DropTable("dbo.Users");
            DropTable("dbo.Rates");
            DropTable("dbo.Images");
            DropTable("dbo.CreationYears");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
            DropTable("dbo.CarModels");
            DropTable("dbo.Cars");
            DropTable("dbo.Bookings");
        }
    }
}
