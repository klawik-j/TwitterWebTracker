using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet1DataAccessLibrary.Migrations
{
    public partial class InitialDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Twitt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Profile_image_url = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Saved_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Twitt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Twitt");
        }
    }
}
