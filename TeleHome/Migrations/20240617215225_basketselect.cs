using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleHome.Migrations
{
    /// <inheritdoc />
    public partial class basketselect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

          

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<bool>(
              name: "BasketSelected",
              schema: "rmlubeco_telehome",
              table: "Baskets",
              type: "bit",
              nullable: false,
              defaultValue: true);



        }
    }
}
