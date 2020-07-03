using Microsoft.EntityFrameworkCore.Migrations;

namespace Watches.DataAccess.Migrations
{
    public partial class promena_use_case3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUseCase_Users_UserId",
                table: "UserUseCase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUseCase",
                table: "UserUseCase");

            migrationBuilder.RenameTable(
                name: "UserUseCase",
                newName: "UserUseCases");

            migrationBuilder.RenameIndex(
                name: "IX_UserUseCase_UserId",
                table: "UserUseCases",
                newName: "IX_UserUseCases_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUseCases",
                table: "UserUseCases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUseCases_Users_UserId",
                table: "UserUseCases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUseCases_Users_UserId",
                table: "UserUseCases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUseCases",
                table: "UserUseCases");

            migrationBuilder.RenameTable(
                name: "UserUseCases",
                newName: "UserUseCase");

            migrationBuilder.RenameIndex(
                name: "IX_UserUseCases_UserId",
                table: "UserUseCase",
                newName: "IX_UserUseCase_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUseCase",
                table: "UserUseCase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUseCase_Users_UserId",
                table: "UserUseCase",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
