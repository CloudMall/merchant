using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMall.Services.Merchant.Migrations
{
    public partial class InitMerchant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditRecords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    OperationType = table.Column<sbyte>(nullable: false),
                    ObjectId = table.Column<string>(maxLength: 256, nullable: true),
                    OriginValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    Extra = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 16, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Extra = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantManagers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 16, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    Extra = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    State = table.Column<sbyte>(nullable: false),
                    Remark = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditRecords");

            migrationBuilder.DropTable(
                name: "MerchantCategories");

            migrationBuilder.DropTable(
                name: "MerchantManagers");

            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
