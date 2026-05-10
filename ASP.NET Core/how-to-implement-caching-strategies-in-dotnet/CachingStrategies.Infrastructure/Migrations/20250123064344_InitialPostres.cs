using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CachingStrategies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "caching_strategies");

            migrationBuilder.CreateTable(
                name: "products",
                schema: "caching_strategies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "caching_strategies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_carts",
                schema: "caching_strategies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_carts", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_carts_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "caching_strategies",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_cart_items",
                schema: "caching_strategies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_cart_items_product_carts_product_cart_id",
                        column: x => x.product_cart_id,
                        principalSchema: "caching_strategies",
                        principalTable: "product_carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "caching_strategies",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_cart_items_product_cart_id",
                schema: "caching_strategies",
                table: "product_cart_items",
                column: "product_cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_cart_items_product_id",
                schema: "caching_strategies",
                table: "product_cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_carts_user_id",
                schema: "caching_strategies",
                table: "product_carts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "caching_strategies",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_cart_items",
                schema: "caching_strategies");

            migrationBuilder.DropTable(
                name: "product_carts",
                schema: "caching_strategies");

            migrationBuilder.DropTable(
                name: "products",
                schema: "caching_strategies");

            migrationBuilder.DropTable(
                name: "users",
                schema: "caching_strategies");
        }
    }
}
