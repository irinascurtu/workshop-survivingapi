using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Position = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LinkedIn = table.Column<string>(nullable: true),
                    Skype = table.Column<string>(nullable: true),
                    GitHub = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyWebsite = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeakerId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talks_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "Id", "CompanyName", "CompanyWebsite", "Description", "Email", "Facebook", "FirstName", "GitHub", "LastName", "LinkedIn", "Position", "Skype", "Twitter", "Website" },
                values: new object[] { 1, "", null, null, null, null, "Berry", null, "Griffin Beak Eldritch", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "Id", "CompanyName", "CompanyWebsite", "Description", "Email", "Facebook", "FirstName", "GitHub", "LastName", "LinkedIn", "Position", "Skype", "Twitter", "Website" },
                values: new object[] { 2, "", null, null, null, null, "Nancy", null, "Swashbuckler Rye", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "Id", "Description", "SpeakerId", "Title" },
                values: new object[] { 1, "Commandeering a ship in rough waters isn't easy.  Commandeering it without getting caught is even harder.  In this course you'll learn how to sail away and avoid those pesky musketeers.", 1, "Commandeering a Ship Without Getting Caught" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "Id", "Description", "SpeakerId", "Title" },
                values: new object[] { 2, "Every good pirate loves rum, but it also has a tendency to get you into trouble.  In this course you'll learn how to avoid that.  This new exclusive edition includes an additional chapter on how to run fast without falling while drunk.", 1, "Avoiding Brawls While Drinking as Much Rum as You Desire" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "Id", "Description", "SpeakerId", "Title" },
                values: new object[] { 3, "In this course, the author provides tips to avoid, or, if needed, overthrow pirate mutiny.", 2, "Overthrowing Mutiny" });

            migrationBuilder.CreateIndex(
                name: "IX_Talks_SpeakerId",
                table: "Talks",
                column: "SpeakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talks");

            migrationBuilder.DropTable(
                name: "Speakers");
        }
    }
}
