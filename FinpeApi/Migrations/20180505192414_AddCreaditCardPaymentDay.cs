using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinpeApi.Migrations
{
    public partial class AddCreaditCardPaymentDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentDay",
                table: "CreditCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CategoryId",
                table: "CreditCards",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Categories_CategoryId",
                table: "CreditCards",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Categories_CategoryId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_CategoryId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "PaymentDay",
                table: "CreditCards");
        }
    }
}
