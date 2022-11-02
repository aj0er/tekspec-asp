using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleForum.Migrations
{
    public partial class ThreadAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Thread",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Thread_AuthorId",
                table: "Thread",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thread_AspNetUsers_AuthorId",
                table: "Thread",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thread_AspNetUsers_AuthorId",
                table: "Thread");

            migrationBuilder.DropIndex(
                name: "IX_Thread_AuthorId",
                table: "Thread");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Thread");
        }
    }
}
