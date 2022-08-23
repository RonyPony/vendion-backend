using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace vendio_backend.Migrations
{
    public partial class vehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdBy = table.Column<int>(type: "integer", nullable: false),
                    isEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    isPublished = table.Column<bool>(type: "boolean", nullable: false),
                    features = table.Column<List<string>>(type: "text[]", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    contactPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    registerDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<string>(type: "text", nullable: false),
                    vim = table.Column<string>(type: "text", nullable: false),
                    condition = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
