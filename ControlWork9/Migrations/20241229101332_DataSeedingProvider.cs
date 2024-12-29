using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlWork9.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "Name", "ServiceType" },
                values: new object[,]
                {
                    { 1, "Megacom", "Mobile" },
                    { 2, "Beeline", "Internet" },
                    { 3, "O!", "Mobile" },
                    { 4, "Saima", "Internet" },
                    { 5, "Onoi", "Internet" }
                });
            migrationBuilder.InsertData(
                table: "ServiceProviders",
                columns: new[] { "Id", "ProviderId", "ServiceName", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Mobile Service", 10.0m },
                    { 2, 2, "Internet Service", 15.0m },
                    { 3, 3, "Mobile Service", 12.0m },
                    { 4, 4, "Internet Service", 8.0m },
                    { 5, 5, "Internet Service", 11.0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });
            
            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5});
        }
    }
}
