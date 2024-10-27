using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerJH.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evenementen",
                columns: table => new
                {
                    EvenementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDoLijst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoodschappenLijst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftenLijst = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenementen", x => x.EvenementID);
                });

            migrationBuilder.CreateTable(
                name: "Boodschappen",
                columns: table => new
                {
                    BoodschapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvenementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boodschappen", x => x.BoodschapID);
                    table.ForeignKey(
                        name: "FK_Boodschappen_Evenementen_EvenementID",
                        column: x => x.EvenementID,
                        principalTable: "Evenementen",
                        principalColumn: "EvenementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shiften",
                columns: table => new
                {
                    ShiftID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftOmschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvenementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shiften", x => x.ShiftID);
                    table.ForeignKey(
                        name: "FK_Shiften_Evenementen_EvenementID",
                        column: x => x.EvenementID,
                        principalTable: "Evenementen",
                        principalColumn: "EvenementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    TodoItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvenementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.TodoItemID);
                    table.ForeignKey(
                        name: "FK_TodoItems_Evenementen_EvenementID",
                        column: x => x.EvenementID,
                        principalTable: "Evenementen",
                        principalColumn: "EvenementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boodschappen_EvenementID",
                table: "Boodschappen",
                column: "EvenementID");

            migrationBuilder.CreateIndex(
                name: "IX_Shiften_EvenementID",
                table: "Shiften",
                column: "EvenementID");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_EvenementID",
                table: "TodoItems",
                column: "EvenementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boodschappen");

            migrationBuilder.DropTable(
                name: "Shiften");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "Evenementen");
        }
    }
}
