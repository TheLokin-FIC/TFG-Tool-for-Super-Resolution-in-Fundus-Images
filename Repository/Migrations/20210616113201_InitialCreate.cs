using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineLearningModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Architecture = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Loss = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineLearningModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperResolutionModels",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    UpscaleFactor = table.Column<byte>(type: "tinyint", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperResolutionModels", x => new { x.ModelId, x.UpscaleFactor });
                    table.ForeignKey(
                        name: "FK_SuperResolutionModels_MachineLearningModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "MachineLearningModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MachineLearningModels",
                columns: new[] { "Id", "Architecture", "Loss", "Name" },
                values: new object[] { 1, "SRResNet", "MSE", "Super Resolution" });

            migrationBuilder.InsertData(
                table: "MachineLearningModels",
                columns: new[] { "Id", "Architecture", "Loss", "Name" },
                values: new object[] { 2, "SRGAN", "MSE", "Super Resolution" });

            migrationBuilder.InsertData(
                table: "MachineLearningModels",
                columns: new[] { "Id", "Architecture", "Loss", "Name" },
                values: new object[] { 3, "SRResNet", "SSIM", "Super Resolution" });

            migrationBuilder.InsertData(
                table: "SuperResolutionModels",
                columns: new[] { "ModelId", "UpscaleFactor", "Path" },
                values: new object[,]
                {
                    { 1, (byte)2, "Resources\\SuperResolution\\SRResNet\\MSE\\super_resolution_2x.onnx" },
                    { 1, (byte)4, "Resources\\SuperResolution\\SRResNet\\MSE\\super_resolution_4x.onnx" },
                    { 1, (byte)8, "Resources\\SuperResolution\\SRResNet\\MSE\\super_resolution_8x.onnx" },
                    { 2, (byte)2, "Resources\\SuperResolution\\SRGAN\\MSE\\super_resolution_2x.onnx" },
                    { 2, (byte)4, "Resources\\SuperResolution\\SRGAN\\MSE\\super_resolution_4x.onnx" },
                    { 2, (byte)8, "Resources\\SuperResolution\\SRGAN\\MSE\\super_resolution_8x.onnx" },
                    { 3, (byte)2, "Resources\\SuperResolution\\SRResNet\\SSIM\\super_resolution_2x.onnx" },
                    { 3, (byte)4, "Resources\\SuperResolution\\SRResNet\\SSIM\\super_resolution_4x.onnx" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperResolutionModels_Path",
                table: "SuperResolutionModels",
                column: "Path",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperResolutionModels");

            migrationBuilder.DropTable(
                name: "MachineLearningModels");
        }
    }
}