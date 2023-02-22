using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteHyperativa_Repository.Migrations
{
    /// <inheritdoc />
    public partial class HyperativaTables_CamposBatchFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfRecords",
                table: "CreditCardsBatchFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LoteDate",
                table: "CreditCardsBatchFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "CreditCardsBatchFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfRecords",
                table: "CreditCardsBatchFiles");

            migrationBuilder.DropColumn(
                name: "LoteDate",
                table: "CreditCardsBatchFiles");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "CreditCardsBatchFiles");
        }
    }
}
