using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CaptchaTraining.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaptchaDataSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    hasCyrillic = table.Column<bool>(type: "boolean", nullable: false),
                    hasLatin = table.Column<bool>(type: "boolean", nullable: false),
                    hasNumeric = table.Column<bool>(type: "boolean", nullable: false),
                    hasSpecialSymbols = table.Column<bool>(type: "boolean", nullable: false),
                    isCaseSesitive = table.Column<bool>(type: "boolean", nullable: false),
                    hasAnswers = table.Column<bool>(type: "boolean", nullable: false),
                    Pictures = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptchaDataSets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaptchaDataSets");
        }
    }
}
