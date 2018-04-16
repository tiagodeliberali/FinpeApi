using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinpeApi.Migrations
{
    public partial class AddPaymentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankStatements_Banks_BankId",
                table: "BankStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_Statements_Categories_CategoryId",
                table: "Statements");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Statements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Statements",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                table: "BankStatements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BankStatements_Banks_BankId",
                table: "BankStatements",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Statements_Categories_CategoryId",
                table: "Statements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankStatements_Banks_BankId",
                table: "BankStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_Statements_Categories_CategoryId",
                table: "Statements");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Statements");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Statements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                table: "BankStatements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankStatements_Banks_BankId",
                table: "BankStatements",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statements_Categories_CategoryId",
                table: "Statements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
