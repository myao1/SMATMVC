using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SMATMVC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Creator = table.Column<string>(nullable: true),
                    LastItemModifiedDate = table.Column<DateTime>(nullable: true),
                    Owners = table.Column<string>(nullable: true),
                    ProposedDisposition = table.Column<int>(nullable: false),
                    SiteCollectionAdmins = table.Column<string>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    WebUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInfo", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteInfo");
        }
    }
}
