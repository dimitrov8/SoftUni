using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class CreatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Done" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { new Guid("668cc731-dc84-4402-9de2-a3191e1a494d"), 1, new DateTime(2023, 7, 10, 12, 35, 27, 858, DateTimeKind.Utc).AddTicks(3675), "Implement better styling for all public pages", "42c27fad-0b2d-4a0d-b431-6a1f166e4cab", "Improve CSS styles" },
                    { new Guid("87d405fa-2d41-4ca8-a61b-f061efcb8e97"), 3, new DateTime(2023, 12, 26, 12, 35, 27, 858, DateTimeKind.Utc).AddTicks(3761), "Create Desktop client App for the RESTful TaskBoard service", "e35a80d1-b78c-4ad3-b516-7a48608083e5", "Desktop Client App" },
                    { new Guid("cf49bd6f-4bce-49a0-ba13-9a1d24843ef1"), 3, new DateTime(2023, 1, 26, 12, 35, 27, 858, DateTimeKind.Utc).AddTicks(3766), "Implement [Create Task] page for adding tasks", "62d4b959-4f43-402f-94a2-0b838a4a539f", "Create Tasks" },
                    { new Guid("e6217dad-a9a8-46fa-ac96-c49a17433ed9"), 2, new DateTime(2023, 8, 26, 12, 35, 27, 858, DateTimeKind.Utc).AddTicks(3749), "Create Android client App for the RESTful TaskBoard service", "e35a80d1-b78c-4ad3-b516-7a48608083e5", "Android Client App" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
