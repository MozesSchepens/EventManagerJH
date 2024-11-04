using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "ToDoItems",
                columns: table => new
                {
                    TodoItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvenementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.TodoItemID);
                    table.ForeignKey(
                        name: "FK_ToDoItems_Evenementen_EvenementID",
                        column: x => x.EvenementID,
                        principalTable: "Evenementen",
                        principalColumn: "EvenementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Evenementen",
                columns: new[] { "EvenementID", "Beschrijving", "Datum", "Locatie", "Titel" },
                values: new object[,]
                {
                    { 1, "Groot feest", new DateTime(2024, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jeugdhuis", "Koerrock" },
                    { 2, "Privé evenement", new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Binnen", "Verjaardag Casi" }
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
                name: "IX_ToDoItems_EvenementID",
                table: "ToDoItems",
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
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Evenementen");
        }
    }
}
