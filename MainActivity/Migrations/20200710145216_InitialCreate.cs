using Microsoft.EntityFrameworkCore.Migrations;

namespace MainActivity.Migrations {
    public partial class InitialCreate : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Administrators", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ArticleContents",
                columns: table => new {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    ParagraphTitle = table.Column<string>(nullable: true),
                    ParagraphContent = table.Column<string>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_ArticleContents", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "ArticleContents");
        }
    }
}
