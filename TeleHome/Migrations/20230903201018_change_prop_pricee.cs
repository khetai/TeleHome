using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleHome.Migrations
{
    /// <inheritdoc />
    public partial class change_prop_pricee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDiscountPrice",
                schema: "rmlubeco_telehome",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductDiscountPrice",
                schema: "rmlubeco_telehome",
                table: "Products",
                type: "float",
                nullable: true);
        }
    }
}
