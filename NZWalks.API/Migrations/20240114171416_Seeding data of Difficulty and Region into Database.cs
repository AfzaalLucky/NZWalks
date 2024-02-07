using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataofDifficultyandRegionintoDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0640a92b-4c27-49b1-a2cf-57e221e087f2"), "High" },
                    { new Guid("326d922b-ebb0-4c19-8829-462a016e2621"), "Low" },
                    { new Guid("876cabf2-b5c1-456e-b130-5b9c37520ce9"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("8b315288-64a6-442b-aa10-2da99acffe3e"), "SHR-R", "Sharjah", "sharjah-region-image-url.jpg" },
                    { new Guid("def50004-2102-4d74-8f90-fd2f51c04d39"), "DXB-R", "Dubai Region", "dubai-region-image-url.jpg" },
                    { new Guid("fe21b1d4-80f7-4c28-b7db-85899a718e21"), "ADB-R", "Abu Dhabi", "abu-dhabi-region-image-url.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0640a92b-4c27-49b1-a2cf-57e221e087f2"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("326d922b-ebb0-4c19-8829-462a016e2621"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("876cabf2-b5c1-456e-b130-5b9c37520ce9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8b315288-64a6-442b-aa10-2da99acffe3e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("def50004-2102-4d74-8f90-fd2f51c04d39"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fe21b1d4-80f7-4c28-b7db-85899a718e21"));
        }
    }
}
