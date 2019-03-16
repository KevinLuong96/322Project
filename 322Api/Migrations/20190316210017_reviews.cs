using Microsoft.EntityFrameworkCore.Migrations;

namespace _322Api.Migrations
{
    public partial class reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Reviews",
                newName: "PhoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneId",
                table: "Reviews",
                newName: "DeviceId");
        }
    }
}
