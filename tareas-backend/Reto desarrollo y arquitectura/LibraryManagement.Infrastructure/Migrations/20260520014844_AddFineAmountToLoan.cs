using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFineAmountToLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoockCopyId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LonedDate",
                table: "Loans",
                newName: "LoanedDate");

            migrationBuilder.RenameColumn(
                name: "Fineamout",
                table: "Loans",
                newName: "FineAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoanedDate",
                table: "Loans",
                newName: "LonedDate");

            migrationBuilder.RenameColumn(
                name: "FineAmount",
                table: "Loans",
                newName: "Fineamout");

            migrationBuilder.AddColumn<Guid>(
                name: "BoockCopyId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
