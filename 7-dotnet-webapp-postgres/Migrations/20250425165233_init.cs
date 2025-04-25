using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace postgres.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "sales",
                columns: table => new
                {
                    categoryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    categoryname = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "sales",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customername = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    contactname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    city = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    postalcode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    country = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customers_pkey", x => x.customerid);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                schema: "sales",
                columns: table => new
                {
                    employeeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lastname = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    firstname = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    photo = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    notes = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employees_pkey", x => x.employeeid);
                });

            migrationBuilder.CreateTable(
                name: "shippers",
                schema: "sales",
                columns: table => new
                {
                    shipperid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    shippername = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shippers_pkey", x => x.shipperid);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                schema: "sales",
                columns: table => new
                {
                    supplierid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    suppliername = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    contactname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    city = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    postalcode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    country = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("suppliers_pkey", x => x.supplierid);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "sales",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerid = table.Column<int>(type: "integer", nullable: true),
                    employeeid = table.Column<int>(type: "integer", nullable: true),
                    orderdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    shipperid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_pkey", x => x.orderid);
                    table.ForeignKey(
                        name: "orders_customerid_fkey",
                        column: x => x.customerid,
                        principalSchema: "sales",
                        principalTable: "customers",
                        principalColumn: "customerid");
                    table.ForeignKey(
                        name: "orders_employeeid_fkey",
                        column: x => x.employeeid,
                        principalSchema: "sales",
                        principalTable: "employees",
                        principalColumn: "employeeid");
                    table.ForeignKey(
                        name: "orders_shipperid_fkey",
                        column: x => x.shipperid,
                        principalSchema: "sales",
                        principalTable: "shippers",
                        principalColumn: "shipperid");
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "sales",
                columns: table => new
                {
                    productid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    supplierid = table.Column<int>(type: "integer", nullable: true),
                    categoryid = table.Column<int>(type: "integer", nullable: true),
                    unit = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.productid);
                    table.ForeignKey(
                        name: "products_categoryid_fkey",
                        column: x => x.categoryid,
                        principalSchema: "sales",
                        principalTable: "categories",
                        principalColumn: "categoryid");
                    table.ForeignKey(
                        name: "products_supplierid_fkey",
                        column: x => x.supplierid,
                        principalSchema: "sales",
                        principalTable: "suppliers",
                        principalColumn: "supplierid");
                });

            migrationBuilder.CreateTable(
                name: "orderdetails",
                schema: "sales",
                columns: table => new
                {
                    orderdetailid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<int>(type: "integer", nullable: true),
                    productid = table.Column<int>(type: "integer", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orderdetails_pkey", x => x.orderdetailid);
                    table.ForeignKey(
                        name: "orderdetails_orderid_fkey",
                        column: x => x.orderid,
                        principalSchema: "sales",
                        principalTable: "orders",
                        principalColumn: "orderid");
                    table.ForeignKey(
                        name: "orderdetails_productid_fkey",
                        column: x => x.productid,
                        principalSchema: "sales",
                        principalTable: "products",
                        principalColumn: "productid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderdetails_orderid",
                schema: "sales",
                table: "orderdetails",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orderdetails_productid",
                schema: "sales",
                table: "orderdetails",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customerid",
                schema: "sales",
                table: "orders",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_employeeid",
                schema: "sales",
                table: "orders",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_shipperid",
                schema: "sales",
                table: "orders",
                column: "shipperid");

            migrationBuilder.CreateIndex(
                name: "IX_products_categoryid",
                schema: "sales",
                table: "products",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_products_supplierid",
                schema: "sales",
                table: "products",
                column: "supplierid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderdetails",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "products",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "employees",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "shippers",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "suppliers",
                schema: "sales");
        }
    }
}
