using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticatorMvc.Migrations
{
    /// <inheritdoc />
    public partial class v12111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailActive = table.Column<bool>(type: "bit", nullable: false),
                    IsQRActive = table.Column<bool>(type: "bit", nullable: false),
                    QrCodeSetupManualEntryKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrCodeSetupImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrCodeSetupCustomerSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserGuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
