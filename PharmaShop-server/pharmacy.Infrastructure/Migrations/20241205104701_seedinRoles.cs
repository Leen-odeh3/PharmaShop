using Microsoft.EntityFrameworkCore.Migrations;
using pharmacy.Core.Enums;

#nullable disable

namespace pharmacy.Infrastructure.Migrations
{
    public partial class seedinRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Admin') 
                BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())
                END");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Customer') 
                BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES (NEWID(), 'Customer', 'CUSTOMER', NEWID())
                END");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Pharmacist') 
                BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES (NEWID(), 'Pharmacist', 'PHARMACIST', NEWID())
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Name IN ('Admin', 'Customer', 'Pharmacist')");
        }
    }
}
