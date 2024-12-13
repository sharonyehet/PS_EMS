using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS_EMS.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                        table: "Employees",
                        columns: new[] { "FirstName", "LastName", "Email", "DateOfBirth", "Gender", "Department", "PhoneNumber" },
                        values: new object[,]
                        {
                { "John", "Doe", "john.doe@example.com", "01/01/2000", 1, 1, "0123456789" },
                { "Jane", "Smith", "jane.smith@example.com", "02/02/2000", 2, 2, "0134567898" },
                { "Sam", "Wilson", "sam.wilson@example.com", "03/03/2000", 1, 3, "0145678987" }
                        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Employees WHERE Id IN (1, 2, 3)");
        }
    }
}
