using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasincIzlemeProjesi.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorDatasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_veriler",
                table: "veriler");

            migrationBuilder.RenameTable(
                name: "veriler",
                newName: "SensorDatas");

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "SensorDatas",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SensorDatas",
                table: "SensorDatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SensorDatas",
                table: "SensorDatas");

            migrationBuilder.RenameTable(
                name: "SensorDatas",
                newName: "veriler");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "veriler",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_veriler",
                table: "veriler",
                column: "Id");
        }
    }
}
