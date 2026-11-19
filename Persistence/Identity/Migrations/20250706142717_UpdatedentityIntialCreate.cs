using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedentityIntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FristName",
                table: "Addresses",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Addresses",
                newName: "FristName");
        }
    }
}
