using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicWeb.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imeGrada = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odeljenja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivOdeljenja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opisOdeljenja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slikaOdeljenja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odeljenja", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Klinike",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivKlinike = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gradID = table.Column<int>(type: "int", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klinike", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Klinike_Gradovi_gradID",
                        column: x => x.gradID,
                        principalTable: "Gradovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KlinikeOdeljenja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    klinikaID = table.Column<int>(type: "int", nullable: true),
                    odeljenjeID = table.Column<int>(type: "int", nullable: true),
                    lekar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlinikeOdeljenja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KlinikeOdeljenja_Klinike_klinikaID",
                        column: x => x.klinikaID,
                        principalTable: "Klinike",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KlinikeOdeljenja_Odeljenja_odeljenjeID",
                        column: x => x.odeljenjeID,
                        principalTable: "Odeljenja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlinikaOdeljenjeID = table.Column<int>(type: "int", nullable: true),
                    termin = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rezervacije_KlinikeOdeljenja_KlinikaOdeljenjeID",
                        column: x => x.KlinikaOdeljenjeID,
                        principalTable: "KlinikeOdeljenja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klinike_gradID",
                table: "Klinike",
                column: "gradID");

            migrationBuilder.CreateIndex(
                name: "IX_KlinikeOdeljenja_klinikaID",
                table: "KlinikeOdeljenja",
                column: "klinikaID");

            migrationBuilder.CreateIndex(
                name: "IX_KlinikeOdeljenja_odeljenjeID",
                table: "KlinikeOdeljenja",
                column: "odeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_KlinikaOdeljenjeID",
                table: "Rezervacije",
                column: "KlinikaOdeljenjeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "KlinikeOdeljenja");

            migrationBuilder.DropTable(
                name: "Klinike");

            migrationBuilder.DropTable(
                name: "Odeljenja");

            migrationBuilder.DropTable(
                name: "Gradovi");
        }
    }
}
