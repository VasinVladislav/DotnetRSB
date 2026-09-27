using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelOneMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelOneMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelTwoMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    LevelOneMenuId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelTwoMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelTwoMenu_LevelOneMenu_LevelOneMenuId",
                        column: x => x.LevelOneMenuId,
                        principalTable: "LevelOneMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LevelThreeMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Href = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    LevelTwoMenuId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelThreeMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelThreeMenu_LevelTwoMenu_LevelTwoMenuId",
                        column: x => x.LevelTwoMenuId,
                        principalTable: "LevelTwoMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelThreeMenu_LevelTwoMenuId",
                table: "LevelThreeMenu",
                column: "LevelTwoMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelTwoMenu_LevelOneMenuId",
                table: "LevelTwoMenu",
                column: "LevelOneMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelThreeMenu");

            migrationBuilder.DropTable(
                name: "LevelTwoMenu");

            migrationBuilder.DropTable(
                name: "LevelOneMenu");
        }
    }
}
