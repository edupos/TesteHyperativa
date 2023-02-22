using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteHyperativa_Repository.Migrations
{
    /// <inheritdoc />
    public partial class HyperativaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InputMode = table.Column<int>(type: "int", nullable: false),
                    InputDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InputUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCardsBatchFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InputDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreditCardsText = table.Column<string>(type: "nvarchar(max)", maxLength: 1000000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardsBatchFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "CreditCardsBatchFiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
