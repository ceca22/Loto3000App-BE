using Microsoft.EntityFrameworkCore.Migrations;

namespace Loto3000App.DataAccess.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "PrizeType" },
                values: new object[,]
                {
                    { 1, "50$ Gift Card" },
                    { 2, "100$ Gift Card" },
                    { 3, "TV" },
                    { 4, "Vacation" },
                    { 5, "Car" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
