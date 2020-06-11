using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcessaCity.Data.Migrations
{
    public partial class AddCidadesECategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("2695bd36-45d4-4135-a1c5-488e592788e5"), true, null, "Pavimentação danificada" },
                    { new Guid("5aa23f4c-e480-462b-b402-966ea8bab551"), true, null, "Sinalização de trânsito" },
                    { new Guid("47632f40-852f-4957-9813-34f1464a1849"), true, null, "Mecânismos de mobilidade" },
                    { new Guid("a255e104-4b55-4954-aee9-07513da17e44"), true, null, "Vandalismo" },
                    { new Guid("6a47411f-eac3-460b-8ede-b2e1a03137cd"), true, null, "Riscos à integridade física" }
                });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7ae590f1-c6a4-4bb3-91bf-1e82ea45bb4b"),
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { -22.9064m, -47.0616m });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IBGECode", "Latitude", "Longitude", "Name", "StateId" },
                values: new object[,]
                {
                    { new Guid("d9805d6e-4048-4783-8497-b8d4a237ef50"), 3552403, -22.8216m, -47.2664m, "Sumaré", new Guid("b545ceb9-fbde-43c9-bbcc-de62a49e1661") },
                    { new Guid("1c3ca2cf-1e8e-4320-9868-65dc8d447315"), 3519071, -22.8577m, -47.2203m, "Hortolândia", new Guid("b545ceb9-fbde-43c9-bbcc-de62a49e1661") },
                    { new Guid("6b4faa2d-22ff-47c9-9023-cf9c45bb3184"), 3536505, -22.7617m, -47.1541m, "Paulínia", new Guid("b545ceb9-fbde-43c9-bbcc-de62a49e1661") },
                    { new Guid("b79016eb-3a9d-4f67-ac79-6c6539d99327"), 3556206, -22.9712m, -46.9964m, "Valinhos", new Guid("b545ceb9-fbde-43c9-bbcc-de62a49e1661") }
                });

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("48cf5f0f-40c9-4a79-9627-6fd22018f72c"),
                column: "Review",
                value: true);

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("52ccae2e-af86-4fcc-82ea-9234088dbedf"),
                column: "Denied",
                value: true);

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("96afa0df-8ad9-4a44-a726-70582b7bd010"),
                column: "Approved",
                value: true);

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("c37d9588-1875-44dd-8cf1-6781de7533c3"),
                column: "InProgress",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2695bd36-45d4-4135-a1c5-488e592788e5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("47632f40-852f-4957-9813-34f1464a1849"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5aa23f4c-e480-462b-b402-966ea8bab551"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6a47411f-eac3-460b-8ede-b2e1a03137cd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a255e104-4b55-4954-aee9-07513da17e44"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1c3ca2cf-1e8e-4320-9868-65dc8d447315"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6b4faa2d-22ff-47c9-9023-cf9c45bb3184"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b79016eb-3a9d-4f67-ac79-6c6539d99327"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d9805d6e-4048-4783-8497-b8d4a237ef50"));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7ae590f1-c6a4-4bb3-91bf-1e82ea45bb4b"),
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { -22.8920565m, -47.2079794m });

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("48cf5f0f-40c9-4a79-9627-6fd22018f72c"),
                column: "Review",
                value: true);

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("52ccae2e-af86-4fcc-82ea-9234088dbedf"),
                column: "Denied",
                value: true);

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("96afa0df-8ad9-4a44-a726-70582b7bd010"),
                column: "Approved",
                value: true);

            migrationBuilder.UpdateData(
                table: "ReportStatus",
                keyColumn: "Id",
                keyValue: new Guid("c37d9588-1875-44dd-8cf1-6781de7533c3"),
                column: "InProgress",
                value: true);
        }
    }
}
