using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerWay.CompanyService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Company");

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ProfilePhoto = table.Column<string>(type: "text", nullable: true),
                    WebSite = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Instragram = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Twitter = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Linkedin = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    EstablishmentYear = table.Column<short>(type: "smallint", nullable: true),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "Company",
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyPackage",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PackageId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPackage_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                schema: "Company",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPackage_CompanyId",
                schema: "Company",
                table: "CompanyPackage",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPackage",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "Company");
        }
    }
}
